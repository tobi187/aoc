using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    internal class Day16
    {
        int x = 0;
        int y = 0;
        internal void PartOne()
        {
            var file = File.ReadAllLines("in.txt");
            var moves = new List<List<(int, int)>>();
            var grid = new Pt[file.Length][];

            for (int i = 0; i< file.Length; i++)
            {
                var row = new Pt[file[i].Length];
                for (int x = 0; x < file[i].Length; x++)
                {
                    switch (file[i][x])
                    {
                        case '.':
                            row[x] = new Tile(moves, '.');
                            break;
                        case '|':
                            row[x] = new Splitter(moves, '|');
                            break;
                        case '-':
                            row[x] = new Splitter(moves, '-');
                            break;
                        case '\\':
                            row[x] = new Mirror(moves, '\\');
                            break;
                        case '/':
                            row[x] = new Mirror(moves, '/');
                            break;
                    }
                }
                grid[i] = row;
            }


        }
    }

    internal interface Pt
    {
        List<List<(int, int)>> moves { get; }
        int stepped { get; }
        char me { get; }

        void Move(int li);
    }

    internal class Tile : Pt
    {
        public List<List<(int, int)>> moves { get; }
        public char me { get; }
        public int stepped { get;  } = 0;

        public Tile(List<List<(int, int)>> mv, char me)
        {
            moves = mv;
            this.me = me;
        }

        public void Move(int li)
        {
            
        }
    }

    internal class Splitter : Pt
    {
        public List<List<(int, int)>> moves { get; }
        public char me { get; }
        public int stepped { get; } = 0;

        public Splitter(List<List<(int, int)>> mv, char me)
        {
            moves = mv;
            this.me = me;
        }

        public void Move(int li)
        {
            throw new NotImplementedException();
        }
    }

    internal class Mirror : Pt
    {
        public List<List<(int, int)>> moves { get; }
        public char me { get; }
        public int stepped { get; } = 0;

        public Mirror(List<List<(int, int)>> mv, char me)
        {
            moves = mv;
            this.me = me;
        }

        public void Move(int li)
        {
            throw new NotImplementedException();
        }
    }
}
