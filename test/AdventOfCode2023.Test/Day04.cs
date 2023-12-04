namespace AdventOfCode2023.Test;

public class Day04 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(18653, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(5921508, output);
    }
}
