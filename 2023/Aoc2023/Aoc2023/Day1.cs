using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    internal class Day1
    {
        public void PartOne()
        {
            Console.WriteLine(File.ReadAllLines("in.txt").Select(TryCollectFirstAndLastNum).Sum());
        }

        public void PartTwoNewNew()
        {
            var f = File.ReadAllLines("in.txt");
            string[] nums = "one, two, three, four, five, six, seven, eight, nine".Split(", ");
            var s = 0;

            foreach (var line in f)
            {
                var l = new List<(int, string)>();
                for (var i = 0; i < line.Length; i++)
                {
                    if (char.IsDigit(line[i]))
                        l.Add((i, line[i].ToString()));
                    foreach (var n in nums)
                    {
                        var ind = Array.IndexOf(line.ToArray(), n, i);
                        if (ind > -1)
                        {
                            l.Add((i, (Array.IndexOf(nums, n) + 1).ToString()));
                        }
                    }
                }
                var ord = l.OrderBy(x => x.Item1).ToArray();
                s += int.Parse($"{l.First().Item2}{l.Last().Item2}");
            }
            Console.WriteLine(s);
        }

        public void PartTwoNew()
        {
            int s = 0;
            var f = File.ReadAllLines("in.txt");
            string[] nums = "one, two, three, four, five, six, seven, eight, nine".Split(", ");
            string[] numsRev = nums.Select(x => string.Concat(x.Reverse())).ToArray();
            
            foreach (var l in f)
            {
                string fi = "";
                string la = "";
                var revL = string.Concat(l.Reverse());
                for (int i = 0; i < l.Length; i++)
                {
                    if (fi == "" && char.IsNumber(l[i]))
                        fi += l[i];
                    
                    if (la == "" && char.IsNumber(revL[i]))
                        la += revL[i];
                    
                    foreach (var a in nums)
                        if (fi == "" && l[i..].StartsWith(a))
                            fi += (Array.IndexOf(nums, a) + 1).ToString();

                    foreach (var a in numsRev)
                        if (la == "" && revL[i..].StartsWith(a))
                            la += (Array.IndexOf(numsRev, a) + 1).ToString();
                    
                    if (fi != "" && la != "")
                        break;
                }

                s += int.Parse($"{fi}{la}");
            }
            Console.WriteLine(s);
        }

        public void PartTwo()
        {
            string[] nums = "one, two, three, four, five, six, seven, eight, nine".Split(", ");
            int s = 0;
            foreach (var l in File.ReadAllLines("in.txt"))
            {
                int first = int.MaxValue;
                int last = 0;
                int valFirst = 0;
                int valLast = 0;    

                foreach (var n in nums)
                {
                    var ind = l.IndexOf(n);
                    int ll = l.LastIndexOf(n);
                    if (ind != -1 && ind < first)
                    {
                        first = ind;
                        valFirst = Array.IndexOf(nums, n) + 1;
                    }
                    if (ll != -1 && ll > last)
                    {
                        last = ll;
                        valLast = Array.IndexOf(nums, n) + 1;
                    }
                }

                for (int i = 0; i < l.Length; i++)
                {
                    if (char.IsDigit(l[i]) && i < first) 
                    {
                        first = i;
                        valFirst = int.Parse(l[i].ToString());
                    }
                    if (char.IsDigit(l[i]) && i > last)
                    {
                        last = i;
                        valLast = int.Parse(l[i].ToString());
                    }
                }
                s += int.Parse($"{valFirst}{valLast}");
            }
            Console.WriteLine(s);
        }



        public int TryCollectFirstAndLastNum(string s)
        {
            var fl = s.Where(char.IsDigit).ToArray();
            return int.Parse($"{fl.First()}{fl.Last()}");
        }
    }
}
