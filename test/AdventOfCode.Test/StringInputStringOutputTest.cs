namespace AdventOfCode.Test;

public abstract class StringInputStringOutputTest : StringInputTest<string, string>
{
    protected StringInputStringOutputTest()
    {
    }

    protected StringInputStringOutputTest(Type alternativeSutType)
        : base(alternativeSutType)
    {
    }
}
