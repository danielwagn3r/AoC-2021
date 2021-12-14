// See https://aka.ms/new-console-template for more information
using System.Text;
using System.Text.RegularExpressions;

Console.WriteLine("AoC 2021 Day 14");

var input = (await File.ReadAllLinesAsync("input.txt"));

var polymeer = input.First();
var rules = input.Skip(2).Select(line => Regex.Split(line, " -> ")).ToDictionary(r => r[0], r => r[1]);

Console.WriteLine($"One: {PuzzleOne(polymeer, rules, 10)}");
Console.WriteLine($"Two: {PuzzleTwo(polymeer, rules, 40)}");

int PuzzleOne(string polymer, IDictionary<string, string> rules, int iterations)
{
    string current = new string(polymer);

    for (int i = 0; i < iterations; i++)
    {
        var pairs = current.Skip(1).Select((e, i) => $"{current[i]}{e}");
        StringBuilder next = new();

        next.Append(pairs.First()[0]);

        foreach (var pair in pairs)
        {
            if (!rules.ContainsKey(pair))
            {
                next.Append(pair[1]);
            }
            else
            {
                next.Append(rules[pair]);
                next.Append(pair[1]);
            }
        }

        current = next.ToString();
    }

    var analysis = current.GroupBy(e => e)
        .Select(g => new { Element = g.Key, Count = g.Count() })
        .OrderBy(g => g.Count);

    return analysis.Last().Count - analysis.First().Count;
}

long PuzzleTwo(string polymer, IDictionary<string, string> rules, int iterations)
{
    Dictionary<char, long> charCount = new();
    Dictionary<string, long> pairCount = new();

    charCount = polymer
        .GroupBy(g => g)
        .ToDictionary(g => g.Key, g => (long)g.Count());

    pairCount = polymer.Skip(1)
        .Select((e, i) => $"{polymer[i]}{e}")
        .GroupBy(g => g)
        .ToDictionary(g => g.Key, g => (long)g.Count());

    for (int i = 0; i < iterations; i++)
    {
        Dictionary<string, long> nextPairCount = new();
        foreach (var pair in pairCount)
        {
            if (rules.ContainsKey(pair.Key))
            {
                string before = new string($"{pair.Key[0]}{rules[pair.Key]}");
                string after = new string($"{rules[pair.Key]}{pair.Key[1]}");

                charCount[rules[pair.Key][0]] = charCount.GetValueOrDefault(rules[pair.Key][0]) + pairCount[pair.Key];
                nextPairCount[before] = pairCount.GetValueOrDefault(pair.Key) + nextPairCount.GetValueOrDefault(before);
                nextPairCount[after] = pairCount.GetValueOrDefault(pair.Key) + nextPairCount.GetValueOrDefault(after);
            }
            else
                nextPairCount[pair.Key] = pairCount.GetValueOrDefault(pair.Key);
        }

        pairCount = nextPairCount;
    }

    var sorted = charCount.OrderBy(p => p.Value);

    return sorted.Last().Value - sorted.First().Value;
}