namespace AdventOfCode2022.Test;

public class Day01
{
    [Fact]
    public void Part1Test()
    {
        var result = AdventOfCode2022.Day01.Part1(Input.Day01_Test);

        Assert.Equal(24000, result);
    }

    [Fact]
    public void Part1Actual()
    {
        var result = AdventOfCode2022.Day01.Part1(Input.Day01_Actual);

        Assert.Equal(68467, result);
    }

    [Fact]
    public void Part2Test()
    {
        var result = AdventOfCode2022.Day01.Part2(Input.Day01_Test);

        Assert.Equal(45000, result);
    }

    [Fact]
    public void Part2Actual()
    {
        var result = AdventOfCode2022.Day01.Part2(Input.Day01_Actual);

        Assert.Equal(203420, result);
    }
}
