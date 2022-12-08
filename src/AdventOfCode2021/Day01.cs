namespace AdventOfCode2021;

public class Day01 : IStringInputIntegerOutputChallenge
{
    public int Part1(string input)
    {
        return GetReadings(input).SlidingWindow(2).Count(x => x.ElementAt(1) > x.ElementAt(0));
    }

    public int Part2(string input)
    {
        return GetReadings(input).SlidingWindow(3).Select(x => x.Sum()).SlidingWindow(2).Count(x => x.ElementAt(1) > x.ElementAt(0));
    }

    private static int[] GetReadings(string input)
    {
        return input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }
}
