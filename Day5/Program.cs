// See https://aka.ms/new-console-template for more information
using Common;
using System.Text.RegularExpressions;

Console.WriteLine("AoC 2021 Day 5");

var input = (await File.ReadAllLinesAsync("input.txt")).ToArray();

Console.WriteLine($"One: {PuzzleOne(input)}");
Console.WriteLine($"Two: {PuzzleTwo(input)}");

int PuzzleOne(string[] input)
{
    var (lines, maxX, maxY) = ParseLines(input);

    var consideredLines = lines.Where(l => l.A.X == l.B.X || l.A.Y == l.B.Y).ToArray();

    return CountOverlaps(consideredLines, maxX, maxY);
}

int PuzzleTwo(string[] input)
{
    var (lines, maxX, maxY) = ParseLines(input);

    return CountOverlaps(lines, maxX, maxY);
}

(Edge[], int, int) ParseLines(string[] input)
{
    Edge[] lines = new Edge[input.Length];
    int maxX = 0;
    int maxY = 0;

    for (int i = 0; i < input.Length; i++)
    {
        var c = Regex.Split(input[i], " -> ")
            .Select(p => p.Split(',').Select(c => int.Parse(c)).ToArray()).ToArray();

        lines[i] = new Edge
        {
            A = new Vertex(c[0][0], c[0][1]),
            B = new Vertex(c[1][0], c[1][1]),
        };

        if (lines[i].A.X > maxX) maxX = lines[i].A.X;
        if (lines[i].B.X > maxX) maxX = lines[i].B.X;
        if (lines[i].A.Y > maxX) maxY = lines[i].A.Y;
        if (lines[i].B.Y > maxX) maxY = lines[i].B.Y;
    }

    return (lines, maxX, maxY);
}

int CountOverlaps(Edge[] lines, int maxX, int maxY)
{
    int[,] diagram = new int[maxX + 1, maxY + 1];
    int count = 0;

    foreach (var line in lines)
    {
        int dx = line.B.X - line.A.X;
        int dy = line.B.Y - line.A.Y;
        int steps;
        int k;

        if (Math.Abs(dx) > Math.Abs(dy))
        {
            steps = Math.Abs(dx);
        }
        else
        {
            steps = Math.Abs(dy);
        }

        double xIncrement = dx / (double)steps;
        double yIncrement = dy / (double)steps;

        double x = line.A.X;
        double y = line.A.Y;

        if (++diagram[(int)Math.Round(x), (int)Math.Round(y)] == 2) count++;

        for (k = 0; k < steps; k++)
        {
            x += xIncrement;
            y += yIncrement;
            if (++diagram[(int)Math.Round(x), (int)Math.Round(y)] == 2) count++;
        }
    }

    return count;
}