namespace AdventOfCode;

public interface IDayChallenge<TInputPart1, TOutputPart1, TInputPart2, TOutputPart2>
{
    public TOutputPart1 Part1(TInputPart1 input);
    public TOutputPart2 Part2(TInputPart2 input);
}
