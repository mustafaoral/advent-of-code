namespace AdventOfCode2024;

public class Day02 : IStringInputIntegerOutputPuzzle
{
    public int Part1(string input)
    {
        return input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Count(x => IsSafeLine(x.MatchIntArray()));
    }

    public int Part2(string input)
    {
        return input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Count(line =>
            {
                var numbers = line.MatchIntArray();

                return IsSafeLine(numbers) || numbers.Masked(maskingWidth: 1).Any(IsSafeLine);
            });
    }

    private static bool IsSafeLine(int[] lineNumbers)
    {
        var differences = lineNumbers.Pairwise(x => x.First - x.Second).ToArray();

        return (differences.All(x => x > 0) || differences.All(x => x < 0)) && differences.All(x => Math.Abs(x) > 0 && Math.Abs(x) < 4);
    }
}
