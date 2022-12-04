namespace AdventOfCode.Test;

public abstract class StringInputTest<TOutputPart1, TOutputPart2> : Test<string, TOutputPart1, string, TOutputPart2>
{
    protected StringInputTest()
    {
    }

    protected StringInputTest(Type alternativeSutType)
        : base(alternativeSutType)
    {
    }

    protected override string GetTestInputPart1() => File.ReadAllText(Path.Combine("input", $"day{Day:00}.test.txt"));
    protected override string GetActualInputPart1() => File.ReadAllText(Path.Combine("input", $"day{Day:00}.actual.txt"));

    protected override string GetTestInputPart2() => File.ReadAllText(Path.Combine("input", $"day{Day:00}.test.txt"));
    protected override string GetActualInputPart2() => File.ReadAllText(Path.Combine("input", $"day{Day:00}.actual.txt"));
}
