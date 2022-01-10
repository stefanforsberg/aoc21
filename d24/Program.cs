using System.Diagnostics;

var input = File
    .ReadAllLines("input.txt")
    .Select(x => x.Split(' '))
    .Select(x => new { instruction = x[0].Trim(), variable = x[1].Trim(), value = x.Length > 2 ? x[2].Trim() : string.Empty })
    .ToArray();

var watch = new Stopwatch();
watch.Start();

var instructions = new List<(int a, int b, int c)>();
instructions.Add((1, 12, 6));
instructions.Add((1, 11, 12));
instructions.Add((1, 10, 5));
instructions.Add((1, 10, 10));
instructions.Add((26, -16, 7));
instructions.Add((1, 14, 0));
instructions.Add((1, 12, 4));
instructions.Add((26, -4, 12));
instructions.Add((1, 15, 14));
instructions.Add((26, -7, 13));
instructions.Add((26, -8, 10));
instructions.Add((26, -4, 11));
instructions.Add((26, -15, 19));
instructions.Add((26, -8, 9));

instructions.Reverse();


Console.WriteLine("Time: " + watch.Elapsed.TotalSeconds);

void P1()
{

    var validSolutions = new HashSet<long>();
    validSolutions.Add(0);

    var candidates = new List<Candidate>();
    candidates.Add(new Candidate(0, string.Empty));

    foreach (var instruction in instructions.Take(13))
    {
        var nextValidSolutions = new HashSet<long>();

        var newCandidates = new List<Candidate>();

        for (var z = 1; z < 10000000; z++)
        {
            for (var w = 1; w < 10; w++)
            {
                var x = ((z % 26) + instruction.b) != w ? 1 : 0;
                var zResult = (long)Math.Floor(z / (decimal)instruction.a) * (25 * x + 1) + (w + instruction.c) * x;

                if (validSolutions.Contains(zResult))
                {
                    var highest = candidates.Where(c => c.z == zResult).OrderByDescending(c => c.AsNumber).First();

                    newCandidates.Add(new Candidate(z, $"{w}{highest.combined}"));

                    nextValidSolutions.Add(z);

                }
            }
        }

        candidates = newCandidates;

        Console.WriteLine("Found " + nextValidSolutions.Count + " possible solutions. Max z: " + (nextValidSolutions.Count > 0 ? nextValidSolutions.Max() : 0));
        Console.WriteLine("Total candidates " + candidates.Count + " after " + watch.Elapsed.TotalSeconds + " seconds");

        validSolutions = nextValidSolutions;
    }

    var l = new List<long>();

    var i1 = instructions.Last();
    for (var w = 1; w < 10; w++)
    {
        var x = ((0 % 26) + i1.b) != w ? 1 : 0;
        var zResult = (long)Math.Floor(0 / (decimal)i1.a) * (25 * x + 1) + (w + i1.c) * x;

        var m = candidates.Where(x => x.z == zResult);
        foreach (var mm in m)
        {
            l.Add(long.Parse(w + mm.combined));
        }
    }

    Console.WriteLine(l.Max());
}

void P2()
{

    var validSolutions = new HashSet<long>();
    validSolutions.Add(0);

    var candidates = new List<Candidate>();
    candidates.Add(new Candidate(0, string.Empty));

    foreach (var instruction in instructions.Take(13))
    {
        var nextValidSolutions = new HashSet<long>();

        var newCandidates = new List<Candidate>();

        for (var z = 1; z < 10000000; z++)
        {
            for (var w = 1; w < 10; w++)
            {
                var x = ((z % 26) + instruction.b) != w ? 1 : 0;
                var zResult = (long)Math.Floor(z / (decimal)instruction.a) * (25 * x + 1) + (w + instruction.c) * x;

                if (validSolutions.Contains(zResult))
                {
                    var highest = candidates.Where(c => c.z == zResult).OrderBy(c => c.AsNumber).First();

                    newCandidates.Add(new Candidate(z, $"{w}{highest.combined}"));

                    nextValidSolutions.Add(z);

                }
            }
        }

        candidates = newCandidates;

        Console.WriteLine("Found " + nextValidSolutions.Count + " possible solutions. Max z: " + (nextValidSolutions.Count > 0 ? nextValidSolutions.Max() : 0));
        Console.WriteLine("Total candidates " + candidates.Count + " after " + watch.Elapsed.TotalSeconds + " seconds");

        validSolutions = nextValidSolutions;
    }

    var l = new List<long>();

    var i1 = instructions.Last();
    for (var w = 1; w < 10; w++)
    {
        var x = ((0 % 26) + i1.b) != w ? 1 : 0;
        var zResult = (long)Math.Floor(0 / (decimal)i1.a) * (25 * x + 1) + (w + i1.c) * x;

        var m = candidates.Where(x => x.z == zResult);
        foreach (var mm in m)
        {
            l.Add(long.Parse(w + mm.combined));
        }
    }

    Console.WriteLine(l.Min());
}

record Candidate(long z, string combined)
{
    public long AsNumber => long.TryParse(combined, out var n) ? n : 0;
}


