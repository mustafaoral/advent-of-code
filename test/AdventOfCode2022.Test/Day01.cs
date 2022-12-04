namespace AdventOfCode2022.Test;

public class Day01 : StringInputIntegerOutputTest
{
    protected override void Part1AssertTest(int output)
    {
        Assert.Equal(24000, output);
    }

    protected override void Part1AssertActual(int output)
    {
        Assert.Equal(68467, output);
    }

    protected override void Part2AssertTest(int output)
    {
        Assert.Equal(45000, output);
    }

    protected override void Part2AssertActual(int output)
    {
        Assert.Equal(203420, output);
    }
}
