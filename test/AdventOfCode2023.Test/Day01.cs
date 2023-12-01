namespace AdventOfCode2023.Test;

public class Day01 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(55208, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(54578, output);
    }
}
