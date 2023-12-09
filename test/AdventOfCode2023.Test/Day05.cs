namespace AdventOfCode2023.Test;

public class Day05 : SharedStringInputTest<long, long>
{
    protected override void Part1Assert(long output)
    {
        Assert.Equal(51752125, output);
    }

    protected override void Part2Assert(long output)
    {
        Assert.Equal(12634632, output);
    }
}
