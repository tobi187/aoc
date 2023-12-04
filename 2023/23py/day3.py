file = open("in.txt", "r").read().splitlines()

s = set()
r = 0

def findNeighs(x: int,y: int):
    nn = [(1,1), (1,0), (0,1),(-1,1), (1,-1), (-1,0),(0,-1), (-1,-1)]

    rr = [(a+x, b+y) for a,b in nn if file[y+b][x+a].isnumeric()]

    ss =[]
    rrr = []

    for xx,yy in rr:
        start = 0
        end = len(file[yy])
        for i in range(xx, len(file[yy])):
            if not file[yy][i].isnumeric():
                end = i
                break
        for i in range(xx, -1, -1):
            if not file[yy][i].isnumeric():
                start = i + 1
                break
        
        if (start, yy) not in ss:
            ss.append((start, yy))
            rrr.append(file[yy][start:end])
    
    return ss, rrr

for i in range(len(file)):
    for j in range(len(file[0])):
        if not file[i][j].isnumeric() and file[i][j] != ".":
            ss, rr = findNeighs(j, i)
            if file[i][j] == "*" and len(rr) == 2 and len(["" for b in ss if b not in s]) == 2:
                r += int(rr[0]) * int(rr[1])
                for sssss in ss:
                    s.add(sssss)
            else:
                for ii in range(len(rr)):
                    if ss[ii] not in s:
                        # r += int(rr[ii])
                        s.add(ss[ii]) 


print(r)