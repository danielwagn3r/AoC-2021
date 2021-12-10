// See https://aka.ms/new-console-template for more information
Console.WriteLine("AoC 2021 Day 10");

var input = (await File.ReadAllLinesAsync("input.txt")).ToArray();

Dictionary<char, char> dict = new()
{
    { '(', ')' },
    { '[', ']' },
    { '{', '}' },
    { '<', '>' }
};

Dictionary<char, int> syntaxPoints = new()
{
    { ')', 3 },
    { ']', 57 },
    { '}', 1197 },
    { '>', 25137 }
};

Dictionary<char, int> completionPoints = new()
{
    { ')', 1 },
    { ']', 2 },
    { '}', 3 },
    { '>', 4 }
};

Console.WriteLine($"One: {PuzzleOne(input)}");
Console.WriteLine($"Two: {PuzzleTwo(input)}");

int PuzzleOne(string[] input)
{
    List<char> errors = new();

    foreach (var line in input)
    {
        Stack<char> chunks = new();

        foreach (var c in line)
        {
            if (dict.ContainsKey(c))
            {
                chunks.Push(c);
            }
            else if (dict.ContainsValue(c))
            {
                var top = chunks.Peek();

                if (Closes(top, c))
                {
                    chunks.Pop();
                }
                else
                {
                    errors.Add(c);
                    break;
                }
            }
            else
                throw new ArgumentException($"Unexpected chunk '{c}' detected.");
        }
    }

    return errors.Select(e => syntaxPoints[e]).Sum();
}

bool Closes(char chunk, char match)
{
    if (!dict.ContainsKey(chunk))
        return false;

    return dict[chunk] == match;
}

long PuzzleTwo(string[] input)
{
    List<long> points = new();

    foreach (var line in input)
    {
        bool currupted = false;
        Stack<char> chunks = new();

        foreach (var c in line)
        {
            if (dict.ContainsKey(c))
            {
                chunks.Push(c);
            }
            else if (dict.ContainsValue(c))
            {
                var top = chunks.Peek();

                if (Closes(top, c))
                {
                    chunks.Pop();
                }
                else
                {
                    currupted = true;
                    break;
                }
            }
            else
                throw new ArgumentException($"Unexpected chunk '{c}' detected.");
        }

        if (!currupted)
        {
            long score = 0;

            foreach (var c in chunks)
            {
                score *= 5;
                score += completionPoints[dict[c]];
            }

            points.Add(score);
        }
    }

    return points.OrderBy(p => p).ElementAt(points.Count / 2);
}