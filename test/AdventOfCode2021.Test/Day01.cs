namespace AdventOfCode2021.Test;

public class Day01 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(1709, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(1761, output);
    }
}
