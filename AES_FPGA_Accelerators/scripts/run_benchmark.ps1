param(
 [ValidateSet('all','hadipour','yaaes','secworks','cmt','hls')][string]$Variant='all',
 [string]$ToolsRoot='C:\AMDDesignTools\2026.1',
 [switch]$SkipImplementation
)
$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$bin=Join-Path $ToolsRoot 'Vivado/bin'
function Run([string]$Tool, [string[]]$Arguments) {
    # XSim embeds arguments in Tcl, where backslashes can become escapes.
    $toolArgs = @($Arguments | ForEach-Object { $_.Replace('\', '/') })
    & (Join-Path $bin "$Tool.bat") @toolArgs
    if ($LASTEXITCODE -ne 0) { throw "$Tool failed with exit code $LASTEXITCODE" }
}
if ($SkipImplementation -and $Variant -eq 'all') {
    throw '-SkipImplementation requires one explicit -Variant.'
}
if (!(Test-Path "$root/tests/vectors.txt")) { & "$PSScriptRoot/make_vectors.ps1" }
$variants = if($Variant -eq 'all') {@('hadipour','yaaes','secworks','cmt','hls')} else {@($Variant)}
foreach($name in $variants) {
    $dir=Join-Path $root "build/$name"
    # HLS project occupies build/hls; benchmark gets its own sibling directory.
    if($name -eq 'hls') {$dir=Join-Path $root 'build/hls_benchmark'}
    New-Item -ItemType Directory -Force $dir | Out-Null
    if ($SkipImplementation) {
        foreach ($artifact in @('routed.dcp','netlist.v','timing_status.txt')) {
            if (!(Test-Path -LiteralPath (Join-Path $dir $artifact))) {
                throw "Cannot skip implementation: $dir/$artifact is missing."
            }
        }
    }
    Copy-Item "$root/tests/vectors.txt" "$dir/vectors.txt" -Force
    # A local batch filename avoids path quoting in XSim's generated Tcl.
    Copy-Item "$PSScriptRoot/sim_saif.tcl" "$dir/sim_saif.tcl" -Force
    Push-Location $dir
    try {
        if (Test-Path 'metrics.csv') { Remove-Item -LiteralPath (Join-Path $dir 'metrics.csv') }
        if (!$SkipImplementation) {
            Run 'vivado' @('-mode','batch','-source',"$PSScriptRoot/implement.tcl",'-tclargs',$name)
        }
        Run 'xvlog' @('netlist.v',"$ToolsRoot/Vivado/data/verilog/src/glbl.v")
        Run 'xvlog' @('--sv',"$root/tests/tb_power.sv")
        Run 'xelab' @('tb_power','glbl','-L','unisims_ver','-L','unimacro_ver','-L','secureip','--debug','all','-s','power_sim')
        Run 'xsim' @('power_sim','--tclbatch','sim_saif.tcl')
        if (!(Test-Path 'metrics.csv')) { throw 'Simulation did not finish successfully; inspect xsim.log' }
        Run 'vivado' @('-mode','batch','-source',"$PSScriptRoot/power.tcl")
    } finally { Pop-Location }
}
python "$PSScriptRoot/summarize.py"
if ($LASTEXITCODE -ne 0) { throw 'Report summarization failed' }
