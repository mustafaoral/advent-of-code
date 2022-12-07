namespace AdventOfCode2022.Test;

public class Day06 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(1816, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(2625, output);
    }
}
