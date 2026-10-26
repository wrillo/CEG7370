# AES-128 ECB on ZedBoard: five implementations

Target: **Zynq-7000 xc7z020clg484-1**, 100 MHz (10 ns), Vivado/Vitis 2026.1.
Two VHDL, two Verilog, one HLS C++ implementation are in `implementations/`.
See [SOURCES.md](SOURCES.md) for architecture differences, published sources,
exact revisions, licenses, and wrapper modifications.

**Status (October 26, 2026):** the user completed the full HLS configuration-file
flow: C simulation passed 256 vectors, HLS synthesis completed, Verilog C/RTL
co-simulation passed 256 vectors, and Vivado IP packaging completed. Hadipour
also completed step 4: all verification and benchmark outputs matched, active
and idle power reports were generated, and the summary CSV was written.
YAAES, Secworks, CMT, and HLS still need their step 4 benchmark runs.
HLS estimated a 10.204 ns clock period against the 10 ns
target; this is not a post-route timing result or confirmed 100 MHz closure.
The installation was located at `C:\AMDDesignTools\2026.1`.

## 1. Open PowerShell

```powershell
Set-Location 'drive:\directory\path\can\not\have\spaces\AES_FPGA_Accelerators'
$tools = 'C:\AMDDesignTools\2026.1'
```

Vivado must have Zynq-7000 device support installed, and Vitis must include HLS.
Python 3 is used only for packaging/report summaries. All build outputs stay
under this folder. No command programs the board or generates a bitstream.

## 2. Generate independent test vectors

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\make_vectors.ps1
```

Creates `tests/vectors.txt` with 256 key/plaintext/expected-ciphertext triples:
six published known answers, 125 pseudorandom blocks under a fixed key, and
125 pseudorandom key/block pairs. The generator verifies the known answers
against .NET AES first. It uses ECB with no padding. Byte order throughout the
public interfaces is conventional hexadecimal: first byte in bits 127:120.

## 3. Build and verify the HLS implementation

### C simulation only

The tested installation omitted the design source in the legacy Tcl flow.
The configuration-file flow compiled both C++ files and passed all 256 vectors.
Do not rerun the old `--tcl scripts/run_hls.tcl` command.
For a quick C-only check, use this command from the project root. It is optional
when running the full build, which already includes C simulation:

```powershell
& "$tools\Vitis\bin\vitis-run.bat" --mode hls --csim `
    --config .\scripts\hls_csim.cfg `
    --work_dir .\build\hls_config_check
```

The config uses paths relative to `scripts/hls_csim.cfg`, so it can be copied
with the project. The testbench now prefixes input strings with `0x` to avoid
misparsing hexadecimal values beginning with `0b` or `0d`. This corrects the
observed mismatch at vector 44. The user's rerun confirmed
`PASS: 256 AES-128 ECB vectors` and `CSim done with 0 errors`.

This command runs C simulation only. It does not produce the synthesized RTL
required by the power benchmark. Use the full configuration-file flow below
to produce and verify the hardware.

### Full configuration-file build

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\run_hls.ps1 -ToolsRoot $tools
```

This runs C simulation, HLS synthesis, C/RTL co-simulation, and IP export, in
that order, stopping if any step fails. It uses `scripts/hls_build.cfg`.
Outputs are in `build/hls_component/hls/`; inspect synthesis reports in
`syn/report/`, co-simulation reports in `sim/report/`, and the exported IP under
`impl/ip/`. The power benchmark reads the exported RTL from this component.
The user completed this flow successfully. The log reported
`C/RTL co-simulation finished: PASS`, created
`impl/ip/xilinx_com_hls_aes128_ecb_hls_1_0.zip`, and ended with
`HLS flow completed`. Exported Verilog is in `impl/ip/hdl/verilog/`.
These results verify the HLS test cases and packaging, not routed timing or power.
There is no need to repeat step 3 before step 4 unless the design, configuration,
or test inputs change, or the generated component is missing.

Optimization pragmas include complete partitioning of round keys and unrolled
byte conversion in the wrapper. The published primitive additionally pipelines
key expansion at II=1 and unrolls SubBytes and MixColumns. These are optimization
requests; the achieved latency/II and resource use come from the HLS reports.
The top is `ap_ctrl_hs`, with parallel data ports, to match the RTL benchmark.
It is an IP-flow HLS component, not an Alveo/XRT `.xclbin` kernel.

## 4. Run the five power benchmarks

Run from the project root in the same folder where step 3 succeeded.
Keep `build/hls_component/` in place: it contains the packaged HLS RTL needed by
this step. No connected ZedBoard is required for these simulation-based estimates.

```powershell
$tools = 'C:\AMDDesignTools\2026.1'
powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\run_benchmark.ps1 -Variant all -ToolsRoot $tools
```

