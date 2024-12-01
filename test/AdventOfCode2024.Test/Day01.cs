namespace AdventOfCode2024.Test;

public class Day01 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(1530215, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(26800609, output);
    }
}
