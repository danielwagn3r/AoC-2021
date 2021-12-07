// See https://aka.ms/new-console-template for more information
Console.WriteLine("AoC 2021 Day 7");

var input = (await File.ReadAllLinesAsync("input.txt")).First().Split(',').Select(line => int.Parse(line)).ToArray();

Console.WriteLine($"One: {PuzzleOne(input)}");
Console.WriteLine($"Two: {PuzzleTwo(input)}");

int PuzzleOne(int[] input)
{
	var positions = input.Distinct();

	var fuel = new Dictionary<int, int>();

	foreach (var pos in positions)
	{
		fuel.Add(pos, input.Select(i => Math.Abs(i - pos)).Sum());
	}

	return fuel.OrderBy(f => f.Value).FirstOrDefault().Value;
}

int PuzzleTwo(int[] input)
{
	var positions = Enumerable.Range(input.Min(), input.Max());

	var fuel = new Dictionary<int, int>();

	foreach (var pos in positions)
	{
		fuel.Add(pos, input.Select(i => Enumerable.Range(0, Math.Abs(i - pos) + 1).Sum()).Sum());
	}

	return fuel.OrderBy(f => f.Value).FirstOrDefault().Value;
}
