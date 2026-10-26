open_checkpoint routed.dcp
# Identical environment across all designs. Report core-only OOC estimates.
set_operating_conditions -ambient_temp 25 -process typical
foreach mode {active idle} {
  reset_switching_activity -all
  read_saif -strip_path tb_power/dut -out_file ${mode}_saif_mapping.rpt ${mode}.saif
  report_power -file ${mode}_power.rpt
}
