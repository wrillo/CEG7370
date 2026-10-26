"""Summarize actual Vivado reports only; never substitute assumed power."""
from pathlib import Path
import csv
import re

ROOT = Path(__file__).resolve().parents[1]

def power(path, label):
    text = path.read_text(errors='replace')
    m = re.search(r'\|\s*' + re.escape(label) + r'\s*\|\s*([0-9.]+)\s*\|', text)
    if not m:
        raise ValueError(f'{label!r} missing from {path}; inspect report format')
    return float(m[1])

rows = []
for name in ['hadipour', 'yaaes', 'secworks', 'cmt', 'hls']:
    folder = ROOT / 'build' / ('hls_benchmark' if name == 'hls' else name)
    needed = ['metrics.csv', 'active_power.rpt', 'idle_power.rpt', 'timing_status.txt']
    if not all((folder / f).exists() for f in needed):
        continue
    with (folder / 'metrics.csv').open() as f:
        metrics = next(csv.DictReader(f))
    active = power(folder / 'active_power.rpt', 'Total On-Chip Power (W)')
    idle = power(folder / 'idle_power.rpt', 'Total On-Chip Power (W)')
    blocks, ns = int(metrics['blocks']), float(metrics['active_ns'])
    slack = float((folder / 'timing_status.txt').read_text().strip())
    rows.append(dict(implementation=name, blocks=blocks, active_ns=ns,
        timing_slack_ns=slack, timing_met=slack >= 0,
        active_total_W=active, idle_total_W=idle,
        throughput_Mbps=blocks*128*1000/ns,
        total_nJ_per_block=active*ns/blocks,
        incremental_nJ_per_block=(active-idle)*ns/blocks,
        estimate='post-route functional SAIF; inspect annotation coverage'))
if not rows:
    raise SystemExit('No completed benchmark reports found. Run run_benchmark.ps1 first.')
out = ROOT / 'build' / 'summary.csv'
with out.open('w', newline='') as f:
    writer = csv.DictWriter(f, fieldnames=rows[0].keys())
    writer.writeheader(); writer.writerows(rows)
print(out)
for row in rows:
    print(f"{row['implementation']}: {row['active_total_W']:.4f} W, "
          f"{row['total_nJ_per_block']:.3f} nJ/block, timing_met={row['timing_met']}")
