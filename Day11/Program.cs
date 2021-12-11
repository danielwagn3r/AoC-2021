// See https://aka.ms/new-console-template for more information
Console.WriteLine("AoC 2021 Day 11");

var input = (await File.ReadAllLinesAsync("input.txt")).Select(l => l.Select(c => c - '0').ToArray()).ToArray();

var iterations = 100;
var (maxX, maxY) = GetDimensions(input);

int[,] octos = new int[maxX, maxY];

for (int x = 0; x < maxX; x++)
{
for (int y = 0; y < maxY; y++)
{
octos[x, y] = input[x][y];
}
}

Console.WriteLine($"One: {PuzzleOne(octos)}");
Console.WriteLine($"Two: {PuzzleTwo(octos)}");

int PuzzleOne(int[,] octos)
{
int flashes = 0;

for (int i = 0; i < iterations; i++)
{
bool[,] flashed = new bool[maxX, maxY];
List<Point> flashing = new();

for (int x = 0; x < maxX; x++)
{
for (int y = 0; y < maxY; y++)
{
if (++octos[x, y] > 9)
{
flashing.Add(new Point(x, y));
flashes++;
}
}
}

foreach (var octo in flashing)
{
flashes += FlashOcto(ref octos, ref flashed, octo);
}
}
return flashes;
}

int FlashOcto(ref int[,] octos, ref bool[,] flashed, Point octo)
{
if (flashed[octo.X, octo.Y])
return 0;

List<Point> flashing = new();

if (octos[octo.X, octo.Y] <= 9)
{
if (++octos[octo.X, octo.Y] > 9)
return 1 + FlashOcto(ref octos, ref flashed, octo);

return 0;
}
else
{
int flashes = 0;
octos[octo.X, octo.Y] = 0;
flashed[octo.X, octo.Y] = true;

// Up-Left
if (octo.X > 0 && octo.Y > 0)
{
flashes += FlashOcto(ref octos, ref flashed, new(octo.X - 1, octo.Y - 1));
}

// Up
if (octo.X > 0)
{
flashes += FlashOcto(ref octos, ref flashed, new(octo.X - 1, octo.Y));
}

// Up-Right
if (octo.X > 0 && octo.Y < maxY - 1)
{
flashes += FlashOcto(ref octos, ref flashed, new(octo.X - 1, octo.Y + 1));
}

// Down
if (octo.X < maxX - 1)
{
flashes += FlashOcto(ref octos, ref flashed, new(octo.X + 1, octo.Y));
}

// Down-Left
if (octo.X < maxX - 1 && octo.Y > 0)
{
flashes += FlashOcto(ref octos, ref flashed, new(octo.X + 1, octo.Y - 1));
}

// Down-Right
if (octo.X < maxX - 1 && octo.Y < maxY - 1)
{
flashes += FlashOcto(ref octos, ref flashed, new(octo.X + 1, octo.Y + 1));
}

// Left
if (octo.Y > 0)
{
flashes += FlashOcto(ref octos, ref flashed, new(octo.X, octo.Y - 1));
}

// Right
if (octo.Y < maxY - 1)
{
flashes += FlashOcto(ref octos, ref flashed, new(octo.X, octo.Y + 1));
}

return flashes;
}
}

int PuzzleTwo(int[,] octos)
{
return 0;
}

(int, int) GetDimensions(int[][] input) => (input.Length, input[0].Length);

internal class Point : IEquatable<Point>
{
	public int X { get; set; }
	public int Y { get; set; }

	public Point(int x, int y)
	{
		X = x;
		Y = y;
	}

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