namespace AdventOfCode2023;

public class Day05 : IPuzzle<string, long, string, long>
{
    public long Part1(string input)
    {
        var regions = input.Split(Environment.NewLine + Environment.NewLine);
        var repeat = regions[0].Split(":")[1].MatchLongArray();
        var maps = GetMaps(regions);

        foreach (var map in maps)
        {
            repeat = repeat.Select(map.GetDestination).ToArray();
        }

        return repeat.Min();
    }

    public long Part2(string input)
    {
        var regions = input.Split(Environment.NewLine + Environment.NewLine);
        var ranges = regions[0].Split(":")[1].MatchLongArray().Windowed(2, 2).Select(x => new AocRange(x.First(), x.Last())).ToArray();
        var maps = GetMaps(regions);

        var location = long.MaxValue;

        foreach (var range in ranges)
        {
            for (long i = 0; i < range.Count; i++)
            {
                var destination = range.Start + i;

                foreach (var map in maps)
                {
                    destination = map.GetDestination(destination);
                }

                location = Math.Min(location, destination);
            }
        }

        return location;
    }

    private static Map[] GetMaps(string[] regions)
    {
        return regions.Skip(1).Select(region =>
         {
             var lines = region.Split(Environment.NewLine).ToArray();

             var match = Regex.Match(lines[0], @"(?<source>\w+)\-to\-(?<destination>\w+).+");

             var map = new Map(match.Groups["source"].Value, match.Groups["destination"].Value);

             foreach (var line in lines.Skip(1))
             {
                 var matches = line.MatchLongArray();
                 map.AddRange(matches[0], matches[1], matches[2]);
             }

             return map;
         }).ToArray();
    }

    private record AocRange(long Start, long Count);

    private class Map(string source, string destination)
    {
        private string _source = source;
        private List<(long, long)> _sourceRanges = [];

        private string _destination = destination;
        private List<(long, long)> _destinationRanges = [];

        public void AddRange(long destinationRangeStart, long sourceRangeStart, long rangeLength)
        {
            _sourceRanges.Add((sourceRangeStart, sourceRangeStart + rangeLength - 1));
            _destinationRanges.Add((destinationRangeStart, destinationRangeStart + rangeLength - 1));
        }

        public long GetDestination(long source)
        {
            int i = 0;
            for (; i < _sourceRanges.Count; i++)
            {
                if (source >= _sourceRanges[i].Item1 && source <= _sourceRanges[i].Item2)
                {
                    break;
                }
            }

            if (i == _sourceRanges.Count)
            {
                return source;
            }

            return _destinationRanges[i].Item1 + source - _sourceRanges[i].Item1;
        }
    }
}
