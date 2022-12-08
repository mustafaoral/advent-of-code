namespace AdventOfCode2021.Test;

public class Day04 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(2496, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(25925, output);
    }
}
