namespace AdventOfCode2022.Test;

public class Day04Alternative : StringInputIntegerOutputTest
{
    public Day04Alternative() : base(alternativeSutType: typeof(AdventOfCode2022.Day04Alternative))
    {
    }

    protected override void Part1AssertTest(int output)
    {
        Assert.Equal(2, output);
    }

    protected override void Part1AssertActual(int output)
    {
        Assert.Equal(595, output);
    }

    protected override void Part2AssertTest(int output)
    {
        Assert.Equal(4, output);
    }

    protected override void Part2AssertActual(int output)
    {
        Assert.Equal(952, output);
    }
}
