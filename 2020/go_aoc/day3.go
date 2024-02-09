package main

import "strings"

type point struct {
	x int
	y int
}

type field struct {
	p     point
	grid  [][]rune
	stepx int
	stepy int
	yMax  int
	xMax  int
}

func (f *field) getTree() int {
	if f.grid[f.p.y][f.p.x] == '#' {
		return 1
	}
	return 0
}

func (f *field) traverse(trees int) int {
	f.p.x += f.stepx
	f.p.y += f.stepy
	if f.p.x >= f.xMax {
		f.p.x -= f.xMax
	}
	if f.p.y >= f.yMax {
		return trees
	}
	trees += f.getTree()
	return f.traverse(trees)
}

func Day3P1() {
	file := strings.Split(ReadFile(), "\n")
	grid := make([][]rune, len(file))
	for i, line := range file {
		row := make([]rune, len(file[i]))
		for j, char := range line {
			row[j] = char
		}
		grid[i] = row
	}

	start := point{0, 0}
	arr := [5]point{{1, 1}, {3, 1}, {5, 1}, {7, 1}, {1, 2}}
	ct := 1
	for _, r := range arr {
		field := field{start, grid, r.x, r.y, len(grid), len(grid[0])}
		ct *= field.traverse(0)
	}

	println(ct)
}
