// See https://aka.ms/new-console-template for more information
Console.WriteLine("AoC 2021 Day 2");

var input = (await File.ReadAllLinesAsync("input.txt")).ToArray();

Console.WriteLine($"One: {PuzzleOne(input)}");
Console.WriteLine($"Two: {PuzzleTwo(input)}");

int PuzzleOne(string[] input)
{
    var commands = input.Select(l => l.Split(' ')).Select(t => new { Direction = t[0], Step = int.Parse(t[1]) });

    int position = 0;
    int depth = 0;

    foreach (var move in commands)
    {
        switch (move.Direction)
        {
            case "forward":
                position += move.Step;
                break;

            case "up":
                depth -= move.Step;
                break;

            case "down":
                depth += move.Step;
                break;

            default:
                throw new InvalidOperationException();
        }
    }

    return position * depth;
}

int PuzzleTwo(string[] input)
{
    var commands = input.Select(l => l.Split(' ')).Select(t => new { Direction = t[0], Step = int.Parse(t[1]) });

    int position = 0;
    int depth = 0;
    int aim = 0;

    foreach (var move in commands)
    {
        switch (move.Direction)
        {
            case "forward":
                position += move.Step;
                depth += aim * move.Step;
                break;

            case "up":
                aim -= move.Step;
                break;

            case "down":
                aim += move.Step;
                break;

            default:
                throw new InvalidOperationException();
        }
    }

    return position * depth;
}