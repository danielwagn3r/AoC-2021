// See https://aka.ms/new-console-template for more information
Console.WriteLine("AoC 2021 Day 12");

var input = (await File.ReadAllLinesAsync("input.txt"));

Console.WriteLine($"One: {PuzzleOne(input)}");
Console.WriteLine($"Two: {PuzzleTwo(input)}");

int PuzzleOne(string[] input)
{
    var (edges, leaving) = ParseInput(input);

    List<List<string>> paths = new();
    List<string> currentPath = new();
    List<string> visited = new();

    FindPath(edges, leaving, "start", paths, visited, false, false, currentPath);

    return paths.Count;
}

int PuzzleTwo(string[] input)
{
    var (edges, leaving) = ParseInput(input);

    List<List<string>> paths = new();
    List<string> currentPath = new();
    List<string> visited = new();

    FindPath(edges, leaving, "start", paths, visited, false, true, currentPath);

    return paths.Count;
}

(List<Edge> edges, Dictionary<string, List<Edge>> leaving) ParseInput(string[] input)
{
    Dictionary<string, List<string>> adjecents = new();

    var lines = input
        .Select(line => line.Split("-"));

    List<Edge> edges = new();

    foreach (var line in lines)
    {
        edges.Add(new Edge(line[0], line[1]));
        edges.Add(new Edge(line[1], line[0]));
    }

    return (edges, edges.GroupBy(e => e.U).ToDictionary(v => v.Key, e => e.ToList()));
}

bool IsLower(string str)
{
    return str.All(c => char.IsLower(c));
}

void FindPath(List<Edge> edges, Dictionary<string, List<Edge>> leaving, string v, List<List<string>> paths, List<string> visited, bool oneDouble, bool oneDoubleAllowed, List<string> currentPath)
{
    // completed a path
    if (v == "end")
    {
        currentPath.Add(v);
        paths.Add(currentPath);

        return;
    }

    // remember vertex of small caves
    if (IsLower(v))
    {
        visited.Add(v);
    }

    // add current vertex to curren path
    currentPath.Add(v);

    // traverse all leaving edges
    foreach (var edge in leaving[v])
    {
        bool contained = false;
        if (IsLower(edge.V))
        {
            contained = currentPath.Contains(edge.V);
            if (IsLower(edge.V) && ((contained && (oneDouble || !oneDoubleAllowed)) || edge.V == "start"))
            {
                continue;
            }
        }

        List<string> nextPath = new(currentPath);
        List<string> nextVisited = new(visited);

        FindPath(edges, leaving, edge.V, paths, nextVisited, contained | oneDouble, oneDoubleAllowed, nextPath);
    }
}

class Edge : IEquatable<Edge>
{
    public string U { get; set; }
    public string V { get; set; }

    public Edge(string x, string y)
    {
        U = x;
        V = y;
    }

    public override bool Equals(Object obj)
    {
        if (obj == null)
            return false;

        if (obj is not Edge edge)
            throw new ArgumentException("Object is not a Point.");

        return this.U.Equals(edge.U) && this.V.Equals(edge.V);
    }

    public override int GetHashCode()
    {
        int hashX = U.GetHashCode();

        int hashY = V.GetHashCode();

        return hashX ^ hashY;
    }

    public bool Equals(Edge obj)
    {
        if (obj == null)
            return false;

        return this.U.Equals(obj.U) && this.V.Equals(obj.V);
    }
}