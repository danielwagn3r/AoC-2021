// See https://aka.ms/new-console-template for more information
using Common;

Console.WriteLine("AoC 2021 Day 15");

Vertex[] neighbors = { new Vertex(1, 0), new Vertex(0, 1), new Vertex(-1, 0), new Vertex(0, -1) };

var input = (await File.ReadAllLinesAsync("input.txt")).Select(l => l.Select(c => c - '0').ToArray()).ToArray();

Console.WriteLine($"One: {PuzzleOne(input)}");
Console.WriteLine($"Two: {PuzzleTwo(input)}");

(int maxX, int maxY) GetDimensions(int[][] input) => (input.Length, input[0].Length);

int PuzzleOne(int[][] input)
{
    return Dijkstra(input);
}

int PuzzleTwo(int[][] input)
{
    var eInput = EnlargeInput(input, 5);

    return Dijkstra(eInput);
}

Vertex GetMinimumVertex(int[][] dist, bool[][] q)
{
    var minDist = int.MaxValue;
    Vertex v = new(0, 0);

    for (int x = 0; x < q.Length; x++)
    {
        for (int y = 0; y < q[x].Length; y++)
        {
            if (!q[x][y] && dist[x][y] < minDist)
            {
                minDist = dist[x][y];
                v.X = x;
                v.Y = y;
            }
        }
    }

    return v;
}

int Dijkstra(int[][] input)
{
    Console.WriteLine("Dijkstra");
    var (maxX, maxY) = GetDimensions(input);
    int[][] dist = new int[maxX][];
    bool[][] q = new bool[maxX][];

    for (int x = 0; x < maxX; x++)
    {
        dist[x] = new int[maxY];
        q[x] = new bool[maxY];

        for (int y = 0; y < maxY; y++)
        {
            dist[x][y] = int.MaxValue;
            q[x][y] = false;
        }
    }

    dist[0][0] = 0;

    while (true)
    {
        var p = GetMinimumVertex(dist, q);
        //Console.WriteLine($"p: {p.X},{p.Y}");

        q[p.X][p.Y] = true;

        foreach (var n in neighbors)
        {
            // potential neighbor vertex
            var potN = new Vertex(p.X + n.X, p.Y + n.Y);
            //Console.WriteLine($"potN: {potN.X},{potN.Y}");

            if (potN.X < 0 || potN.Y < 0 || potN.X >= maxX || potN.Y >= maxY)
                continue;

            var alt = dist[p.X][p.Y] + input[potN.X][potN.Y];
            if (alt < dist[potN.X][potN.Y])
            {
                dist[potN.X][potN.Y] = alt;
            }

            if (potN.X == maxX - 1 && potN.Y == maxY - 1)
                return dist[maxX - 1][maxY - 1];
        }
    }
}

int[][] EnlargeInput(int[][] input, int factor)
{
    Console.WriteLine("EnlargeInput");

    var (maxX, maxY) = GetDimensions(input);
    var (eMaxX, eMaxY) = (maxX * factor, maxY * factor);

    int[][] eInput = new int[eMaxX][];

    // Create rows
    for (int x = 0; x < eMaxX; x++)
    {
        eInput[x] = new int[eMaxY];
    }

    // Copy original input
    for (int x = 0; x < maxX; x++)
    {
        for (int y = 0; y < maxY; y++)
        {
            eInput[x][y] = input[x][y];
        }
    }

    // First line of tiles
    for (int i = 1; i < factor; i++)
    {
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                int n = eInput[x][y + maxY * (i - 1)] + 1;
                if (n > 9) n = 1;

                eInput[x][y + maxY * i] = n;
            }
        }
    }

    // Remaining lines of tiles
    for (int i = 1; i < factor; i++)
    {
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < eMaxY; y++)
            {
                int n = eInput[x + maxX * (i - 1)][y] + 1;
                if (n > 9) n = 1;

                eInput[x + maxX * i][y] = n;
            }
        }
    }

    return eInput;
}