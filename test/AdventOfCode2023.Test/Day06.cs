namespace AdventOfCode2023.Test;

public class Day06 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(2344708, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(30125202, output);
    }
}
