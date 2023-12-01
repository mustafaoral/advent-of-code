namespace AdventOfCode2023;

public class Day01 : IStringInputIntegerOutputPuzzle
{
    public int Part1(string input)
    {
        var valueMap = new Dictionary<string, int>
        {
            ["1"] = 1,
            ["2"] = 2,
            ["3"] = 3,
            ["4"] = 4,
            ["5"] = 5,
            ["6"] = 6,
            ["7"] = 7,
            ["8"] = 8,
            ["9"] = 9
        };

        return GetCalibrationTotal(input, valueMap);
    }

    public int Part2(string input)
    {
        var valueMap = new Dictionary<string, int>
        {
            ["1"] = 1,
            ["2"] = 2,
            ["3"] = 3,
            ["4"] = 4,
            ["5"] = 5,
            ["6"] = 6,
            ["7"] = 7,
            ["8"] = 8,
            ["9"] = 9,
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4,
            ["five"] = 5,
            ["six"] = 6,
            ["seven"] = 7,
            ["eight"] = 8,
            ["nine"] = 9
        };

        return GetCalibrationTotal(input, valueMap);
    }

    private static int GetCalibrationTotal(string input, Dictionary<string, int> valueMap)
    {
        return input
            .Split(Environment.NewLine)
            .Sum(line =>
            {
                var matches = valueMap.Keys.SelectMany(key => Regex.Matches(line, key)).OrderBy(x => x.Index).ToArray();

                return int.Parse($"{valueMap[matches[0].Value]}{valueMap[matches[^1].Value]}");
            });
    }
}
