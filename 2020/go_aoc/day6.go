package main

import (
	"strings"
	"unicode"
)

func Day6() {
	file := ReadFile()
	parts := strings.Split(file, "\n\n")

	s := 0

	for _, e := range parts {
		mp := make(map[rune]int, len(parts)*5)
		for _, c := range e {
			if _, ok := mp[c]; ok && unicode.IsLetter(c) {
				mp[c] = 0
				s++
			}
		}
	}
	println(s)
}
