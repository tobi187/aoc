using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Aoc2023
{
    internal class Day8
    {
        public void PartTwo()
        {
            var file = File.ReadAllLines("in.txt");
            var dirs = file[0];
            var ff = file.Skip(2).ToArray();

            var nodes = new Dictionary<string, Node>();
            var nos = new List<Node>();
            foreach (var b in ff)
            {
                var n = new Node(b);
                nodes.Add(n.Val, n);
                if (n.Val[2] == 'A')
                    nos.Add(n);
            }

            foreach (var fff in ff)
            {
                var l = fff.Split("=");
                var ll = l[0].TrimEnd();
                var lll = l[1].Split(",").Select(x => x.Replace(")", "").Replace("(", "").Trim());
                nodes[ll].Left = nodes[lll.ElementAt(0)];
                nodes[ll].Right = nodes[lll.ElementAt(1)];
            }


        }

            public void PartOne()
        {
            var file = File.ReadAllLines("in.txt");
            var dirs = file[0];
            var ff = file.Skip(2).ToArray();

            // var a = ff.GroupBy(x => x[..3]).OrderBy(x => x.Count()).ToArray();
            var nodes = new Dictionary<string, Node>(); 
            var nos = new List<Node>();
            foreach (var b in ff)
            {
                var n = new Node(b);
                nodes.Add(n.Val, n);
                if (n.Val[2] == 'A')
                    nos.Add(n);
            }

            foreach (var fff in ff)
            {
                var l = fff.Split("=");
                var ll = l[0].TrimEnd();
                var lll = l[1].Split(",").Select(x => x.Replace(")", "").Replace("(", "").Trim());
                nodes[ll].Left = nodes[lll.ElementAt(0)];
                nodes[ll].Right = nodes[lll.ElementAt(1)];
            }

            var cd = new ConcurrentQueue<(int, long)>();

            var c = Channel.CreateUnbounded<(int, long)>();

            var threads = nos.Select((x,i) => CreateThread(x, i, dirs, c.Writer)).ToArray();
            var tData = new HashSet<long>[threads.Length];
            for (int i2 = 0; i2 < threads.Length; i2++)
                tData[i2] = new HashSet<long>();

            foreach (var t in threads)
                t.Start();

            var reader = c.Reader;

            while (true)
            {
                if (reader.TryRead(out var a)) {
                    tData[a.Item1].Add(a.Item2);
                    foreach (var val in tData[0]) 
                    { 
                        if (tData.All(x => x.Contains(val)))
                        {
                            Console.WriteLine(val);
                            Environment.Exit(0);
                        }
                    }
                }
            }
        }

        Thread CreateThread(Node start, int threadIndex, string dirs, ChannelWriter<(int, long)> q)
        {
            return new Thread(new ThreadStart(() =>
            {
                long i = 0;
                while (true)
                {
                    int v = (int)(i++ % dirs.Length);
                    start = start.Walk(dirs[v]);
                    if (start.Val[2] == 'Z')
                    {
                        while (!q.TryWrite((threadIndex, i))) { }
                    }
                }
            }));
        }
    }

    internal class Node
    {
        public Node Right { get; set; }
        public Node Left { get; set; }
        public string Val { get; set; }

        public Node(string val) {
            Val = val[..3];
        }

        public Node Walk(char d)
        {
            if (d == 'R')
                return Right;
            else
                return Left;
        }

        public Node Walk(string d)
        {
            if (d == "R")
                return Right;
            else
                return Left;
        }
    }

}
