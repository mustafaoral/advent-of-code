namespace AdventOfCode2021.Test;

public class Day02 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(1654760, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(1956047400, output);
    }
}
