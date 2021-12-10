// See https://aka.ms/new-console-template for more information
Console.WriteLine("AoC 2021 Day 10");

var input = (await File.ReadAllLinesAsync("input.txt")).ToArray();

Dictionary<char, char> dict = new Dictionary<char, char>()
{
    { '(', ')' },
    { '[', ']' },
    { '{', '}' },
    { '<', '>'}
};

Dictionary<char, int> points = new Dictionary<char, int>()
{
    { ')', 3 },
    { ']', 57 },
    { '}', 1197 },
    { '>', 25137 }
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

    return errors.Select(e => points[e]).Sum(); ;
}

bool Closes(char chunk, char match)
{
    if (!dict.ContainsKey(chunk))
        return false;

    return dict[chunk] == match;
}

int PuzzleTwo(string[] input)
{
    return 0;
}