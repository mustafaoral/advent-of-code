namespace AdventOfCode.Test;

public abstract class StringInputIntegerOutputTest : StringInputTest<int, int>
{
    protected StringInputIntegerOutputTest()
    {
    }

    protected StringInputIntegerOutputTest(Type alternativeSutType)
        : base(alternativeSutType)
    {
    }
}
