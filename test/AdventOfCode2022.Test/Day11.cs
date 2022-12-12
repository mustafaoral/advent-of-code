namespace AdventOfCode2022.Test;

public class Day11 : SharedStringInputTest<long, long>
{
    protected override void Part1Assert(long output)
    {
        Assert.Equal(151312, output);
    }

    protected override void Part2Assert(long output)
    {
        Assert.Equal(51382025916, output);
    }
}
