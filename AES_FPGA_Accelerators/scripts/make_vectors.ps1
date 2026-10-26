$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
New-Item -ItemType Directory -Force "$root/tests" | Out-Null
function Bytes([string]$s) {
    [byte[]]$b = for ($i=0; $i -lt $s.Length; $i+=2) { [Convert]::ToByte($s.Substring($i,2),16) }
    return ,$b
}
function Hex([byte[]]$b) { return (($b | ForEach-Object { $_.ToString('x2') }) -join '') }
$aes = [System.Security.Cryptography.Aes]::Create()
$aes.Mode = [System.Security.Cryptography.CipherMode]::ECB
$aes.Padding = [System.Security.Cryptography.PaddingMode]::None
$rows = [System.Collections.Generic.List[string]]::new()
# FIPS-197, all-zero, and NIST SP 800-38A ECB examples.
$known = @(
 '000102030405060708090a0b0c0d0e0f 00112233445566778899aabbccddeeff 69c4e0d86a7b0430d8cdb78070b4c55a',
 '00000000000000000000000000000000 00000000000000000000000000000000 66e94bd4ef8a2c3b884cfa59ca342b2e',
 '2b7e151628aed2a6abf7158809cf4f3c 6bc1bee22e409f96e93d7e117393172a 3ad77bb40d7a3660a89ecaf32466ef97',
 '2b7e151628aed2a6abf7158809cf4f3c ae2d8a571e03ac9c9eb76fac45af8e51 f5d3d58503b9699de785895a96fdbaaf',
 '2b7e151628aed2a6abf7158809cf4f3c 30c81c46a35ce411e5fbc1191a0a52ef 43b1cd7f598ece23881b00e3ed030688',
 '2b7e151628aed2a6abf7158809cf4f3c f69f2445df4f9b17ad2b417be66c3710 7b0c785e27e8ad3f8223207104725dd4'
)
foreach ($row in $known) {
    $parts = $row.Split(' '); $aes.Key = Bytes $parts[0]
    $enc = $aes.CreateEncryptor()
    $actual = Hex ($enc.TransformFinalBlock((Bytes $parts[1]),0,16)); $enc.Dispose()
    if ($actual -ne $parts[2]) { throw 'Independent AES reference failed known-answer test' }
    $rows.Add($row)
}
$random = [System.Random]::new(128)
for ($i=0; $i -lt 250; $i++) {
    $k = [byte[]]::new(16); $p = [byte[]]::new(16)
    $random.NextBytes($k); $random.NextBytes($p)
    # Half fixed-key data, half key changes. Every implementation still
    # expands its key per transaction, including the fixed-key subset.
    if ($i -lt 125) { $k = Bytes '2b7e151628aed2a6abf7158809cf4f3c' }
    $aes.Key = $k; $enc = $aes.CreateEncryptor()
    $c = $enc.TransformFinalBlock($p,0,16); $enc.Dispose()
    $rows.Add("$(Hex $k) $(Hex $p) $(Hex $c)")
}
$aes.Dispose()
[IO.File]::WriteAllLines("$root/tests/vectors.txt", $rows, [Text.Encoding]::ASCII)
Write-Host "Created $($rows.Count) vectors using .NET AES; six published answers verified."
