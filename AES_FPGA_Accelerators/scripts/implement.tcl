# Invoked with cwd = build/<variant>. No bitstream or board programming.
set root [file normalize [file join [file dirname [info script]] ..]]
set variant [lindex $argv 0]
set allowed {hadipour yaaes secworks cmt hls}
if {$variant ni $allowed} {error "Unknown variant: $variant"}
create_project -in_memory -part xc7z020clg484-1
if {$variant eq "hls"} {
  # Use exported RTL from the configuration-file flow.
  set rtl_dir ""
  foreach candidate [list $root/build/hls_component/hls/impl/ip/hdl/verilog $root/build/hls_component/hls/impl/verilog] {
    if {[file exists $candidate/aes128_ecb_hls.v]} {
      set rtl_dir $candidate
      break
    }
  }
  if {$rtl_dir eq ""} {error "Packaged HLS RTL missing. Run scripts/run_hls.ps1 first; inspect build/hls_component/hls/impl."}
  set rtl [glob $rtl_dir/*.v]
  read_verilog $rtl
  # Include HLS ROM initialization data if emitted by this tool version.
  foreach f [glob -nocomplain $rtl_dir/*.dat $rtl_dir/*.mem $rtl_dir/*.hex] {file copy -force $f .}
  set inst {wire unused_idle, unused_ready, unused_valid;
reg busy = 0;
assign ready = !busy && !rst;
always @(posedge clk) begin
  if (rst) busy <= 0;
  else if (start && ready) busy <= 1;
  else if (done) busy <= 0;
end
aes128_ecb_hls core(.ap_clk(clk), .ap_rst(rst), .ap_start(start && ready),
 .ap_done(done), .ap_idle(unused_idle), .ap_ready(unused_ready),
 .key(key), .plaintext(plaintext), .ciphertext(ciphertext), .ciphertext_ap_vld(unused_valid));}
} else {
  if {$variant in {hadipour yaaes}} {
    read_vhdl -vhdl2008 $root/implementations/aes128_ecb_${variant}.vhd
  } else {read_verilog $root/implementations/aes128_ecb_${variant}.v}
  set inst "aes128_ecb_${variant} core(.clk(clk), .rst(rst), .start(start), .key(key), .plaintext(plaintext), .ready(ready), .done(done), .ciphertext(ciphertext));"
}
set f [open adapter.v w]
puts $f "module aes_bench_top(input wire clk,rst,start, input wire \[127:0\] key,plaintext, output wire ready,done, output wire \[127:0\] ciphertext);"
puts $f $inst
puts $f {endmodule}
close $f
read_verilog adapter.v
synth_design -top aes_bench_top -part xc7z020clg484-1 -mode out_of_context
create_clock -name clk -period 10 [get_ports clk]
set_input_delay 1 -clock clk [get_ports {rst start key[*] plaintext[*]}]
set_output_delay 1 -clock clk [get_ports {ready done ciphertext[*]}]
opt_design
place_design
route_design
report_utilization -file utilization.rpt
report_timing_summary -file timing.rpt
write_checkpoint -force routed.dcp
write_verilog -force -mode funcsim netlist.v
set paths [get_timing_paths -max_paths 1 -nworst 1]
if {![llength $paths]} {error "No timing paths found"}
set f [open timing_status.txt w]
puts $f [get_property SLACK [lindex $paths 0]]
close $f
if {[get_property SLACK [lindex $paths 0]] < 0} {
  puts "WARNING: Timing failed at 100 MHz; do not interpret this as a realizable benchmark."
}
