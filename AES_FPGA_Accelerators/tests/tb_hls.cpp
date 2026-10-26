#include <ap_int.h>
#include <fstream>
#include <iostream>
#include <string>
void aes128_ecb_hls(ap_uint<128>, ap_uint<128>, ap_uint<128>&);
int main() {
    std::ifstream f("vectors.txt");
    if (!f) { std::cerr << "vectors.txt missing\n"; return 1; }
    std::string k,p,c;
    unsigned count=0;
    while (f >> k >> p >> c) {
        // Explicit prefixes prevent hex bytes such as 0b or 0d from being
        // mistaken for radix prefixes by the ap_uint string constructor.
        ap_uint<128> key(("0x" + k).c_str(), 16);
        ap_uint<128> plain(("0x" + p).c_str(), 16);
        ap_uint<128> expected(("0x" + c).c_str(), 16);
        ap_uint<128> actual;
        aes128_ecb_hls(key,plain,actual);
        if (actual != expected) { std::cerr << "Mismatch at " << count << '\n'; return 1; }
        ++count;
    }
    if (count != 256) return 1;
    std::cout << "PASS: " << count << " AES-128 ECB vectors\n";
    return 0;
}
