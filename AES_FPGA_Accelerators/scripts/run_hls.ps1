param([string]$ToolsRoot = 'C:\AMDDesignTools\2026.1')
$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
$bin = Join-Path $ToolsRoot 'Vitis/bin'
$config = Join-Path $PSScriptRoot 'hls_build.cfg'
$work = Join-Path $root 'build/hls_component'
function Invoke-HlsTool([string]$Name, [string[]]$ToolArguments) {
    & (Join-Path $bin "$Name.bat") @ToolArguments
    if ($LASTEXITCODE -ne 0) { throw "$Name failed (exit $LASTEXITCODE); stopping HLS build." }
}
if (!(Test-Path -LiteralPath "$root/tests/vectors.txt")) {
    throw 'Generate tests/vectors.txt with scripts/make_vectors.ps1 first.'
}
Push-Location $root
try {
    Invoke-HlsTool 'vitis-run' @('--mode','hls','--csim','--config',$config,'--work_dir',$work)
    Invoke-HlsTool 'v++' @('-c','--mode','hls','--config',$config,'--work_dir',$work)
    Invoke-HlsTool 'vitis-run' @('--mode','hls','--cosim','--config',$config,'--work_dir',$work)
    Invoke-HlsTool 'vitis-run' @('--mode','hls','--package','--config',$config,'--work_dir',$work)
    Write-Host "HLS flow completed: $work"
} finally { Pop-Location }
