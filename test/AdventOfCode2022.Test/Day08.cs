namespace AdventOfCode2022.Test;

public class Day08 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(1776, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(234416, output);
    }
}
