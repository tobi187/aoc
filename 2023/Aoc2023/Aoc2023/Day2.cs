using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc2023
{
    internal class Day2
    {
        public void PartOne()
        {
            var file = File.ReadAllLines("in.txt");

            string[] cs = ["red", "green", "blue"];
            int[] am = [12, 13, 14];
            int r = 0;

            foreach  (var line in file)
            {
                var p = line.Split(":");
                var id = int.Parse(string.Concat(p.First().Where(char.IsDigit)));
                r+=id;
                var snd = p.Last().Split(" ").Select(x => Regex.Replace(x, @"\W", "")).ToArray();
                
                for (int i = 0; i < snd.Length; i++) { 
                    if (int.TryParse(snd[i], out var num))
                    {
                        var ind = Array.IndexOf(cs, snd[i+1]);
                        if (num > am[ind])
                        {
                            r-=id;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine(r);
        }

        public void PartTwo()
        {
            var file = File.ReadAllLines("in.txt");

            string[] cs = ["red", "green", "blue"];
            long r = 0;

            foreach (var line in file)
            {
                var snd = line.Split(":").Last().Split(" ").Select(x => Regex.Replace(x, @"\W", "")).ToArray();

                int[] lowest = [0,0,0];

                for (int i = 0; i < snd.Length; i++)
                {
                    if (int.TryParse(snd[i], out var num))
                    {
                        var ind = Array.IndexOf(cs, snd[i + 1]);
                        lowest[ind] = Math.Max(lowest[ind], num);
                    }
                }
                int a = 1;
                foreach (var n in lowest)
                    if (n != int.MaxValue)
                        a *= n;
                r += a;
            }

            Console.WriteLine(r);
        }
    }
}
