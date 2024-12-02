namespace AdventOfCode2024.Test;

public class Day02 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(236, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(308, output);
    }
}
