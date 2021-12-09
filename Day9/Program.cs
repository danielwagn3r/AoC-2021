// See https://aka.ms/new-console-template for more information
Console.WriteLine("AoC 2021 Day 9");

var input = (await File.ReadAllLinesAsync("input.txt")).Select(l => l.Select(c => c - '0').ToArray()).ToArray();

Console.WriteLine($"One: {PuzzleOne(input)}");
Console.WriteLine($"Two: {PuzzleTwo(input)}");

(int, int) GetDimensions(int[][] input) => (input.Length, input[0].Length);

int PuzzleOne(int[][] input)
{
    var (lines, rows) = GetDimensions(input);

    Console.WriteLine($"lines: {lines}");
    Console.WriteLine($"lines: {rows}");

    int riskLevel = 0;
    for (int i = 0; i < lines; i++)
    {
        for (int j = 0; j < rows; j++)
        {
            if (IsLowPoint(input, i, j))
            {
                Console.WriteLine($"lowpoint: ({i},{j}): {input[i][j]}");
                riskLevel += input[i][j] + 1;
            };
        }
    }

    return riskLevel;
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

int PuzzleTwo(int[][] input)
{
    return 0;
}