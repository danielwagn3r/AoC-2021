// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("AoC 2021 Day 4");

var input = (await File.ReadAllLinesAsync("input.txt")).Select(l => l.TrimStart()).ToArray();

var boardDim = Regex.Split(input[2], " +").Length;

var boardNum = input.Skip(1).Count() / (boardDim + 1);

var draw = input[0].Split(',').Select(n => Convert.ToInt32(n)).ToArray();

var boards = ParseBoards();

Console.WriteLine($"One: {PuzzleOne(draw, boards)}");

boards = ParseBoards();

Console.WriteLine($"Two: {PuzzleTwo(draw, boards)}");

List<IEnumerable<Element>> ParseBoards()
{
    var boards = new List<IEnumerable<Element>>();

    for (int i = 0; i < boardNum; i++)
    {
        var elements = input.Skip(i * (boardDim + 1) + 2)
            .Take(boardDim)
            .Select((l, r) =>
                Regex.Split(l, " +").Select((n, c) => new Element { Value = Convert.ToInt32(n), X = r, Y = c, Marked = false })).SelectMany(x => x).ToArray();

        boards.Add(elements);
    }

    return boards;
}
int PuzzleOne(int[] draw, IList<IEnumerable<Element>> boards)
{
    var rows = new int[boardNum, boardDim];
    var cols = new int[boardNum, boardDim];
    bool finished = false;
    int lastDraw = 0;
    int lastBoard = 0;

    foreach (int d in draw)
    {
        for (int bi = 0; bi < boardNum; bi++)
        {
            boards[bi].Select((e, i) => new { e, i })
                .Where(ie => ie.e.Value == d)
                .AsParallel().ForAll(x =>
            {
                x.e.Marked = true;
                if (++rows[bi, x.e.Y] >= boardDim) { finished = true; lastBoard = bi; }
                if (++cols[bi, x.e.X] >= boardDim) { finished = true; lastBoard = bi; }
            });
        }

        if (finished)
        {
            lastDraw = d;
            break;
        }
    }

    return boards[lastBoard].Where(e => !e.Marked).Select(e => e.Value).Sum() * lastDraw;
}

int PuzzleTwo(int[] draw, IList<IEnumerable<Element>> boards)
{
    var rows = new int[boardNum, boardDim];
    var cols = new int[boardNum, boardDim];
    int[] finishedBoards = new int[boardNum];
    int lastDraw = 0;
    int lastBoard = 0;

    foreach (int d in draw)
    {
        for (int bi = 0; bi < boardNum; bi++)
        {
            if (finishedBoards[bi] == 1)
                continue;

            boards[bi].Select((e, i) => new { e, i })
                .Where(ie => ie.e.Value == d)
                .AsParallel().ForAll(x =>
                {
                    x.e.Marked = true;
                    if (++rows[bi, x.e.Y] >= boardDim) { finishedBoards[bi] = 1; lastBoard = bi; }
                    if (++cols[bi, x.e.X] >= boardDim) { finishedBoards[bi] = 1; lastBoard = bi; }
                });
        }

        if (finishedBoards.Sum() >= boardNum)
        {
            lastDraw = d;
            break;
        }
    }

    return boards[lastBoard].Where(e => !e.Marked).Select(e => e.Value).Sum() * lastDraw;
}

internal class Element
{
    public int Value { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public bool Marked { get; set; }
}