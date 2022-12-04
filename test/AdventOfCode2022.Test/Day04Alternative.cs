namespace AdventOfCode2022.Test;

public class Day04Alternative
{
    [Fact]
    public void Part1Test()
    {
        var result = AdventOfCode2022.Day04Alternative.Part1(Input.Day04_Test);

        Assert.Equal(2, result);
    }

    [Fact]
    public void Part1Actual()
    {
        var result = AdventOfCode2022.Day04Alternative.Part1(Input.Day04_Actual);

        Assert.Equal(595, result);
    }

    [Fact]
    public void Part2Test()
    {
        var result = AdventOfCode2022.Day04Alternative.Part2(Input.Day04_Test);

        Assert.Equal(4, result);
    }

    [Fact]
    public void Part2Actual()
    {
        var result = AdventOfCode2022.Day04Alternative.Part2(Input.Day04_Actual);

        Assert.Equal(952, result);
    }
}
