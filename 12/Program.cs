using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File
    .ReadAllLines("input.txt")
    .Select(x => x.Split("-"))
    .Select(x => (from: x[0], to: x[1]))
    .ToList();

var possibleMoves = input
    .Select(x => x.from)
    .Concat(input.Select(x => x.to))
    .Distinct()
    .ToDictionary(x => x, x =>
    {
        var from = input
           .Where(i => i.from == x)
           .Select(x => x.to);

        var to = input
            .Where(i => i.to == x)
            .Select(x => x.from);

        return from.Union(to).Where(x => x != "start").ToList();
    });

var s = new System.Diagnostics.Stopwatch();
s.Start();

P2();

Console.WriteLine(s.Elapsed.TotalSeconds);

void P2()
{
    var paths = new Queue<(string current, Dictionary<string, int> counter)>();
    paths.Enqueue(("start", new Dictionary<string, int>()));

    var totalPaths = 0;

    do
    {
        var path = paths.Dequeue();

        var possible = possibleMoves[path.current];

        foreach (var moveTo in possible)
        {
            if (moveTo == "end")
            {
                totalPaths++;
                continue;
            }

            var newPath = new Dictionary<string, int>(path.counter);

            if (Char.IsLower(moveTo[0]))
            {
                if(!newPath.TryAdd(moveTo, 1))
                {
                    newPath[moveTo]++;
                }
                
                // // P1
                // if(newPath.Any(n => n.Value > 1))
                // {
                //     continue;
                // }

                if (newPath[moveTo] > 2 || newPath.Count(n => n.Value > 1) > 1)
                {
                    continue;
                }
            }

            paths.Enqueue((moveTo, newPath));
        }
    } while (paths.Count() > 0);

    Console.WriteLine("Total number of paths: " + totalPaths);
}