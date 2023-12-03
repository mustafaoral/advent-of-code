namespace AdventOfCode2023.Test;

public class Day03 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(517021, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(81296995, output);
    }
}
