`timescale 1ns/1ps
module tb_power;
  reg clk=0, rst=1, start=0;
  reg [127:0] key=0, plaintext=0;
  wire ready, done;
  wire [127:0] ciphertext;
  aes_bench_top dut(.*);
  always #5 clk=~clk;
  reg [127:0] keys[0:255], plains[0:255], answers[0:255];
  integer f, status, metrics, i, j, cycles;
  real t0, active_ns, idle_ns;
  task automatic tick;
    @(posedge clk); #1;
  endtask
  task automatic transaction(input integer n);
    cycles=0;
    while (ready !== 1'b1) begin
      tick(); cycles++;
      if(cycles>10000) $fatal(1,"ready timeout");
    end
    @(negedge clk); key=keys[n]; plaintext=plains[n]; start=1;
    tick();
    @(negedge clk); start=0;
    cycles=0;
    while (done !== 1'b1) begin
      tick(); cycles++;
      if(cycles>10000) $fatal(1,"done timeout at vector %0d",n);
    end
    if(ciphertext !== answers[n])
      $fatal(1,"AES mismatch vector %0d: got %032h expected %032h",n,ciphertext,answers[n]);
  endtask
  initial begin
    f=$fopen("vectors.txt","r");
    if(!f) $fatal(1,"vectors.txt missing");
    for(i=0;i<256;i++) begin
      status=$fscanf(f,"%h %h %h",keys[i],plains[i],answers[i]);
      if(status!=3) $fatal(1,"Malformed vector %0d",i);
    end
    $fclose(f);
    // Allow the global FPGA startup reset to settle for netlist simulation.
    repeat(30) tick();
    @(negedge clk); rst=0;
    for(i=0;i<256;i++) transaction(i);
    // First stop: Tcl opens active.saif after warm-up/verification.
    @(negedge clk); $display("BEGIN_ACTIVE"); $stop;
    t0=$realtime;
    for(j=0;j<4;j++) for(i=0;i<256;i++) transaction(i);
    @(negedge clk); active_ns=$realtime-t0;
    $display("END_ACTIVE_BEGIN_IDLE"); $stop;
    // No reset or clock gating: measure the actual quiescent design behavior.
    t0=$realtime;
    repeat(10000) tick();
    @(negedge clk); idle_ns=$realtime-t0;
    $display("END_IDLE"); $stop;
    metrics=$fopen("metrics.csv","w");
    $fdisplay(metrics,"blocks,active_ns,idle_ns,clock_ns");
    $fdisplay(metrics,"1024,%0.3f,%0.3f,10",active_ns,idle_ns);
    $fclose(metrics);
    $display("PASS: all verification and benchmark outputs matched");
    $finish;
  end
  initial begin #200000000; $fatal(1,"global timeout"); end
endmodule
