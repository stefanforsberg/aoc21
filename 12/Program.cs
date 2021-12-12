using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = new List<(string, string)>();

var s = new System.Diagnostics.Stopwatch();
s.Start();

P1();
P2();

Console.WriteLine(s.Elapsed.TotalSeconds);

void P1()
{
    Parse();

    var start = input.Where(x => x.Item1 == "start");

    var paths = new List<Path>();
    var endedPaths = new List<Path>();

    paths.Add(new Path(new[] { "start" }.ToList()));

    do
    {
        var path = paths.First();

        var possible = PossibleVisits(path.LatestPoint());

        foreach (var otherpossible in possible.Skip(1))
        {
            paths.Add(Path.NewPath(path, otherpossible));
        }

        path.Add(possible.First());

        endedPaths.AddRange(paths.Where(p => p.IsEnded()));

        paths = paths.Where(p => !p.IsEnded()).ToList();

    } while (paths.Count() > 0);

    var ending = endedPaths.Where(p => p.Visited.Contains("end"));

    Console.WriteLine("Ending cave: " + ending.Count());
}

List<string> AddToPath(string path, string pos)
{
    var newPaths = new List<string>();
    foreach (var pv in PossibleVisits(pos))
    {
        newPaths.Add(path + "->" + pv);
    }

    return newPaths;
}

List<string> PossibleVisits(string pos)
{
    var f = input
        .Where(i => i.Item1 == pos)
        .Select(x => x.Item2);

    var to = input
        .Where(i => i.Item2 == pos)
        .Select(x => x.Item1);

    return f.Union(to).Where(x => x != "start").ToList();
}



void P2()
{
}

void Parse()
{
    input = File
        .ReadAllLines("input.txt")
        .Select(x => x.Split("-"))
        .Select(x => (from: x[0], to: x[1]))
        .ToList();
}

public class Path
{
    private bool ended;
    public List<string> Visited;

    public bool HasBeenVisistedTwice = false;

    public static Path NewPath(Path path, string to)
    {
        var p = new Path(new List<string>(path.Visited));
        p.HasBeenVisistedTwice = path.HasBeenVisistedTwice;
        p.Add(to);
        return p;
    }

    public Path(List<string> path)
    {
        Visited = path;
    }

    public void Add(string cave)
    {
        Visited.Add(cave);


        SetIsEnded(cave);
    }

    public override string ToString()
    {
        return string.Join("->", Visited);
    }

    public string LatestPoint()
    {
        return Visited.Last();
    }

    public bool IsEnded()
    {
        return ended;
    }

    private void SetIsEnded(string cave)
    {
        if (cave == "end")
        {
            ended = true;
            return;
        }

        if (cave.All(char.IsLower))
        {
            var visitsForCave = Visited.Count(x => x == cave);
            if (visitsForCave > 2)
            {
                ended = true;
                return;
            }

            if (visitsForCave == 2)
            {
                if (HasBeenVisistedTwice)
                {
                    ended = true;
                    return;
                }

                HasBeenVisistedTwice = true;

                // ended = Visited.Where(x => x.All(char.IsLower)).GroupBy(x => x).Select(x => x.Count()).OrderByDescending(x => x).Take(2).Sum() > 3;

                // ended = Visited.Where(x => x.All(char.IsLower)).Where(x => x!= cave).GroupBy(x => x).Any(x => x.Count() > 1);
            }
        }

        // ended = Visited.Where(x => x.All(char.IsLower)).GroupBy(x => x).Any(x => x.Count() > 1);


        // var smallCaves = Visited.Where(x => x.All(char.IsLower)).GroupBy(x => x).Select(x => x.Count()).OrderByDescending(x => x).Take(2).Sum() > 3;
        // ended = Visited.Any(x => x == "end") || smallCaves;

        // // p1
        // ended = Visited.Any(x => x == "end") || Visited.Where(x => x.All(char.IsLower)).GroupBy(x => x).Any(x => x.Count() > 1); 
    }
}