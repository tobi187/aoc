package main

import "os"

func main() {
	Day3P1()
}

func ReadFile() string {
	dt, _ := os.ReadFile("in.txt")
	return string(dt)
}
