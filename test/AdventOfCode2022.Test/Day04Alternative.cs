namespace AdventOfCode2022.Test;

public class Day04Alternative : SharedStringInputTest<int, int>
{
    public Day04Alternative() : base(alternativeSutType: typeof(AdventOfCode2022.Day04Alternative))
    {
    }

    protected override void Part1Assert(int output)
    {
        Assert.Equal(595, output);
    }

    protected override void Part2Assert(int output)
    {
        Assert.Equal(952, output);
    }
}
