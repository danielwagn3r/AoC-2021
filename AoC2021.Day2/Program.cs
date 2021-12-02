// See https://aka.ms/new-console-template for more information

var input = (await File.ReadAllLinesAsync("input.txt")).ToArray();

Console.WriteLine($"One: {PuzzleOne(input)}");
Console.WriteLine($"Two: {PuzzleTwo(input)}");

int PuzzleOne(string[] input)
{
    var commands = input.Select(l => l.Split(' ')).Select(t => new { Direction = t[0], Step = int.Parse(t[1])});

    int px = 0;
    int py = 0;

    foreach( var move in commands)
    {
        switch(move.Direction)
        {
            case "forward":
                px += move.Step;
                break;
            case "up":
                py -= move.Step;
                break;
            case "down":
                py += move.Step;
                break;
            default:
                throw new InvalidOperationException();
        }
    }

    return px * py;
}

int PuzzleTwo(string[] input)
{
    return 0;
}