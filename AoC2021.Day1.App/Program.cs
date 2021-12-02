// See https://aka.ms/new-console-template for more information
using AoC2021.Common;

string sessionToken = "53616c7465645f5fa8e69a3592c87eaa604b1c639256aa8ca1e07c56f6f025c624065bea1ec0e4758e2717813b4c799e";

var client = new AoCClient(sessionToken);
var result = await client.GetInput(1, 2021);

var input = result.GetLines(false).Select(line => int.Parse(line)).ToArray();

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