using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    internal class Day14
    {
        public char[][] file = File.ReadAllLines("in.txt").Select(x => x.ToArray()).ToArray();
        public void PartOne()
        {
            for (int i = 0; i < 1_000_000_000; i++)
            {
                Rotate();
                if (i% 100_000_000 == 0) Console.WriteLine(i);
            }
            Calc();
        }

        public void Rotate()
        {
            ShiftNorth(true);
            ShiftEast(true);
            ShiftNorth(false);
            ShiftEast(false);
        }

        void Easy()
        {
            for (int x = 0; x < file[0].Length; x++)
            {
                int count = 0;
                for (int y = 0; y < file.Length; y++)
                {

                }
            }
        }

        void ShiftNorth(bool d)
        {
            var n = file.Select(x => x.Select(y => y == 'O' ? '.' : y).ToArray()).ToArray();
            
            for (int x = 0; x < file[0].Length; x++)
            {
                int stoneCount = 0;
                int lastStone = d ? 0 : file.Length-1;
                for (int y = d ? 0 : n.Length - 1; y < n.Length && y >= 0; y += d ? 1 : -1)
                {
                    if (file[y][x] == 'O') stoneCount++;
                    else if (file[y][x] == '#')
                    {
                        for (int yy = 0; yy < stoneCount; yy++)
                            n[lastStone + yy * (d ? 1 : -1)][x] = 'O';
                        stoneCount = 0;
                        lastStone = y + (d ? 1 : -1);
                    }
                }
                for (int yy = 0; yy < stoneCount; yy++)
                    n[lastStone + yy * (d ? 1 : -1)][x] = 'O';
            }

            //Console.WriteLine(string.Join("\n", n.Select(s => string.Concat(s))));

            file = n;
        }

        void ShiftEast(bool d)
        {
            var n = file.Select(x => x.Select(y => y == 'O' ? '.' : y).ToArray()).ToArray();

            for (int y = 0; y < file[0].Length; y++)
            {
                int stoneCount = 0;
                int lastStone = d ? 0 : file.Length - 1;
                for (int x = d ? 0 : n.Length - 1; x < n.Length && x >= 0; x += d ? 1 : -1)
                {
                    if (file[y][x] == 'O') stoneCount++;
                    else if (file[y][x] == '#')
                    {
                        for (int xx = 0; xx < stoneCount; xx++)
                            n[y][lastStone + xx * (d ? 1 : -1)] = 'O';
                        stoneCount = 0;
                        lastStone = x + (d ? 1 : -1);
                    }
                }
                for (int xx = 0; xx < stoneCount; xx++)
                    n[y][lastStone + xx * (d ? 1 : -1)] = 'O';
            }

            //Console.WriteLine(string.Join("\n", n.Select(s => string.Concat(s))));

            file = n;
        }

        void Calc(bool show = false)
        {
            if (show)
                Console.WriteLine(string.Join("\n", file.Select(s => string.Concat(s))));

            var g = file;
            int s = 0;
            for (int y = 0; y < g.Length; y++)
            {
                var c = g[y].Count(x => x == 'O');
                s += c * (g.Length - y);
            }
            Console.WriteLine(s);
        }
    }
}
