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
            ShiftNorth(1);
        }

        void Easy()
        {
            for (int x = 0; x < file[0].Length; x++)
            {
                int count = 0;
                for (int y = ; y < file.Length; y++)
                {

                }
            }
        }

        void ShiftNorth(int dir)
        {
            var n = file.Select(x => x.Select(y => y == 'O' ? '.' : y).ToArray()).ToArray();
            
            for (int x = 0; x < file[0].Length; x++)
            {
                int stoneCount = 0;
                int lastStone = 0;
                for (int y = 0; y < n.Length; y++)
                {
                    if (file[y][x] == 'O') stoneCount++;
                    else if (file[y][x] == '#')
                    {
                        if (stoneCount == 0)
                        {
                            lastStone = y;
                            continue;
                        }
                        //if (lastStone != 0) ++lastStone;
                        for (int yy = lastStone; yy < lastStone + stoneCount; yy++)
                            n[yy][x] = 'O';
                        stoneCount = 0;
                        lastStone = y;
                    }
                }
                for (int yy = lastStone; yy != lastStone + stoneCount; yy++)
                    n[yy][x] = 'O';
            }

            Console.WriteLine(string.Join("\n", n.Select(s => string.Concat(s))));
        }
    }
}
