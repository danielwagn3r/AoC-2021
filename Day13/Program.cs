// See https://aka.ms/new-console-template for more information
using Common;

Console.WriteLine("AoC 2021 Day 13");

var input = (await File.ReadAllLinesAsync("input.txt"));

// Find split line between paper dots and folding instructions
var split = input.Select((l, i) => (string.IsNullOrEmpty(l), i)).Where(r => r.Item1).Select(r => r.Item2).First();

// Parse dots
var dotSpec = input.Take(split).Select(line => line.Split(',')).Select(s => new Point(int.Parse(s[0]), int.Parse(s[1])));
var foldings = input.Skip(split + 1)
    .Select(line => line.Split(' '))
    .Select(i => i[2].Split('='))
    .Select(i => new Folding { Direction = i[0], Line = int.Parse(i[1]) })
    .ToArray();

var (maxX, maxY) = (dotSpec.Select(d => d.X).Max() + 1, dotSpec.Select(d => d.Y).Max() + 1);

bool[,] dots = new bool[maxX, maxY];
foreach (var dot in dotSpec)
    dots[dot.X, dot.Y] = true;

var (visible, _) = Puzzle(dots, foldings, true);

Console.WriteLine($"One: {visible}");

(visible, var newDots) = Puzzle(dots, foldings);
Console.WriteLine($"Two: {visible}");
PlotDots(newDots);

(int visibleDots, bool[,] newDots) Puzzle(bool[,] dots, Folding[] foldings, bool onlyFirst = false)
{
    var maxX = dots.GetLength(0);
    var maxY = dots.GetLength(1);

    var curFolding = foldings.First();
    int visibleDots = 0;

    int newMaxX = 0;
    int newMaxY = 0;
    bool[,] newDots;

    if (curFolding.Direction == "y")
    {
        // Up fold
        newMaxX = maxX;
        newMaxY = curFolding.Line;

        newDots = new bool[newMaxX, newMaxY];

        for (int x = 0; x < newMaxX; x++)
        {
            for (int y = 0; y < newMaxY; y++)
            {
                int h = curFolding.Line * 2 - y;
                //Console.WriteLine($"newDots[{x},{y}] = dots[{x},{y}] | dots[{x},{h}]");
                if (newDots[x, y] = dots[x, y] | dots[x, h])
                    visibleDots++;
            }
        }
    }
    else if (curFolding.Direction == "x")
    {
        // Left fold
        newMaxX = curFolding.Line;
        newMaxY = maxY;

        newDots = new bool[newMaxX, newMaxY];

        for (int x = 0; x < newMaxX; x++)
        {
            for (int y = 0; y < newMaxY; y++)
            {
                int h = curFolding.Line * 2 - x;
                if (newDots[x, y] = dots[x, y] | dots[h, y])
                    visibleDots++;
            }
        }
    }
    else
    {
        // Invalid instruction
        throw new ArgumentException($"Direction {curFolding.Direction} unexpected.");
    }

    if (onlyFirst || foldings.Length == 1)
    {
        return (visibleDots, newDots);
    }
    else
        return Puzzle(newDots, foldings.Skip(1).ToArray());
}

void PlotDots(bool[,] dots)
{
    var maxX = dots.GetLength(0);
    var maxY = dots.GetLength(1);

    for (int y = 0; y < maxY; y++)
    {
        for (int x = 0; x < maxX; x++)
            Console.Write(dots[x, y] ? 'O' : ' ');

        Console.WriteLine();
    }
}

internal class Folding
{
    public string Direction { get; set; }

    public int Line { get; set; }
}