namespace AdventOfCode2022.Test;

public class Day02 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(14069, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(12411, output);
    }
}
