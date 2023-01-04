using System.Diagnostics;

namespace AdventOfCode2022;

public class Day11 : IStringInputPuzzle<long, long>
{
    public long Part1(string input)
    {
        return GetOutput(GetMonkeyMap(input), rounds: 20, worryAdjustment: x => x / 3);
    }

    public long Part2(string input)
    {
        var monkeys = GetMonkeyMap(input);

        var mod = monkeys.Values.Select(x => x.Divisor).Aggregate((x, y) => x * y);

        return GetOutput(monkeys, rounds: 10_000, worryAdjustment: x => x % mod);
    }

    private static Dictionary<int, Monkey> GetMonkeyMap(string input)
    {
        return input.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            .Chunk(6)
            .SelectIndexed()
            .ToDictionary(x => x.Index, x => Monkey.Parse(x.Item));
    }

    private static long GetOutput(Dictionary<int, Monkey> monkeyMap, int rounds, Func<long, long> worryAdjustment)
    {
        for (int i = 0; i < rounds; i++)
        {
            foreach (var monkey in monkeyMap.Values)
            {
                monkey.InspectAndThrowItems(monkeyMap, worryAdjustment);
            }
        }

        return monkeyMap.Values.OrderByDescending(x => x.InspectedItems).Take(2).Aggregate(1L, (x, y) => x *= y.InspectedItems);
    }

    [DebuggerDisplay("{DebuggerDisplayValue,nq}")]
    private class Monkey
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplayValue => $"{_queue.Cast<long>().Select(x => x.ToString()).JoinWithComma()} - {InspectedItems}";

        private Queue<long> _queue;
        public Func<long, long, long> _operation;
        private long _operand;
        private int _throwsToWhenTrue;
        private int _throwsToWhenFalse;

        public long Divisor;
        public long InspectedItems { get; private set; }

        public static Monkey Parse(string[] lines)
        {
            var match = Regex.Match(lines[2], @"old (?<operator>\+|\*) (?<operand>old|\d+)");

            return new Monkey
            {
                _queue = new Queue<long>(Regex.Matches(lines[1], @"\d+").Select(x => x.Value).Select(long.Parse)),
                _operation = match.Groups["operator"].Value == "+" ? (a, b) => a + b : (a, b) => a * b,
                _operand = match.Groups["operand"].Value == "old" ? long.MinValue : long.Parse(match.Groups["operand"].Value),
                _throwsToWhenTrue = int.Parse(Regex.Match(lines[4], @"\d+").Value),
                _throwsToWhenFalse = int.Parse(Regex.Match(lines[5], @"\d+").Value),

                Divisor = int.Parse(Regex.Match(lines[3], @"\d+").Value)
            };
        }

        public void InspectAndThrowItems(Dictionary<int, Monkey> monkeyMap, Func<long, long> worryAdjustment)
        {
            while (_queue.TryDequeue(out var item))
            {
                InspectedItems++;

                var operand = _operand == long.MinValue ? item : _operand;

                var worryLevel = worryAdjustment(_operation(item, operand));

                if (worryLevel % Divisor == 0)
                {
                    monkeyMap[_throwsToWhenTrue].Catch(worryLevel);
                }
                else
                {
                    monkeyMap[_throwsToWhenFalse].Catch(worryLevel);
                }
            }
        }

        public void Catch(long item)
        {
            _queue.Enqueue(item);
        }
    }
}
