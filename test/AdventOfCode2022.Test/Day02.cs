namespace AdventOfCode2022.Test;

public class Day02 : StringInputIntegerOutputTest
{
    protected override void Part1AssertTest(int output)
    {
        Assert.Equal(15, output);
    }

    protected override void Part1AssertActual(int output)
    {
        Assert.Equal(14069, output);
    }

    protected override void Part2AssertTest(int output)
    {
        Assert.Equal(12, output);
    }

    protected override void Part2AssertActual(int output)
    {
        Assert.Equal(12411, output);
    }
}
