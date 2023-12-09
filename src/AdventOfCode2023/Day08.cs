namespace AdventOfCode2023;

public class Day08 : IPuzzle<string, long, string, long>
{
    public long Part1(string input)
    {
        var steps = input.Split(Environment.NewLine)[0].ToCharArray();
        var nodes = GetNodes(input);

        var startNode = nodes.First(x => x.Name == "AAA");

        return SolveForNode(startNode, nodes, steps, endStatePredicate: x => x.Name == "ZZZ");
    }

    public long Part2(string input)
    {
        var steps = input.Split(Environment.NewLine)[0].ToCharArray();
        var nodes = GetNodes(input);

        var startNodes = nodes.Where(x => x.Name.EndsWith('A'));

        var stepCounts = startNodes.Select(startNode => SolveForNode(startNode, nodes, steps, endStatePredicate: x => x.Name.EndsWith('Z'))).ToArray();

        return stepCounts.Aggregate(LeastCommonMultiple);

        long GreatestCommongFactor(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        long LeastCommonMultiple(long a, long b)
        {
            return a / GreatestCommongFactor(a, b) * b;
        }
    }

    private record Node(string Name, string Left, string Right);

    private static Node[] GetNodes(string input)
    {
        return input.Split(Environment.NewLine)
            .Skip(2)
            .Select(line =>
            {
                var match = Regex.Match(line, @"(?<name>\w{3}) = \((?<left>\w{3}), (?<right>\w{3})\)");

                return new Node(match.Groups["name"].Value, match.Groups["left"].Value, match.Groups["right"].Value);
            })
            .ToArray();
    }

    private static long SolveForNode(Node node, Node[] nodes, char[] steps, Func<Node, bool> endStatePredicate)
    {
        var stepIndex = 0;
        var stepCount = 0L;

        while (true)
        {
            stepCount++;
            var nextStep = steps[stepIndex++];
            stepIndex %= steps.Length;

            if (nextStep == 'L')
            {
                node = nodes.First(x => x.Name == node.Left);
            }
            else
            {
                node = nodes.First(x => x.Name == node.Right);
            }

            if (endStatePredicate(node))
            {
                break;
            }
        }

        return stepCount;
    }
}
