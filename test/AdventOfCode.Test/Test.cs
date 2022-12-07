using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode.Test;

public abstract class Test<TInputPart1, TOutputPart1, TInputPart2, TOutputPart2>
{
    private readonly IDayChallenge<TInputPart1, TOutputPart1, TInputPart2, TOutputPart2> _sut;
    
    protected readonly int Day;

    protected Test()
    {
        Day = GetDay();

        var year = Regex.Match(GetType().Assembly.GetName().FullName, @"AdventOfCode(?<year>\d{4})\.Test").Groups["year"].Value;

        var assembly = Assembly.LoadFrom($"AdventOfCode{year}.dll");
        var sutType = assembly.GetType($"{assembly.GetName().Name}.Day{Day:00}");

        _sut = Activator.CreateInstance(sutType) as IDayChallenge<TInputPart1, TOutputPart1, TInputPart2, TOutputPart2>;
    }

    protected Test(Type alternativeSutType)
    {
        Day = GetDay();

        _sut = Activator.CreateInstance(alternativeSutType) as IDayChallenge<TInputPart1, TOutputPart1, TInputPart2, TOutputPart2>;
    }

    private int GetDay() => int.Parse(Regex.Match(GetType().Name, @"Day(?<day>\d{2}).*").Groups["day"].Value);

    [Fact]
    public void Part1() => Part1Assert(_sut.Part1(GetInputPart1()));

    [Fact]
    public void Part2() => Part2Assert(_sut.Part2(GetInputPart2()));

    protected abstract TInputPart1 GetInputPart1();
    protected abstract TInputPart2 GetInputPart2();

    protected abstract void Part1Assert(TOutputPart1 output);
    protected abstract void Part2Assert(TOutputPart2 output);
}
