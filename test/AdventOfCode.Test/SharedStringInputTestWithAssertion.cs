namespace AdventOfCode.Test;

public abstract class SharedStringInputTestWithAssertion<TOutputPart1, TOutputPart2>(TOutputPart1 expectedOutputPart1, TOutputPart2 expectedOutputPart2) : Test<string, TOutputPart1, string, TOutputPart2>
{
    private string GetInput() => File.ReadAllText(Path.Combine("input", $"day{Day:00}.txt"));

    private readonly TOutputPart1 _expectedOutputPart1 = expectedOutputPart1;
    private readonly TOutputPart2 _expectedOutputPart2 = expectedOutputPart2;

    protected override void Part1Assert(TOutputPart1 output) => Assert.Equal(_expectedOutputPart1, output);
    protected override void Part2Assert(TOutputPart2 output) => Assert.Equal(_expectedOutputPart2, output);

    protected override string GetInputPart1() => GetInput();
    protected override string GetInputPart2() => GetInput();
}
