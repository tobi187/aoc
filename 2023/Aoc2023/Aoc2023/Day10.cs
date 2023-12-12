using System.Collections;
using System.Security.Cryptography;

namespace Aoc2023
{
    internal class Day10
    {
        char[][] file = File.ReadAllLines("in.txt").Select(x => x.ToCharArray()).ToArray();

        int x  = 0;
        int y = 0;
        int steps = 0;

        internal void PartOne()
        {
            int sx = 0;
            int sy = 0;

            for (int y = 0; y < file.Length; y++)
                for (int x = 0; x < file[0].Length; x++)
                    if (file[y][x] == 's')
                        (sx, sy) = (x, y);

            
        }

        internal bool GotoNext()
        {
            switch (file[y][x])
            {
                case 'S': 
                    return true;
                case '-'


            }
            
        }
    }

    internal record Point
    {
        internal int x;
        internal int y;
        internal (int x, int y) From;
        internal int steps;
    }
}
