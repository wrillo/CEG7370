# Stop before the legacy flow can repeat its source-registration failure.
error {Use: powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\run_hls.ps1 -ToolsRoot C:\AMDDesignTools\2026.1}

# Original flow retained below for reference only.
set root [file normalize [file join [file dirname [info script]] ..]]
cd $root

open_project -reset [file join $root build hls]
set_top aes128_ecb_hls
open_solution -reset solution

set_part {xc7z020clg484-1}
create_clock -period 10 -name default
config_rtl -reset all -reset_level high

set source_file [file join $root implementations aes128_ecb_hls.cpp]
set test_file [file join $root tests tb_hls.cpp]
set vectors_file [file join $root tests vectors.txt]
set include_dir [file join $root vendor vitis_security]

foreach f [list $source_file $test_file $vectors_file] {
    if {![file exists $f]} {
        error "Required file missing: $f"
    }
}

add_files $source_file
add_files -tb $test_file
add_files -tb $vectors_file

csim_design -clean
csynth_design
cosim_design -rtl verilog
export_design -format ip_catalog -rtl verilog
exit
