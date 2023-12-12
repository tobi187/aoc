using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    internal class Day7
    {
        internal void PartOne()
        {
            var f = File.ReadLines("in.txt");
            var cards = f
                .Select(Card.Create)
                .Select(x => x.GetWeight())
                .OrderByDescending(x => x)
                .ToArray();

            Console.WriteLine(cards.Select((x, i) => x.Res(i+1)).Sum());
        }
    }

    internal class Card : IComparable<Card>
    {
        readonly string Cards;
        string CardsWithJ;
        int Weigth;
        readonly int Bid;
        static readonly FrozenDictionary<string, int> Ranks = 
            "A, K, Q, T, 9, 8, 7, 6, 5, 4, 3, 2, J"
            .Split(", ")
            .Select((x, i) => (i, x))
            .ToFrozenDictionary(k => k.x, k => k.i);

        internal int Res(int rank) => rank * Bid;

        public static Card Create(string l) => new(l);

        public Card(string line)
        {
            var p = line.Split();
            Cards = p[0];
            Bid = int.Parse(p[1]);
            CardsWithJ = Cards;
        }

        public string Setj()
        {
            // 253420528 -> low

            var grp = Cards.GroupBy(x => x).OrderByDescending(x => x.Count());

            var maxOcc = grp.First();

            return Cards.Replace('J', maxOcc!.Key);
        }

        public Card GetWeight()
        {
            var cards = Setj();
            var grp = cards.GroupBy(x => x).OrderByDescending(x => x.Count());
            var count = grp.Count();
            switch (count)
            {
                case 1:
                    Weigth = 7; // alle gleich
                    break;
                case 2:
                    if (grp.Last().Count() == 2) // full house
                        Weigth = 5;
                    else
                        Weigth = 6; // 4 gleiche
                    break;
                case 3:
                    if (grp.First().Count() == 3)
                        Weigth = 4;
                    else
                        Weigth = 3;
                    break;
                case 4:
                    Weigth = 2;
                    break;
                default:
                    Weigth = 1;
                    break;
            }

            return this;
        }

        public int CompareTo(Card? other)
        {
            if (other == null) return -1;
            
            if (Weigth > other.Weigth)
                return -1;
            else if (Weigth < other.Weigth)
                return 1;
           
            for (int i = 0; i < Cards.Length; i++)
            {
                if (Ranks[Cards[i].ToString()] < Ranks[other.Cards[i].ToString()])
                    return -1;
                else if (Ranks[Cards[i].ToString()] > Ranks[other.Cards[i].ToString()])
                    return 1;
            }

            return 0;
        }
    }
}
