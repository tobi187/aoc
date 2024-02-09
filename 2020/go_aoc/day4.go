package main

import (
	"os"
	"strings"
)

func Day1() {
	file, _ := os.ReadFile("in.txt")
	str := string(file)

	parts := strings.Split(str, "\n\n")

	println(len((parts)))
}
