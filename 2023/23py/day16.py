from enum import Enum

class Direction(Enum):
    RIGHT = 1
    UP = 2
    DOWN = 3
    LEFT = 4


class Point:
    def __init__(self, xx, yy, el) -> None:
        self.x: int = xx
        self.y: int = yy
        self.sign: str = el



    def came_from(self, old) -> Direction:
        if self.x == old.x:
            if old.y > self.y:
                return Direction.UP
            else:
                return Direction.DOWN
        else:
            if old.x > self.x:
                return Direction.LEFT
            else:
                return Direction.RIGHT


class TileBase:
    def __init__(self, pt) -> None:
        self.me: Point = pt
        self.energized = False
        self.history = []

    def clear_history(self):
        self.history = []
        self.energized = False

    def rundlauf(self, last: Point, direction: Direction):
        contained = (last.x, last.y, direction) in self.history
        if not contained:
            self.history.append((last.x, last.y, direction))
        return contained

    def move(self, last: Point):
        self.energized = True
        direction = self.me.came_from(last)
        r = None
        if self.rundlauf(last, direction):
            return []
        match direction:
            case Direction.RIGHT:
                r = self.me.x + 1, self.me.y
            case Direction.LEFT:
                r = self.me.x - 1, self.me.y
            case Direction.UP:
                r = self.me.x, self.me.y - 1
            case Direction.DOWN:
                r = self.me.x, self.me.y + 1

        return [r]
        
class Mirror(TileBase):
    def __init__(self, pt) -> None:
        super().__init__(pt)


    def move(self, last: Point):
        self.energized = True
        direction = self.me.came_from(last)
        if self.rundlauf(last, direction):
            return []
        r = None
        match direction:
            case Direction.RIGHT:
                if self.me.sign == "/":
                    r = self.me.x, self.me.y - 1
                else:
                    r = self.me.x, self.me.y + 1
            case Direction.LEFT:
                if self.me.sign == "/":
                    r = self.me.x, self.me.y + 1
                else:
                    r = self.me.x, self.me.y - 1
            case Direction.UP:
                if self.me.sign == "/":
                    r = self.me.x + 1, self.me.y
                else:
                    r = self.me.x - 1, self.me.y
            case Direction.DOWN:
                if self.me.sign == "/":
                    r = self.me.x - 1, self.me.y
                else:
                    r = self.me.x + 1, self.me.y

        return [r]

class Splitter(TileBase):
    def __init__(self, pt) -> None:
        super().__init__(pt)


    def move(self, last: Point):
        self.energized = True
        direction = self.me.came_from(last)
        r = []
        if self.rundlauf(last, direction):
            return []
        match direction:
            case Direction.RIGHT:
                if self.me.sign == "|":
                    r.append((self.me.x, self.me.y + 1))
                    r.append((self.me.x, self.me.y - 1))
                else:
                    r.append((self.me.x + 1, self.me.y))
            case Direction.LEFT:
                if self.me.sign == "|":
                    r.append((self.me.x, self.me.y + 1))
                    r.append((self.me.x, self.me.y - 1))
                else:
                    r.append((self.me.x - 1, self.me.y))
            case Direction.UP:
                if self.me.sign == "-":
                    r.append((self.me.x + 1, self.me.y))
                    r.append((self.me.x - 1, self.me.y))
                else:
                    r.append((self.me.x, self.me.y - 1))
            case Direction.DOWN:
                if self.me.sign == "-":
                    r.append((self.me.x + 1, self.me.y))
                    r.append((self.me.x - 1, self.me.y))
                else:
                    r.append((self.me.x, self.me.y + 1))
        
        return r

def task(prev_x, prev_y, start_x, start_y, points: list[list[TileBase | Splitter | Mirror]]):
    # file = list(map(list, open("in.txt", "r").read().splitlines()))

    # points : list[list[TileBase | Mirror | Splitter]] = []

    X = len(points[0])
    Y = len(points)

    # for y, row in enumerate(file):
    #     nr = []
    #     for x, sign in enumerate(row):
    #         p = Point(x, y, sign)
    #         tt = None
    #         match sign:
    #             case '.':
    #                 tt = TileBase(p)
    #             case '/' | '\\':
    #                 tt = Mirror(p)
    #             case '|' | '-':
    #                 tt = Splitter(p)
    #         nr.append(tt)

    #     points.append(nr) 

    curr_points = [(Point(prev_x, prev_y, "."), points[start_y][start_x])]
    s = 0
    for _ in range(10000):
        stays = []
        for old, new in curr_points:
            l = new.move(old)
            for new_x, new_y in l:
                if 0 <= new_x < X and 0 <= new_y < Y:
                    stays.append((new.me, points[new_y][new_x]))
        s+=1
        curr_points = stays


    # result = set()
    sss = 0
    for row in points:
        for p in row:
            if p.energized:
                sss += 1

    [[a.clear_history() for a in b] for b in points]
    return sss



file = list(map(list, open("in.txt", "r").read().splitlines()))
yy = len(file)
xx = len(file[0])

file = list(map(list, open("in.txt", "r").read().splitlines()))

points : list[list[TileBase | Mirror | Splitter]] = []

for y, row in enumerate(file):
    nr = []
    for x, sign in enumerate(row):
        p = Point(x, y, sign)
        tt = None
        match sign:
            case '.':
                tt = TileBase(p)
            case '/' | '\\':
                tt = Mirror(p)
            case '|' | '-':
                tt = Splitter(p)
        nr.append(tt)

    points.append(nr) 

starts = []

for i in range(yy):
    starts.append((-1, i, 0, i))
    starts.append((xx, i, xx-1, i))
    
for i in range(xx):
    starts.append((i, -1, i, 0))
    starts.append((i, yy, i, yy-1))



# res = pool.starmap(task, [(a, b, points) for a, b in starts[:2]])

ab = max([task(a, b, c, d, points) for a, b, c, d in starts])
print(ab)


# s = set()
# for ss in res:
#     s.update(ss)

# print(len(ss))
    