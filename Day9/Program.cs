// See https://aka.ms/new-console-template for more information
Console.WriteLine("AoC 2021 Day 9");

var input = (await File.ReadAllLinesAsync("input.txt")).Select(l => l.Select(c => c - '0').ToArray()).ToArray();

Console.WriteLine($"One: {PuzzleOne(input)}");
Console.WriteLine($"Two: {PuzzleTwo(input)}");

(int, int) GetDimensions(int[][] input) => (input.Length, input[0].Length);

int PuzzleOne(int[][] input)
{
    var lowPoints = FindLowPoints(input);
    int riskLevel = 0;

    foreach (var lP in lowPoints)
    {
        riskLevel += input[lP.X][lP.Y] + 1;
    }

    return riskLevel;
}

int PuzzleTwo(int[][] input)
{
    var lowPoints = FindLowPoints(input);

    var basins = lowPoints.Select(lp => FindBasin(input, lp));

    var dBasinsCount = basins.Select(b => b.Distinct().Count()).OrderByDescending(b => b);

    return dBasinsCount.Take(3).Aggregate(1, (x, y) => x * y);
}

IList<Point> FindBasin(int[][] input, Point point)
{
    var (lines, rows) = GetDimensions(input);
    List<Point> basin = new();

    if (input[point.X][point.Y] == 9)
        return basin;

    basin.Add(point);

    if (point.X > 0 && input[point.X][point.Y] < input[point.X - 1][point.Y])
    {
        basin.AddRange(FindBasin(input, new Point { X = point.X - 1, Y = point.Y }));
    }

    if (point.X < lines - 1 && input[point.X][point.Y] < input[point.X + 1][point.Y])
    {
        basin.AddRange(FindBasin(input, new Point { X = point.X + 1, Y = point.Y }));
    }

    if (point.Y > 0 && input[point.X][point.Y] < input[point.X][point.Y - 1])
    {
        basin.AddRange(FindBasin(input, new Point { X = point.X, Y = point.Y - 1 }));
    }

    if (point.Y < rows - 1 && input[point.X][point.Y] < input[point.X][point.Y + 1])
    {
        basin.AddRange(FindBasin(input, new Point { X = point.X, Y = point.Y + 1 }));
    }

    return basin;
}

IList<Point> FindLowPoints(int[][] input)
{
    var (lines, rows) = GetDimensions(input);

    //    Console.WriteLine($"lines: {lines}");
    //    Console.WriteLine($"lines: {rows}");

    List<Point> lowPoints = new();

    for (int i = 0; i < lines; i++)
    {
        for (int j = 0; j < rows; j++)
        {
            if (IsLowPoint(input, i, j))
            {
                //                Console.WriteLine($"lowpoint: ({i},{j}): {input[i][j]}");
                lowPoints.Add(new Point { X = i, Y = j });
            };
        }
    }

    return lowPoints;
}

bool IsLowPoint(int[][] input, int x, int y)
{
    var (lines, rows) = GetDimensions(input);

    if (x > 0 && input[x][y] >= input[x - 1][y])
    {
        return false;
    }

    if (x < lines - 1 && input[x][y] >= input[x + 1][y])
    {
        return false;
    }

    if (y > 0 && input[x][y] >= input[x][y - 1])
    {
        return false;
    }

    if (y < rows - 1 && input[x][y] >= input[x][y + 1])
    {
        return false;
    }

    return true;
}

internal class Point : IEquatable<Point>
{
    public int X { get; set; }
    public int Y { get; set; }

    public override bool Equals(Object obj)
    {
        if (obj == null)
            return false;

        if (obj is not Point point)
            throw new ArgumentException("Object is not a Point.");

        return this.X == point.X && this.Y == point.Y;
    }

    public override int GetHashCode()
    {
        int hashX = X.GetHashCode();

        int hashY = Y.GetHashCode();

        return hashX ^ hashY;
    }

    public bool Equals(Point obj)
    {
        if (obj == null)
            return false;

        return this.X == obj.X && this.Y == obj.Y;
    }
}