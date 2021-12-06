// See https://aka.ms/new-console-template for more information

Console.WriteLine("AoC 2021 Day 6");

var input = (await File.ReadAllLinesAsync("input.txt")).First().Split(',').Select(x => int.Parse(x)).ToArray();

Console.WriteLine($"One: {Puzzle(input, 80)}");
Console.WriteLine($"Two: {Puzzle(input, 256)}");

static long Puzzle(int[] input, int days)
{
    IDictionary<int, long> swarm = input.GroupBy(d => d).ToDictionary(i => i.Key, i => (long)i.Count());

    for (int i = 0; i < days; i++)
    {
        swarm = IterateSwarm(swarm);
    }

    return swarm.Values.Sum();
}

static IDictionary<int, long> IterateSwarm(IDictionary<int, long> swarm)
{
    Dictionary<int, long> nextSwarm = new();

    foreach (var e in swarm)
    {
        if (e.Key == 0)
        {
            nextSwarm[6] = nextSwarm.GetValueOrDefault(6) + e.Value;
            nextSwarm[8] = nextSwarm.GetValueOrDefault(8) + e.Value;
        }
        else
        {
            nextSwarm[e.Key - 1] = nextSwarm.GetValueOrDefault(e.Key - 1) + e.Value;
        }
    }

    return nextSwarm;
}