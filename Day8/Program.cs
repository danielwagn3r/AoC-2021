// See https://aka.ms/new-console-template for more information
Console.WriteLine("AoC 2021 Day 8");

var input = (await File.ReadAllLinesAsync("input.txt")).ToArray();

Console.WriteLine($"One: {PuzzleOne(input)}");
Console.WriteLine($"Two: {PuzzleTwo(input)}");

int PuzzleOne(string[] input)
{
    var parsed = input.Select(l => l.Split('|').Select(o => o.Trim().Split(' ')).ToArray()).Select(o => new { Signals = o[0], Outputs = o[1] });

    var outputs = parsed.Select(p => p.Outputs).ToArray();

    var unique = outputs.Select(o => o.Where(d => d.Length == 2 || d.Length == 4 || d.Length == 3 || d.Length == 7));

    return unique.Select(o => o.Count()).Sum(); ;
}

int PuzzleTwo(string[] input)
{
    return 0;
}