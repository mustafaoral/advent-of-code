namespace AdventOfCode2022.Test;

public class Day04 : SharedStringInputTest<int ,int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(595, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(952, output);
    }
}
