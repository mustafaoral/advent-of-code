namespace AdventOfCode.Test;

public abstract class SharedStringInputTest<TOutputPart1, TOutputPart2> : Test<string, TOutputPart1, string, TOutputPart2>
{
    private string GetInput() => File.ReadAllText(Path.Combine("input", $"day{Day:00}.txt"));

    protected override string GetInputPart1() => GetInput();
    protected override string GetInputPart2() => GetInput();
}
