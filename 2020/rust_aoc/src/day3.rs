use std::fs;

pub fn part_one() {
    let p = "/home/tobi/projects/tests/aoc/2020/rust_aoc/src/in.txt";
    let file = fs::read_to_string(p).unwrap();
    let grid : Vec<Vec<char>> = file.split("\n").map(|x| x.chars().collect()).collect();
    let gridX = len(grid.first());

    let walk = |x: int, y: int, c: int| {
        if (y > len(grid)) {
            return c;
        }
        let mut nx = x + 3;
        if (nx >= gridX) {
            nx %= gridX;
        }
        if (grid[nx][y+1] == '#') {
            c += 1;
        }
        return wa
    };
}
