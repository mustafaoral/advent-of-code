namespace AdventOfCode2022.Test;

public class Day07 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(1206825, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(9608311, output);
    }
}
