package main

import (
	"maps"
	"os"
	"strconv"
	"strings"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

type Card struct {
	Cards string
	Rank  int
	Bid   int
}

func (c Card) getRank() {
	m := make(map[rune]int)
	for _, v := range c.Cards {
		m[v]++
	}

	switch len(m) {
	case 1:
		c.Rank = 5
	case 2:
		if 
	}
}

func day7() int {
	file, err := os.ReadFile("in.txt")
	check(err)
	lines := strings.Split(string(file), "\r\n")

	cards := make([]Card, len(lines))

	for i, l := range lines {
		p := strings.Split(l, " ")
		cards[i] = Card{
			Rank:  -1,
			Cards: p[0],
		}
		b, e := strconv.Atoi(p[1])
		check(e)
		cards[i].Rank = b
	}

	for _, c := range cards {
		c.getRank()
	}

	return 1
}
