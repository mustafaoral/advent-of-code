namespace AdventOfCode2023.Test;

public class Day09 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(1757008019, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(995, output);
    }
}
