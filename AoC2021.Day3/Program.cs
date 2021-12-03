// See https://aka.ms/new-console-template for more information
Console.WriteLine("AoC 2021 Day 3");

var input = (await File.ReadAllLinesAsync("input.txt")).ToArray();

Console.WriteLine($"One: {PuzzleOne(input)}");
//Console.WriteLine($"Two: {PuzzleTwo(input)}");

int PuzzleOne(string[] input)
{
    int gamma;
    int epsilon;

    string[] trans = new string[input[0].Length];

    for (int i = 0; i < input[0].Length; i++)
    {
        trans[i] = new string(input.Select(x => x[i]).ToArray());
    }


    var gammaStr = new string(trans.Select(l => new { Zeros = l.Where(b => b == '0').Count(), Ones = l.Where(b => b == '1').Count() }).Select(t => t.Ones > t.Zeros ? '1' : '0').ToArray());
    var epsilonStr = new string(trans.Select(l => new { Zeros = l.Where(b => b == '0').Count(), Ones = l.Where(b => b == '1').Count() }).Select(t => t.Ones < t.Zeros ? '1' : '0').ToArray());

    gamma = Convert.ToInt32(gammaStr, 2);
    epsilon = Convert.ToInt32(epsilonStr, 2);

    return gamma * epsilon;
}

int PuzzleTwo(string[] input)
{
    throw new NotImplementedException();
}