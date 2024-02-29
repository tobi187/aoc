package main

import "os"

func main() {
	Day6()
}

func ReadFile() string {
	dt, _ := os.ReadFile("in.txt")
	return string(dt)
}
