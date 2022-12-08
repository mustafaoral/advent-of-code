namespace AdventOfCode2021.Test;

public class Day03 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(1025636, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(793873, output);
    }
}
