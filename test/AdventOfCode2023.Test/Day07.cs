namespace AdventOfCode2023.Test;

public class Day07 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(250474325, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(248909434, output);
    }
}
