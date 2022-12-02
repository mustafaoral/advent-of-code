namespace AdventOfCode2022.Test;

public class Day02
{
    [Fact]
    public void Part1Test()
    {
        var result = AdventOfCode2022.Day02.Part1(Input.Day02_Test);

        Assert.Equal(15, result);
    }

    [Fact]
    public void Part1Actual()
    {
        var result = AdventOfCode2022.Day02.Part1(Input.Day02_Actual);

        Assert.Equal(14069, result);
    }

    [Fact]
    public void Part2Test()
    {
        var result = AdventOfCode2022.Day02.Part2(Input.Day02_Test);

        Assert.Equal(12, result);
    }

    [Fact]
    public void Part2Actual()
    {
        var result = AdventOfCode2022.Day02.Part2(Input.Day02_Actual);

        Assert.Equal(12411, result);
    }
}
