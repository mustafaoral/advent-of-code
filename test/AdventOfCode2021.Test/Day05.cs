namespace AdventOfCode2021.Test;

public class Day05 : SharedStringInputTest<int, int>
{
    protected override void Part1Assert(int output)
    {
        Assert.Equal(6856, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(20666, output);
    }
}
