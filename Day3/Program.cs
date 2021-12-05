// See https://aka.ms/new-console-template for more information
Console.WriteLine("AoC 2021 Day 3");

var input = (await File.ReadAllLinesAsync("input.txt")).ToArray();

Console.WriteLine($"One: {PuzzleOne(input)}");
Console.WriteLine($"Two: {PuzzleTwo(input)}");

int PuzzleOne(string[] input)
{
    int gamma;
    int epsilon;

    string[] trans = Transpose(input);

    var gammaStr = new string(trans.Select(l =>
        new
        {
            Zeros = l.Where(b => b == '0').Count(),
            Ones = l.Where(b => b == '1').Count()
        }).Select(t => t.Ones > t.Zeros ? '1' : '0').ToArray());

    var epsilonStr = new string(trans.Select(l =>
        new
        {
            Zeros = l.Where(b => b == '0').Count(),
            Ones = l.Where(b => b == '1').Count()
        }).Select(t => t.Ones < t.Zeros ? '1' : '0').ToArray());

    gamma = Convert.ToInt32(gammaStr, 2);
    epsilon = Convert.ToInt32(epsilonStr, 2);

    return gamma * epsilon;
}

int PuzzleTwo(string[] input)
{
    int oxygen;
    int co2Scrubber;

    var search = input;

    for (int i = 0; i < input[0].Length && search.Length > 1; i++)
    {
        string[] trans = Transpose(search);

        var bitCounts = trans.Select(l => new { Zeros = l.Where(b => b == '0').Count(), Ones = l.Where(b => b == '1').Count() }).ToArray();
        var mcValue = bitCounts.Select(c => c.Ones >= c.Zeros ? '1' : '0').ToArray();

        search = search.Where(b => b[i] == mcValue[i]).ToArray();
    }

    oxygen = Convert.ToInt32(search[0], 2);

    search = input;

    for (int i = 0; i < input[0].Length && search.Length > 1; i++)
    {
        string[] trans = Transpose(search);

        var bitCounts = trans.Select(l => new { Zeros = l.Where(b => b == '0').Count(), Ones = l.Where(b => b == '1').Count() }).ToArray();
        var lcValue = bitCounts.Select(c => c.Ones < c.Zeros ? '1' : '0').ToArray();

        search = search.Where(b => b[i] == lcValue[i]).ToArray();
    }

    co2Scrubber = Convert.ToInt32(search[0], 2);

    return oxygen * co2Scrubber;
}

string[] Transpose(string[] input)
{
    string[] trans = new string[input[0].Length];

    for (int i = 0; i < input[0].Length; i++)
    {
        trans[i] = new string(input.Select(x => x[i]).ToArray());
    }

    return trans;
}