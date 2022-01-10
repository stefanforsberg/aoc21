using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

var watch = new Stopwatch();
watch.Start();

P2();

Console.WriteLine("Time: " + watch.Elapsed.TotalSeconds);

void P2()
{

    var rolls = Enumerable.Range(1, 3)
        .SelectMany(d1 => Enumerable.Range(1, 3).SelectMany(d2 => Enumerable.Range(1, 3).Select(d3 => d1 + d2 + d3)))
        .GroupBy(x => x)
        .Select(x =>
            new
            {
                Sum = x.Key,
                NumberOfRollsLeadingToSum = x.Count()
            }
        ).ToArray();

    var p1 = (8, 0);
    var p2 = (2, 0);

    var cacheLookup = 0;

    var Cache = new Dictionary<((int, int), (int, int)), (long, long)>();



    (long p1wins, long p2wins) Play((int pos, int score) p1, (int pos, int score) p2)
    {
        if (p1.score >= 21) return (1, 0);
        if (p2.score >= 21) return (0, 1);

        var key = (p1, p2);

        if (!Cache.ContainsKey(key))
        {
            long numberOfWinsP1 = 0;
            long numberOfWinsP2 = 0;
            foreach (var roll in rolls)
            {
                // p1 rolls and gets new pos/score
                var newP1Pos = (p1.pos + roll.Sum) % 10;
                if (newP1Pos == 0) newP1Pos = 10;
                var newP1 = (newP1Pos, p1.score + newP1Pos);

                // Let p2 play against the "new" p1, ie p2 is the first player in this scenario
                var wins = Play(p2, newP1);

                // SInce we let p2 play first the wins for p2 is captured as wins for p1 so
                // take that into account when adding the total number of wins
                numberOfWinsP1 += wins.p2wins * roll.NumberOfRollsLeadingToSum;
                numberOfWinsP2 += wins.p1wins * roll.NumberOfRollsLeadingToSum;
            }

            Cache[key] = (numberOfWinsP1, numberOfWinsP2);
        }
        else
        {
            cacheLookup++;
        }

        return Cache[key];

    }

    var result = Play(p1, p2);
    Console.WriteLine("Cache lookups: " + cacheLookup);
    Console.WriteLine("p1: " + result.p1wins);
    Console.WriteLine("p2: " + result.p2wins);
}

