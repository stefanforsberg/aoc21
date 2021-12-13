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

P1();

Console.WriteLine(s.Elapsed.TotalSeconds);

void P1()
{
}