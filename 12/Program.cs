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
    .ToDictionary(x => x, x => {
         var from = input
            .Where(i => i.Item1 == x)
            .Select(x => x.Item2);

        var to = input
            .Where(i => i.Item2 == x)
            .Select(x => x.Item1);

        return from.Union(to).Where(x => x != "start").ToList();
    });

var s = new System.Diagnostics.Stopwatch();
s.Start();

P2();

Console.WriteLine(s.Elapsed.TotalSeconds);

void P2()
{
    var paths = new List<List<string>>();
    paths.Add(new List<string> {"start"});

    var totalPaths = 0;

    do
    {
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

            if(!IllegalMoveP2(cp, moveTo))
            {
                paths.Add(cp);
            }
        }
    } while (paths.Count() > 0);

    Console.WriteLine("Total number of paths: " + totalPaths);
}

bool IllegalMoveP1(List<string> path, string newCave)
{
    return path.Where(x => x.All(char.IsLower)).GroupBy(x => x).Any(x => x.Count() > 1);
}

bool IllegalMoveP2(List<string> path, string newCave)
{
    if (!newCave.All(char.IsLower))
    {
        return false;
    }

    var c = path.Count(x => x == newCave);
    
    if(c <= 1) return false;

    if(c > 2) return true;

    return path.Where(x => x.All(char.IsLower)).GroupBy(x => x).Count(x => x.Count() > 1) > 1;

    // return path.Where(x => x.All(char.IsLower)).GroupBy(x => x).Select(x => x.Count()).OrderByDescending(x => x).Take(2).Sum() > 3;
}