namespace AdventOfCode2022.Test;

public class Day12 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(330, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(321, output);
    }
}
