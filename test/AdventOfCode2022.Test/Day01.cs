namespace AdventOfCode2022.Test;

public class Day01 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(68467, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(203420, output);
    }
}
