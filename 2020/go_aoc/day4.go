package main

import (
	"os"
	"regexp"
	"slices"
	"strconv"
	"strings"
)

type Passport struct {
	byr string
	iyr string
	eyr string
	hgt string
	hcl string
	ecl string
	pid string
	cid string
}

func parsePassPort(line string) Passport {
	fixedLine := strings.Split(strings.ReplaceAll(line, "\n", " "), " ")
	pw := &Passport{}
	for _, fLine := range fixedLine {
		if len(fLine) > 3 {
			parts := strings.Split(fLine, ":")
			switch parts[0] {
			case "byr":
				pw.byr = parts[1]
			case "iyr":
				pw.iyr = parts[1]
			case "eyr":
				pw.eyr = parts[1]
			case "hgt":
				pw.hgt = parts[1]
			case "hcl":
				pw.hcl = parts[1]
			case "ecl":
				pw.ecl = parts[1]
			case "pid":
				pw.pid = parts[1]
			case "cid":
				pw.cid = parts[1]
			}
		}
	}

	return *pw
}

func isValid(p Passport) bool {
	switch "" {
	case p.byr:
		return false
	case p.iyr:
		return false
	case p.eyr:
		return false
	case p.hgt:
		return false
	case p.hcl:
		return false
	case p.ecl:
		return false
	case p.pid:
		return false
	}
	byr, err := strconv.Atoi(p.byr)
	if err != nil || byr < 1920 || byr > 2002 {
		return false
	}
	yir, err := strconv.Atoi(p.iyr)
	if err != nil || yir < 2010 || byr > 2020 {
		return false
	}
	eyr, err := strconv.Atoi(p.eyr)
	if err != nil || eyr < 2020 || byr > 2030 {
		return false
	}
	hgt, err := strconv.Atoi(strings.ReplaceAll(strings.ReplaceAll(p.hgt, "cm", ""), "in", ""))
	if err != nil {
		return false
	}
	m3, err := regexp.MatchString("\\d{2,3}(cm|in)", p.hgt)
	if err != nil || !m3 {
		return false
	}
	if strings.HasSuffix(p.hgt, "cm") && (hgt < 150 || hgt > 193) {
		return false
	}
	if strings.HasSuffix(p.hgt, "in") && (hgt < 59 || hgt > 76) {
		return false
	}
	if !slices.Contains([]string{"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}, p.ecl) {
		return false
	}
	m, err := regexp.MatchString("#[0-9a-f]{6}", p.hcl)
	if err != nil || !m {
		return false
	}
	m2, err := regexp.MatchString("[0-9]{9}", p.pid)
	if err != nil || !m2 {
		return false
	}
	return true
}

func PartOne() {
	file, _ := os.ReadFile("in.txt")
	str := string(file)

	parts := strings.Split(str, "\n\n")

	slice := make([]Passport, len(parts))
	for i, p := range parts {
		pp := parsePassPort(p)
		slice[i] = pp
	}
	count := 0
	for _, p := range slice {
		if isValid(p) {
			count++
		}
	}

	println(count)
}
