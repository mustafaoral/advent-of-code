namespace AdventOfCode2023;

public class Day07 : IStringInputIntegerOutputPuzzle
{
    public int Part1(string input) => GetWinnings(input, joker: '*');
    public int Part2(string input) => GetWinnings(input, joker: 'J');

    private static int GetWinnings(string input, char joker)
    {
        var hands = input.Split(Environment.NewLine)
            .Select(line =>
            {
                var tokens = line.Split(" ");

                return new Hand(tokens[0], int.Parse(tokens[1]), joker);
            })
            .ToArray();

        return hands
            .OrderBy(x => x.Kind)
            .ThenBy(x => x.ComparisonValue)
            .Select((x, i) => x.Bid * (i + 1))
            .Sum();
    }

    private enum HandKind
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }

    private class Hand
    {
        private static readonly Dictionary<char, char> _replacementMap = new()
        {
            ['2'] = 'b',
            ['3'] = 'c',
            ['4'] = 'd',
            ['5'] = 'e',
            ['6'] = 'f',
            ['7'] = 'g',
            ['8'] = 'h',
            ['9'] = 'i',
            ['T'] = 'j',
            ['J'] = 'k',
            ['Q'] = 'l',
            ['K'] = 'm',
            ['A'] = 'n',
        };

        private static readonly Dictionary<char, char> _jokerReplacementMap = new()
        {
            ['J'] = 'a',
            ['2'] = 'b',
            ['3'] = 'c',
            ['4'] = 'd',
            ['5'] = 'e',
            ['6'] = 'f',
            ['7'] = 'g',
            ['8'] = 'h',
            ['9'] = 'i',
            ['T'] = 'j',
            ['Q'] = 'l',
            ['K'] = 'm',
            ['A'] = 'n',
        };

        public HandKind Kind { get; private init; }
        public int Bid { get; private init; }
        public string ComparisonValue { get; private init; }

        public Hand(string cards, int bid, char joker)
        {
            Kind = GetHandKind();
            Bid = bid;

            var jokers = cards.Count(x => x == joker);
            if (jokers == 0)
            {
                ComparisonValue = new string(cards.Select(x => _replacementMap[x]).ToArray());
            }
            else
            {
                Kind = (Kind, jokers) switch
                {
                    (HandKind.FiveOfAKind, _) => HandKind.FiveOfAKind,
                    (HandKind.FourOfAKind, _) => HandKind.FiveOfAKind,
                    (HandKind.FullHouse, _) => HandKind.FiveOfAKind,
                    (HandKind.ThreeOfAKind, _) => HandKind.FourOfAKind,
                    (HandKind.TwoPair, 1) => HandKind.FullHouse,
                    (HandKind.TwoPair, _) => HandKind.FourOfAKind,
                    (HandKind.OnePair, _) => HandKind.ThreeOfAKind,
                    (HandKind.HighCard, _) => HandKind.OnePair,
                    _ => throw new NotImplementedException(),
                };

                ComparisonValue = new string(cards.Select(x => _jokerReplacementMap[x]).ToArray());
            }

            HandKind GetHandKind()
            {
                var cardGrouping = cards.GroupBy(x => x).ToArray();

                if (cardGrouping.Length == 1)
                {
                    return HandKind.FiveOfAKind;
                }

                if (cardGrouping.Length == 2)
                {
                    if (cardGrouping.Any(x => x.Count() == 3))
                    {
                        return HandKind.FullHouse;
                    }

                    return HandKind.FourOfAKind;
                }

                if (cardGrouping.Length == 3)
                {
                    if (cardGrouping.Any(x => x.Count() == 3))
                    {
                        return HandKind.ThreeOfAKind;
                    }

                    return HandKind.TwoPair;
                }

                if (cardGrouping.Length == 4)
                {
                    return HandKind.OnePair;
                }

                return HandKind.HighCard;
            }
        }
    }
}
