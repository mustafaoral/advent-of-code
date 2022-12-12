using System.Diagnostics;

namespace AdventOfCode2022;

public class Day07Alternative : IStringInputIntegerOutputPuzzle
{
    public int Part1(string input)
    {
        return GetRoot(input).GetAllSubDirectories().Where(x => x.TotalSize <= 100_000).Sum(x => x.TotalSize);
    }

    public int Part2(string input)
    {
        var root = GetRoot(input);

        var freeSize = 70_000_000 - root.TotalSize;
        var desiredSize = 30_000_000;

        return root.GetAllSubDirectories().OrderBy(x => x.TotalSize).First(x => freeSize + x.TotalSize >= desiredSize).TotalSize;
    }

    private static ElfDirectory GetRoot(string input)
    {
        return input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Skip(1)
            .Aggregate(seed: new ElfDirectory("/", null), (accumulate, line) =>
            {
                if (line.StartsWith("$"))
                {
                    var command = line[2..];

                    if (command.StartsWith("cd"))
                    {
                        var target = command[3..];

                        if (target == "..")
                        {
                            accumulate = accumulate.Parent;
                        }
                        else
                        {
                            accumulate = accumulate.Directories.Single(x => x.Name == target);
                        }
                    }
                }
                else
                {
                    var tokens = line.Split(" ");

                    if (line.StartsWith("dir"))
                    {
                        accumulate.Directories.Add(new ElfDirectory(tokens[1], accumulate));
                    }
                    else
                    {
                        accumulate.Files.Add(new ElfFile(tokens[1], int.Parse(tokens[0])));
                    }
                }

                return accumulate;
            }, x =>
            {
                while (x.Parent != null)
                {
                    x = x.Parent;
                }

                return x;
            });
    }

    [DebuggerDisplay("{DebuggerDisplayValue,nq}")]
    private class ElfDirectory
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplayValue => $"{Name} - D:{Directories.Count} F:{Files.Count} T:{TotalSize}";

        public string Name { get; }
        public ElfDirectory Parent { get; }
        public List<ElfDirectory> Directories { get; } = new();
        public List<ElfFile> Files { get; } = new();
        public int TotalSize => Files.Sum(x => x.Size) + Directories.Sum(x => x.TotalSize);

        public ElfDirectory(string name, ElfDirectory parent)
        {
            Name = name;
            Parent = parent;
        }

        public List<ElfDirectory> GetAllSubDirectories() => Directories.Union(Directories.SelectMany(x => x.GetAllSubDirectories())).ToList();
    }

    [DebuggerDisplay("{DebuggerDisplayValue,nq}")]
    private class ElfFile
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplayValue => $"{Name} - {Size}";

        public string Name { get; }
        public int Size { get; }

        public ElfFile(string name, int size)
        {
            Name = name;
            Size = size;
        }
    }
}
