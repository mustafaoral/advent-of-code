namespace AdventOfCode2021;

public class Day01 : IStringInputIntegerOutputChallenge
{
    public int Part1(string input)
    {
        return GetReadings(input).PairwiseCount(x => x.Second > x.First);
    }

    public int Part2(string input)
    {
        return GetReadings(input).SlidingWindow(3, x => x.Sum()).PairwiseCount(x => x.Second > x.First);
    }

    private static int[] GetReadings(string input)
    {
        return input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }
}