The candidates run sequentially: Hadipour, YAAES, Secworks, CMT, then HLS.
The script stops on a tool failure. After fixing a failure, you can rerun that
candidate and any remaining candidates individually using the commands below;
the summary collects available completed results. Check that all five rows
are present before comparing results. 

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\run_benchmark.ps1 -Variant hadipour -SkipImplementation -ToolsRoot $tools
```

This recompiles and reruns simulation, then estimates power. Use
`-SkipImplementation` only for a single candidate with an existing checkpoint,
netlist, and timing status, and only when its hardware sources and implementation
constraints have not changed.

Or run one at a time (the four RTL designs do not require step 3):

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\run_benchmark.ps1 -Variant hadipour -ToolsRoot $tools
powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\run_benchmark.ps1 -Variant yaaes -ToolsRoot $tools
powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\run_benchmark.ps1 -Variant secworks -ToolsRoot $tools
powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\run_benchmark.ps1 -Variant cmt -ToolsRoot $tools
powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\run_benchmark.ps1 -Variant hls -ToolsRoot $tools
```

Each run synthesizes, places, and routes the candidate out of context, exports
a functional implementation netlist, and runs the shared SystemVerilog
`tests/tb_power.sv` testbench with XSim. The testbench first verifies all 256
vectors, then checks another 1,024 transactions while recording active activity,
then measures 10,000 idle clocks. It fails on wrong ciphertext or timeout.
`$stop` calls mark the SAIF measurement windows; the Tcl script resumes each
window automatically. Those stops are intentional.

Vivado then annotates the routed design with the active and idle SAIF files and
writes power estimates with identical operating conditions (25 C ambient,
typical process). Key expansion is included **on every transaction** for all
five candidates. There is one outstanding block at a time; this benchmarks
single-request operation, not maximum pipelined streaming throughput.

## 5. Read and compare results

Open `build/summary.csv`. Individual reports are under `build/<variant>/`,
except HLS benchmark reports, which are in `build/hls_benchmark/`:

| File | Meaning |
|---|---|
| `active_power.rpt`, `idle_power.rpt` | Estimated power breakdown and confidence |
| `active_saif_mapping.rpt`, `idle_saif_mapping.rpt` | Activity annotation matching; check for missing/unmatched nets |
| `timing.rpt`, `timing_status.txt` | Timing summary and worst setup slack at 100 MHz |
| `utilization.rpt` | LUTs, registers, RAMs, DSPs |
| `metrics.csv` | Actual completed blocks and measurement-window durations |
| `active.saif`, `idle.saif` | Recorded switching activity |
| `xsim.log` | Test pass/failure diagnostics |

Summary formulas:

* Throughput (Mbps) = `128 × blocks × 1000 / active_ns`.
* Total energy (nJ/block) = `active_total_W × active_ns / blocks`.
* Incremental energy (nJ/block) = `(active_total_W − idle_total_W) × active_ns / blocks`.

Compare energy/block as well as watts: a slower engine can use fewer watts yet
more energy to complete the same work. Incremental energy subtracts the idle
baseline; it is not a substitute for total energy. Report rounding can make
small differences unreliable. The summary includes completed runs only; check
that all five appear. If `timing_met` is false, reduce the common clock frequency
for **all** candidates before treating their throughput as physically realizable.
Adjust the clock consistently in `hls_build.cfg`, `hls_csim.cfg`, `implement.tcl`, and
`tb_power.sv` (including the metrics clock field), then rerun.

These are **core-only, out-of-context power estimates**, not measured ZedBoard
wall/rail power. They exclude PS software, DDR traffic, AXI fabric, package I/O,
and board regulators. Device static power may still be included in the tool's
total. Functional post-route SAIF supplies mapped activity but not SDF timing
glitches. Inspect annotation coverage and report confidence before trusting a
ranking; use timing simulation or physical rail measurements for greater
accuracy. Idle behavior is deliberately preserved, including internal activity
in cores without clock gating. No power ranking is assumed in advance.

## Interfaces and use in a Vitis application

Each RTL top is named after its filename and exposes `clk`, active-high `rst`,
`start`, 128-bit `key` and `plaintext`, `ready`, one-cycle `done`, and 128-bit
`ciphertext`. Assert start for one cycle only while ready is high. Inputs are
captured by the wrapper; ciphertext is registered when done asserts. YAAES
waits 32 cycles after reset to drain its resetless internal pipeline. HLS uses
the equivalent generated `ap_*` control ports; the benchmark supplies an adapter.

For this comparison you run the RTL tools through Vivado, even though your
software development environment is Vitis. To call an accelerator from ZedBoard
ARM software later, package it as Vivado IP, add an AXI4-Lite register wrapper
(or AXI Stream plus DMA), connect it to Zynq PS in a block design, generate a
bitstream and export an XSA, then create a Vitis platform/application from that
XSA. This package supplies the AES cores and core power benchmark; it does not
include an AXI host peripheral, board bitstream, or ARM application. Such system
components would change the scope of the power comparison.
