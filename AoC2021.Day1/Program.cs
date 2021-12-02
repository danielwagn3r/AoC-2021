﻿// See https://aka.ms/new-console-template for more information

var input = (await File.ReadAllLinesAsync("input.txt")).Select(line => int.Parse(line)).ToArray();

Console.WriteLine($"One: {PuzzleOne(input)}");
Console.WriteLine($"Two: {PuzzleTwo(input)}");

int PuzzleOne(int[] input)
{
    int count = 0;

    for (int i = 1; i < input.Length; i++)
    {
        if (input[i] > input[i - 1])
        {
            count++;
        }
    }

    return count;
}

int PuzzleTwo(int[] input)
{
    var windows = GetWindows(input);

    return PuzzleOne(windows);
}

int[] GetWindows(int[] input)
{
    var r2 = input.Zip(input.Skip(1), (a, b) => Tuple.Create(a, b));
    var result = r2.Zip(input.Skip(2), (a, b) => Tuple.Create(a.Item1, a.Item2, b));

    var windows = result.Select(t => t.Item1 + t.Item2 + t.Item3);

    return windows.ToArray();
}