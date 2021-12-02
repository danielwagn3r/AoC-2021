// See https://aka.ms/new-console-template for more information

var input = (await File.ReadAllLinesAsync("input.txt")).Select(line => int.Parse(line)).ToArray();

Console.WriteLine($"One: {PuzzleOne(input)}");
Console.WriteLine($"Two: {PuzzleTwo(input)}");

int PuzzleOne(int[] input)
{
    return input.Zip(input.Skip(1), (a, b) => b > a ? 1 : 0).Sum();
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