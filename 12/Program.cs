using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File
    .ReadAllLines("input.txt")
    .Select(x => x.Split("-"))
    .Select(x => (from: x[0], to: x[1]))
    .ToList();

var possibleMoves = input.Select(x => x.from).Concat(input.Select(x => x.to))
    .Distinct()
    .ToDictionary(x => x, x => PossibleVisits(x));

var s = new System.Diagnostics.Stopwatch();
s.Start();

P1();

Console.WriteLine(s.Elapsed.TotalSeconds);

void P1()
{
    var paths = new List<List<string>>();
    paths.Add(new List<string> {"start"});

    var maxPossiblePaths = 0;
    var totalPaths = 0;

    do
    {
        if(paths.Count > maxPossiblePaths)
        {
            maxPossiblePaths = paths.Count;
        }

        var path = paths.First();
        paths.RemoveAt(0);

        var possible = possibleMoves[path.Last()];

        foreach (var moveTo in possible)
        {
            if(moveTo == "end")
            {
                totalPaths++;
                continue;
            }

            var cp = new List<string>(path);
            cp.Add(moveTo);

            if(!EndedP2(cp, moveTo))
            {
                paths.Add(cp);
            }
            // if(EndedP2(cp, otherpossible))
            // {
            //     endedPaths.Add(cp);
            // } 
            // else
            // {
            //     paths.Add(cp);
            // }
        }
    } while (paths.Count() > 0);

    Console.WriteLine("Total number of paths: " + totalPaths);
    Console.WriteLine("Max paths considered: " + maxPossiblePaths);
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

bool EndedP1(List<string> path, string newCave)
{
    return path.Where(x => x.All(char.IsLower)).GroupBy(x => x).Any(x => x.Count() > 1);
}

bool EndedP2(List<string> path, string newCave)
{
    if (!newCave.All(char.IsLower))
    {
        return false;
    }

    return path.Where(x => x.All(char.IsLower)).GroupBy(x => x).Select(x => x.Count()).OrderByDescending(x => x).Take(2).Sum() > 3;
}