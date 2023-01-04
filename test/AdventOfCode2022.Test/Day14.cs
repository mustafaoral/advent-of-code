namespace AdventOfCode2022.Test;

public class Day14 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(913, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(30762, output);
    }
}
