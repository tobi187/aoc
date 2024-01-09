using Aoc2023;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Aoc2023
{
    internal class Day4
    {
        static void Main()
        {
            string[] input = File.ReadAllLines("input.txt");
            int totalCommonElements = 0;
            int points = 0;
            int[] output = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                char[] Kennzeichen = { '|', ':' };
                string[] subs = input[i].Split(Kennzeichen);
                string[] winningNumbers = subs[1].Trim().Split(' ');
                string[] numberUhave = subs[2].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine("ausgabe = " + subs[0]);

                var commonElements = winningNumbers.Intersect(numberUhave);
                int commonCount = commonElements.Count();

                totalCommonElements += commonCount;
                Console.WriteLine("Common elements: " + string.Join(", ", commonElements));
                Console.WriteLine("Number of common elements: " + commonCount);
                output[i] = commonCount;

            }


            for (int i = input.Length - 1; i >= 0; i--)
            {
                int sum = 1;
                int anzahl = output[i];
                for (int j = 0; j < anzahl; j++)
                    sum += output[i + j + 1];
                output[i] = sum;

            }


            int ergebnis = 0;
            for (int i = input.Length - 1; i >= 0; i--)
                ergebnis += output[i];

            Console.WriteLine("ergebnis" + ergebnis);

            Console.WriteLine("Total number of common elements: " + totalCommonElements);
        }

        public void PartOne()
        {
            var file = File.ReadAllLines("in.txt");
            var solutions = new List<int[]>();
            var tries = new List<int[]>();
            
            foreach (var line in file)
            {
                var parts = line.Split(":")[1].Split("|");
                var solLine = parts[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                solutions.Add(solLine.Select(int.Parse).ToArray());
                var tryLine = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                tries.Add(tryLine.Select(int.Parse).ToArray());
            }

            int sum = 0;

            for (int i = 0; i < tries.Count; i++)
            {
                int lineMultiplikator = 0;
                int zs = sum;
                foreach (var num in tries[i])
                {
                    if (solutions[i].Contains(num))
                    {
                        if (lineMultiplikator == 0) lineMultiplikator++;
                        else lineMultiplikator *= 2;
                    }
                }
                sum += lineMultiplikator;
                Console.WriteLine($"Line {i+1}: {sum - zs}");
            }
            Console.WriteLine(sum);
        }

        public void PartTwo()
        {
            var file = File.ReadAllLines("in.txt");
            var solutions = new List<int[]>();
            var tries = new List<int[]>();

            foreach (var line in file)
            {
                var parts = line.Split(":")[1].Split("|");
                var solLine = parts[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                solutions.Add(solLine.Select(int.Parse).ToArray());
                var tryLine = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                tries.Add(tryLine.Select(int.Parse).ToArray());
            }

            var scratchCount = new int[tries.Count];
            Array.Fill(scratchCount, 1);

            for (int i = 0; i < tries.Count; i++)
            {
                int s = 0;
                foreach (var num in tries[i])
                    if (solutions[i].Contains(num))
                        s++;
                
                for (int c = 1; c <= s; c++)
                    scratchCount[c + i] += scratchCount[i];
            }
            Console.WriteLine(string.Join("\n", scratchCount));
            Console.WriteLine(scratchCount.Sum());
        }
    }
}

//Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
//Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
//Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
//Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
//Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
//Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11

// 1 for the first match, then doubled three times for each of the three matches after the first





