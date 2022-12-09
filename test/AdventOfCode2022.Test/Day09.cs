namespace AdventOfCode2022.Test;

public class Day09 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(6067, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(2471, output);
    }
}
