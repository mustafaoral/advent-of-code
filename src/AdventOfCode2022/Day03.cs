namespace AdventOfCode2022;

public class Day03 : IStringInputIntegerOutputChallenge
{
    private static Dictionary<char, int> _priorityMap;

    static Day03()
    {
        var lowerCaseMap = Enumerable.Range(0, 26).Select(x => new KeyValuePair<char, int>((char)('a' + x), x + 1));
        var upperCaseMap = Enumerable.Range(0, 26).Select(x => new KeyValuePair<char, int>((char)('A' + x), x + 1 + 26));

        _priorityMap = lowerCaseMap.Union(upperCaseMap).ToDictionary(x => x.Key, x => x.Value);
    }

    public int Part1(string input)
    {
        return input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .SelectMany(line =>
            {
                var items = line.SplitToTokensInLength(line.Length / 2).Select(x => x.ToCharArray());

                return items.ElementAt(0).Intersect(items.ElementAt(1));
            })
            .Select(x => _priorityMap[x])
            .Sum();
    }

    public int Part2(string input)
    {
        return input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select((line, i) => new { Index = i, Line = line })
            .GroupBy(x => x.Index / 3)
            .Select(grouping =>
            {
                return grouping.Skip(1).Aggregate(seed: new HashSet<char>(grouping.ElementAt(0).Line), (accumulate, x) =>
                {
                    accumulate.IntersectWith(x.Line);

                    return accumulate;
                }, x => x.Single());
            })
            .Select(x => _priorityMap[x])
            .Sum();
    }
}
