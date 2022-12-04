namespace AdventOfCode2022.Test;

public class Day03 : StringInputIntegerOutputTest
{
    protected override void Part1AssertTest(int output)
    {
        Assert.Equal(157, output);
    }

    protected override void Part1AssertActual(int output)
    {
        Assert.Equal(7674, output);
    }

    protected override void Part2AssertTest(int output)
    {
        Assert.Equal(70, output);
    }

    protected override void Part2AssertActual(int output)
    {
        Assert.Equal(2805, output);
    }
}
