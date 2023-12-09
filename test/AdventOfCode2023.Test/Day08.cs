namespace AdventOfCode2023.Test;

public class Day08 : SharedStringInputTest<long, long>
{
    protected override void Part1Assert(long output)
    {
        Assert.Equal(17141, output);
    }

    protected override void Part2Assert(long output)
    {
        Assert.Equal(10818234074807, output);
    }
}
