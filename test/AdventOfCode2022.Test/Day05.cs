namespace AdventOfCode2022.Test;

public class Day05 : SharedStringInputTest<string, string>
{
    protected override void Part1Assert(string output)
    {
        Assert.Equal("WHTLRMZRC", output);
    }

    protected override void Part2Assert(string output)
    {
        Assert.Equal("GMPMLWNMG", output);
    }
}
