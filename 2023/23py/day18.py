file = [a.split() for a in open("in.txt", "r").read().splitlines()]

steps = [(a[0], int(a[1])) for a in file]

min_x = 10000
min_y = 10000
max_x = -10000
max_y = -10000

cpt = 0, 0
pts = []

for direction, amount in steps:
    match direction:
        case "U":
            pts.extend([(cpt[0], cpt[1]-a) for a in range(1, amount+1)])
        case "D":
            pts.extend([(cpt[0], cpt[1]+a) for a in range(1, amount+1)])
        case "R":
            pts.extend([(cpt[0]+a, cpt[1]) for a in range(1, amount+1)])
        case "L":
            pts.extend([(cpt[0]-a, cpt[1]) for a in range(1, amount+1)])
    cpt = pts[-1]

for xx,yy in pts:
    max_x = max(max_x, xx)
    min_x = min(min_x, xx)
    min_y = min(min_y, yy)
    max_y = max(max_y, yy)

s = 0

for yy in range(min_y, max_y +1):
    start = None
    for xx in range(min_x, max_x+1):
        in_pts = (xx, yy) in pts
        if start and in_pts and (diff := xx - start) > 1:
            s += diff + 1
            start = None
        elif in_pts:
            start = xx
            s += 1

print(s)