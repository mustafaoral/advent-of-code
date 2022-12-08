using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode.Test;

public abstract class Test<TInputPart1, TOutputPart1, TInputPart2, TOutputPart2>
{
    private readonly IDayChallenge<TInputPart1, TOutputPart1, TInputPart2, TOutputPart2> _sut;
    
    protected readonly int Day;

    protected Test()
    {
        var type = GetType();

        Day = int.Parse(Regex.Match(type.Name, @"Day(?<day>\d{2}).*").Groups["day"].Value);

        var year = Regex.Match(type.Assembly.GetName().FullName, @"AdventOfCode(?<year>\d{4})\.Test").Groups["year"].Value;

        var assembly = Assembly.LoadFrom($"AdventOfCode{year}.dll");
        var sutType = assembly.GetType($"{assembly.GetName().Name}.{type.Name}");

        _sut = Activator.CreateInstance(sutType) as IDayChallenge<TInputPart1, TOutputPart1, TInputPart2, TOutputPart2>;
    }

    [Fact]
    public void Part1() => Part1Assert(_sut.Part1(GetInputPart1()));

    [Fact]
    public void Part2() => Part2Assert(_sut.Part2(GetInputPart2()));

    protected abstract TInputPart1 GetInputPart1();
    protected abstract TInputPart2 GetInputPart2();

    protected abstract void Part1Assert(TOutputPart1 output);
    protected abstract void Part2Assert(TOutputPart2 output);
}
