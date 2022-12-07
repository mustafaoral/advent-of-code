namespace AdventOfCode2022.Test;

public class Day03 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(7674, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(2805, output);
    }
}
