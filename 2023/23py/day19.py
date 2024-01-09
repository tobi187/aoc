

class Workflow:
    def __init__(self, n, d, r) -> None:
        self.name = n
        self.calcs = d
        self.results = r

    @classmethod
    def create(cls, line: str):
        parts = line[:-1].split("{")
        r = []
        p = []
        for pt in line[1].split(","):
            

        return Workflow()
    