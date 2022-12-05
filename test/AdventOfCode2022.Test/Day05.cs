namespace AdventOfCode2022.Test;

public class Day05 : StringInputStringOutputTest
{
    protected override void Part1AssertTest(string output)
    {
        Assert.Equal("CMZ", output);
    }

    protected override void Part1AssertActual(string output)
    {
        Assert.Equal("WHTLRMZRC", output);
    }

    protected override void Part2AssertTest(string output)
    {
        Assert.Equal("MCD", output);
    }

    protected override void Part2AssertActual(string output)
    {
        Assert.Equal("GMPMLWNMG", output);
    }
}
