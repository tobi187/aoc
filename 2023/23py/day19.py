import re
import json

class Workflow:
    def __init__(self, n, d, r) -> None:
        self.name = n
        self.calcs = d
        self.results = r

    @classmethod
    def create(cls, line: str):
        parts = line[:-1].split("{")
        p = []
        r = []
        for pt in parts[1].split(","):
            clc = pt.split(':')
            if len(clc) == 1:
                p.append('')
                r.append(clc[0])
            else:
                p.append(clc[0])
                r.append(clc[1])

        return Workflow(parts[0], p, r)
        
    def get_next(self, vals: dict[str, int]):
        for ri, item in enumerate(self.calcs):
            if not item:
                return self.results[ri]
            else:
                for k, v in vals.items():
                    if not k in item:
                        continue
                    if eval(item.replace(k, str(v))):
                        return self.results[ri]
                    else:
                        break


file = open('in.txt', 'r').read().split('\n\n')
print(file)
workflows: dict[str, Workflow] = {}
 
for line in file[0].splitlines():
    wf = Workflow.create(line)
    workflows[wf.name] = wf

res = []

for line in file[1].splitlines():
    sub = re.sub('([a-z])', r'"\1"', line.replace('=', ':'))
    dt = json.loads(sub)

    rr = ""
    wf = workflows['in']
    while True:
        nxt = wf.get_next(dt)
        if nxt == 'A':
            res.append(dt)
            break
        elif nxt == 'R':
            break
        wf = workflows[nxt]

print(sum([sum(a.values()) for a in res]))
