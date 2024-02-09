mod day3;
use std::fs;

fn main() {
    let d = try_one();
    day3::part_one();
}

fn try_one() -> i64 {
    let p = "/home/tobi/projects/tests/aoc/2020/rust_aoc/src/in.txt";
    let contents = fs::read_to_string(p);
    let lines = contents.unwrap();
    let nextL = lines.lines();
    let ints = nextL
        .map(|x| x.parse::<i32>().unwrap())
        .collect::<Vec<i32>>();
    let count = ints.len();

    for i in 0..count {
        let i2 = i + 1;
        for j in i2..count {
            let i3 = i2 + 1;
            for k in i3..count {
                if ints[i] + ints[j] + ints[k] == 2020 {
                    return 0;
                }
            }
        }
    }

    return -1;
}
