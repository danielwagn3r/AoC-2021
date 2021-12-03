// See https://aka.ms/new-console-template for more information
Console.WriteLine("AoC 2021 Day 1");

var input = (await File.ReadAllLinesAsync("input.txt")).Select(line => int.Parse(line)).ToArray();

Console.WriteLine($"One: {PuzzleOne(input)}");
Console.WriteLine($"Two: {PuzzleTwo(input)}");

int PuzzleOne(int[] input)
{
    return input.Select((m, i) => (i != 0 && m > input[i - 1]) ? 1 : 0).Sum();
}

int PuzzleTwo(int[] input)
{
    var windows = GetWindows(input);

    return PuzzleOne(windows);
}

int[] GetWindows(int[] input)
{
    return input.SkipLast(2).Select((m, i) => m + input[i + 1] + input[i + 2]).ToArray();
}