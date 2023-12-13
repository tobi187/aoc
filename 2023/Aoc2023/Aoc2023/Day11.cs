namespace Aoc2023
{
    public class Day11
    {
        public IEnumerable<(long, long)> PartTwo()
        {
            var file = File.ReadAllLines("in.txt").Select(x => x.ToArray()).ToArray();
            List<(long x, long y)> coords = [];

            for (int i = 0; i < file.Length; i++)
                for (int j = 0; j < file[0].Length; j++)
                    if (file[i][j] == '#')
                        coords.Add((j, i));

            var replRows = Enumerable.Range(0, file.Length).Except(coords.Select(el => (int)el.y)).ToArray();
            var replCols = Enumerable.Range(0, file.Length).Except(coords.Select(el => (int)el.x)).ToArray();

            for (var i = 0; i < coords.Count; i++)
            {
                var c = coords[i];
                long rowAmpl = replRows.TakeWhile(y => y < c.y).Count() * (1000000 - 1);
                long colAmpl = replCols.TakeWhile(x => x < c.x).Count() * (1000000 - 1);
                coords[i] = (c.x + colAmpl, c.y + rowAmpl);
            }

            return coords;
        }
    }
}
