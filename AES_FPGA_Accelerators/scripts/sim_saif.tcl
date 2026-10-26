run all
open_saif active.saif
log_saif [get_objects -r /tb_power/dut/*]
run all
close_saif
open_saif idle.saif
log_saif [get_objects -r /tb_power/dut/*]
run all
close_saif
run all
quit
