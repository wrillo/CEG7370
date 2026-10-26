// AMD Vitis Security Library AES-128, Apache-2.0; see SOURCES.md.
// Public interface uses normal hex order (first byte in bits 127:120).
#include <ap_int.h>
#include "../vendor/vitis_security/xf_security/aes.hpp"

static ap_uint<128> reverse_bytes(ap_uint<128> x) {
#pragma HLS INLINE
    ap_uint<128> y;
    for (int i = 0; i < 16; ++i) {
#pragma HLS UNROLL
        y.range(8*i+7, 8*i) = x.range(127-8*i, 120-8*i);
    }
    return y;
}

void aes128_ecb_hls(ap_uint<128> key, ap_uint<128> plaintext,
                    ap_uint<128>& ciphertext) {
#pragma HLS INTERFACE ap_ctrl_hs port=return
#pragma HLS INTERFACE ap_none port=key
#pragma HLS INTERFACE ap_none port=plaintext
#pragma HLS INTERFACE ap_vld port=ciphertext
    xf::security::aesEnc<128> cipher;
#pragma HLS ARRAY_PARTITION variable=cipher.key_list complete dim=1
    ap_uint<128> k = reverse_bytes(key);
    ap_uint<128> result;
    // Published implementation pipelines key expansion at II=1 and unrolls
    // byte substitution and MixColumns. No unsupported throughput promise:
    // inspect the achieved schedule in the HLS synthesis report.
    cipher.updateKey(k);
    cipher.process(reverse_bytes(plaintext), k, result);
    ciphertext = reverse_bytes(result);
}
