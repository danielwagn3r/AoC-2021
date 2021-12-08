// See https://aka.ms/new-console-template for more information
Console.WriteLine("AoC 2021 Day 8");

var input = (await File.ReadAllLinesAsync("input.txt")).ToArray();

Console.WriteLine($"One: {PuzzleOne(input)}");
Console.WriteLine($"Two: {PuzzleTwo(input)}");

int PuzzleOne(string[] input)
{
    return ParseInput(input)
        .Select(p => p.Outputs)
        .Select(o => o.Where(d => d.Length == 2 || d.Length == 4 || d.Length == 3 || d.Length == 7))
        .Select(o => o.Count())
        .Sum();
}

long PuzzleTwo(string[] input)
{
    var notes = ParseInput(input);

    long result = 0;

    foreach (var note in notes)
    {
        long number = 0;

        var dict = ParseSignalsOfNote(note);

        foreach (var signal in note.Outputs)
        {
            int i;

            for (i = 0; i < 10; i++)
            {
                if (signal.Length == dict[i].Length && dict[i].FitsOn(signal))
                    break;
            }

            number = number * 10 + i;
        }

        result += number;
    }

    return result;
}

IEnumerable<Note> ParseInput(string[] input) =>
    input.Select(l => l.Split('|')
        .Select(o => o.Trim().Split(' '))
        .ToArray())
        .Select(o => new Note { Signals = o[0], Outputs = o[1] });

string[] ParseSignalsOfNote(Note note)
{
    string[] dict = new string[10];

    // Digits 1, 4, 7 and 8
    foreach (var signal in note.Signals)
    {
        switch (signal.Length)
        {
            case 2:
                dict[1] = signal;
                break;

            case 3:
                dict[7] = signal;
                break;

            case 4:
                dict[4] = signal;
                break;

            case 7:
                dict[8] = signal;
                break;
        }
    }

    // Digit 9
    foreach (var signal in note.Signals)
    {
        if (signal.Length == 6 && dict[4].FitsOn(signal))
        {
            dict[9] = signal;
            break;
        }
    }

    // Digit 3
    foreach (var signal in note.Signals)
    {
        if (signal.Length == 5 && signal.FitsOn(dict[9]) && dict[1].FitsOn(signal))
        {
            dict[3] = signal;
            break;
        }
    }

    // Digit 5
    foreach (var signal in note.Signals)
    {
        if (signal.Length == 5 && signal.FitsOn(dict[9]) && signal != dict[3])
        {
            dict[5] = signal;
            break;
        }
    }

    // Digit 2
    foreach (var signal in note.Signals)
    {
        if (signal.Length == 5 && dict[5] != signal && dict[3] != signal)
        {
            dict[2] = signal;
            break;
        }
    }

    // Digit 0
    foreach (var signal in note.Signals)
    {
        if (signal.Length == 6 && dict[1].FitsOn(signal) && signal != dict[9])
        {
            dict[0] = signal;
            break;
        }
    }

    // Digit 6
    foreach (var signal in note.Signals)
    {
        if (signal.Length == 6 && signal != dict[0] && signal != dict[9])
        {
            dict[6] = signal;
            break;
        }
    }

    return dict;
}

internal class Note

{
    public string[] Signals { get; set; }

    public string[] Outputs { get; set; }
}