using System.Diagnostics;

namespace AdventOfCode2022;

public class Day07 : IStringInputIntegerOutputPuzzle
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
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var lineIndex = 1;
        var root = new ElfDirectory("/", null);
        var current = root;

        while (lineIndex < lines.Length)
        {
            var command = lines[lineIndex++][2..];

            if (command.StartsWith("ls"))
            {
                var contents = lines.Skip(lineIndex).TakeWhile(x => x[0] != '$').ToArray();

                foreach (var content in contents)
                {
                    var tokens = content.Split(" ");

                    if (content.StartsWith("dir"))
                    {
                        current.Directories.Add(new ElfDirectory(tokens[1], current));
                    }
                    else
                    {
                        current.Files.Add(new ElfFile(tokens[1], int.Parse(tokens[0])));
                    }
                }

                lineIndex += contents.Length;
            }
            else if (command.StartsWith("cd"))
            {
                var target = command[3..];

                if (target == "..")
                {
                    current = current.Parent;
                }
                else
                {
                    current = current.Directories.Single(x => x.Name == target);
                }
            }
        }

        return root;
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
