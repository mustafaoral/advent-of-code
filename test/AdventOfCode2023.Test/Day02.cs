namespace AdventOfCode2023.Test;

public class Day02 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(2810, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(69110, output);
    }
}
