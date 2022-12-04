using System.Reflection;
using System.Text.RegularExpressions;
using Xunit;

namespace AdventOfCode.Test;

public abstract class Test<TInputPart1, TOutputPart1, TInputPart2, TOutputPart2>
{
    protected readonly int Day;

    private readonly IDayChallenge<TInputPart1, TOutputPart1, TInputPart2, TOutputPart2> _sut;

    protected Test()
    {
        var type = GetType();

        Day = int.Parse(Regex.Match(type.Name, @"Day(?<day>\d{2}).*").Groups["day"].Value);

        var year = Regex.Match(type.Assembly.GetName().FullName, @"AdventOfCode(?<year>\d{4})\.Test").Groups["year"].Value;

        var assembly = Assembly.LoadFrom($"AdventOfCode{year}.dll");
        var assemblyName = assembly.GetName().Name;
        var sutType = assembly.GetType($"{assemblyName}.Day{Day:00}");

        _sut = Activator.CreateInstance(sutType) as IDayChallenge<TInputPart1, TOutputPart1, TInputPart2, TOutputPart2>;
    }

    protected Test(Type alternativeSutType)
        : this()
    {
        _sut = Activator.CreateInstance(alternativeSutType) as IDayChallenge<TInputPart1, TOutputPart1, TInputPart2, TOutputPart2>;
    }

    [Fact]
    public void Part1Test() => Part1AssertTest(_sut.Part1(GetTestInputPart1()));

    [Fact]
    public void Part1Actual() => Part1AssertActual(_sut.Part1(GetActualInputPart1()));

    [Fact]
    public void Part2Test() => Part2AssertTest(_sut.Part2(GetTestInputPart2()));

    [Fact]
    public void Part2Actual() => Part2AssertActual(_sut.Part2(GetActualInputPart2()));

    protected abstract TInputPart1 GetTestInputPart1();
    protected abstract TInputPart1 GetActualInputPart1();
    protected abstract TInputPart2 GetTestInputPart2();
    protected abstract TInputPart2 GetActualInputPart2();

    protected abstract void Part1AssertTest(TOutputPart1 output);
    protected abstract void Part1AssertActual(TOutputPart1 output);
    protected abstract void Part2AssertTest(TOutputPart2 output);
    protected abstract void Part2AssertActual(TOutputPart2 output);
}
