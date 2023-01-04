namespace AdventOfCode2022.Test;

public class Day13 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(5013, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(25038, output);
    }
}
