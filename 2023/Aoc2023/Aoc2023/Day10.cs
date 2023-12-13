using System.Collections;
using System.Security.Cryptography;

namespace Aoc2023
{
    internal class Day10
    {
        char[][] file = File.ReadAllLines("in.txt").Select(x => x.ToCharArray()).ToArray();

        int x  = 0;
        int y = 0;
        int steps = 1;
        (int x, int y) last = (0, 0);

        internal void PartOne()
        {
            int sx = 0;
            int sy = 0;

            for (int y = 0; y < file.Length; y++)
                for (int x = 0; x < file[0].Length; x++)
                    if (file[y][x] == 'S')
                        (sx, sy) = (x, y);

            last = (sx, sy);
            x = last.x - 1;
            y = last.y;
            while (!GotoNext()) 
                steps++;

            Console.WriteLine(Math.Ceiling(steps / 2.0));
        }

        internal bool GotoNext()
        {
            var l = (x, y); 
            switch (file[y][x])
            {
                case 'S':
                    return true;
                case '-':
                    if (last.x < x)
                        x += 1;
                    else
                        x -= 1;
                    break;
                case '|':
                    if (last.y < y)
                        y += 1;
                    else
                        y -= 1;
                    break;
                case 'L':
                    if (last.y < y)
                        x += 1;
                    else 
                        y -= 1;
                    break;
                case 'J':
                    if (last.y < y) 
                        x -= 1;
                    else
                        y -= 1;
                    break;
                case 'F':
                    if (last.y > y)
                        x += 1;
                    else
                        y += 1;
                    break;
                case '7':
                    if (last.y > y)
                        x -= 1;
                    else
                        y += 1;
                    break;
            }
            last = l;
            return false;
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
