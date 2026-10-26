# Published implementations and modifications

The five entries are encryption-only AES-128 ECB implementations, with no
padding. Each encrypts one independent 128-bit block under a 128-bit key.
There are four standalone RTL source files and one HLS source file. The HLS
file includes the locally vendored AMD header and the toolchain's HLS headers.

| File in `implementations/` | Published source | Architecture | License |
|---|---|---|---|
| `aes128_ecb_hadipour.vhd` | [Hosein Hadipour AES-VHDL](https://github.com/hadipourh/AES-VHDL) | Round-iterative, 128-bit datapath, on-the-fly keys | GPL-3.0-or-later |
| `aes128_ecb_yaaes.vhd` | [marph91 YAAES](https://github.com/marph91/yaaes) | Iterative, two-stage round datapath, on-the-fly keys | LGPL-3.0, see vendor LICENSE |
| `aes128_ecb_secworks.v` | [Secworks AES](https://github.com/secworks/aes) | Word-serial, four table S-boxes, stored round keys | BSD-2-Clause |
| `aes128_ecb_cmt.v` | [Secworks CMT branch](https://github.com/secworks/aes/tree/cmt_sbox) | Word-serial, Boolean circuit S-boxes, stored round keys | BSD-2-Clause |
| `aes128_ecb_hls.cpp` | [AMD Vitis Security Library](https://github.com/Xilinx/Vitis_Libraries/tree/main/security) | HLS AES primitive with pipelined expansion and unrolled byte/column operations | Apache-2.0 |

The two Verilog designs are **different published variants of the same core**,
not independently authored AES engines. They offer a useful table-versus-logic
S-box comparison. The CMT branch is an older revision; differences should not
be attributed solely to the S-box without examining the two full source trees.

Pinned revisions downloaded for this project:

```
hadipour/AES-VHDL  ee6c114ac4ce1ccefcb4f44099a88ca31714fd1f
marph91/yaaes      43fdc855d783c4f4ca61f8a8ad64c60a798c119c
secworks/aes       80dc4718e1dcbbdb4b0dd1bdb393d8f7b98981dc
secworks/cmt_sbox  3269e993723a6e9567f8b082acba36e4c1f7de22
Vitis_Libraries   3ab1ecd20cf338b3f8f2824fdfeee115f3398889
```

The complete RTL source repositories and licenses are retained under `vendor/`.
AMD's AES header and repository license are under `vendor/vitis_security/`.
`scripts/package_rtl.py` concatenates the RTL in dependency order and prefixes
entity/module names to prevent collisions. YAAES library references are changed
to `work`. No cryptographic datapath algorithms are rewritten. New wrappers
normalize byte ordering, reset handling, and one-block start/ready/done control.
Wrapper material in GPL/LGPL-derived bundles is supplied under the corresponding
upstream terms. New standalone scripts/testbenches/HLS wrapper are supplied
under MIT terms in `LICENSE`.

Hadipour and AMD primitives use the opposite byte packing to the public wrapper;
their adapters reverse bytes at entry/exit. YAAES uses its published matrix
conversion functions. YAAES has no native reset; the wrapper drains outstanding
work for 32 clocks after reset before asserting ready. Encryption datapaths are
fixed to AES-128; Secworks decryption is tied off for synthesis removal.

Reference test cases: [FIPS 197](https://doi.org/10.6028/NIST.FIPS.197-upd1)
and [NIST SP 800-38A](https://doi.org/10.6028/NIST.SP.800-38A).
Remaining expected outputs are generated independently with .NET AES, rather
than any of the five candidate implementations.
