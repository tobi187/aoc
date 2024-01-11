using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace Aoc2023
{
    internal class Day21
    {
        int steps = 63;
        int Xlen;
        int Ylen;
        ImmutableArray<ImmutableArray<char>> grid;
        
        public void PartOne()
        {
            grid = 
                File.ReadLines("in.txt")
                .Select(x => x.ToImmutableArray())
                .ToImmutableArray();

            Xlen = grid[0].Length;
            Ylen = grid.Length;
            var start = grid.Select((x,i) => (x: x.IndexOf('S'), y: i)).Single(p => p.x > -1);

            Bfs(start.x, start.y);
        }

        public void Bfs(int sx, int sy)
        {
            HashSet<(int, int)> visited = [];
            Queue<(int x, int y)> q = [];
            q.Enqueue((sx, sy));
            visited.Add((sx, sy));

            while (steps-- > 0)
            {
                int qc = q.Count;
                for (int _ = 0; _ < qc; _++)
                {
                    var (x, y) = q.Dequeue();
                    foreach (var ng in GetNeighs(x, y))
                    {
                        if (grid[ng.y][ng.x] == '#')
                            continue;
                        q.Enqueue(ng);
                    }
                }
                Console.WriteLine(steps);
            }

            var hs = new HashSet<(int, int)>();
            while (q.Count > 0)
            {
                var pt =q.Dequeue();
                foreach (var x in GetNeighs(pt.x, pt.y))
                    if (grid[x.y][x.x] != '#')
                        hs.Add(x);
            }

            char[] arr = new char[Xlen];
            Array.Fill(arr, '.');
            var ss = new char[Ylen][];
            for (int i = 0; i < ss.Length; i++)
                ss[i] = [..arr];
            for (int y = 0 ; y < Ylen; y++)
            {
                for (int x = 0 ; x < Xlen; x++)
                {
                    if (grid[y][x] == '#')
                        ss[y][x] = '#';
                    if (hs.Contains((x, y)))
                        ss[y][x] = 'O';
                }
            }



            Console.WriteLine(string.Join('\n', ss.Select(x => string.Concat(x))));
            Console.WriteLine(hs.Count);
        }

        public IEnumerable<(int x, int y)> GetNeighs(int x, int y)
        {
            return new (int x, int y)[] { (x + 1, y), (x - 1, y), (x, y - 1), (x, y + 1) }
            .Where(pt => pt.x >= 0 && pt.x < Xlen && pt.y >= 0 && pt.y < Ylen);
        }
    }
}
