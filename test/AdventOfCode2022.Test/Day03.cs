namespace AdventOfCode2022.Test;

public class Day03
{
    [Fact]
    public void Part1Test()
    {
        var result = AdventOfCode2022.Day03.Part1(Input.Day03_Test);

        Assert.Equal(157, result);
    }

    [Fact]
    public void Part1Actual()
    {
        var result = AdventOfCode2022.Day03.Part1(Input.Day03_Actual);

        Assert.Equal(7674, result);
    }

    [Fact]
    public void Part2Test()
    {
        var result = AdventOfCode2022.Day03.Part2(Input.Day03_Test);

        Assert.Equal(70, result);
    }

    [Fact]
    public void Part2Actual()
    {
        var result = AdventOfCode2022.Day03.Part2(Input.Day03_Actual);

        Assert.Equal(2805, result);
    }
}
