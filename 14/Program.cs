using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var s = new System.Diagnostics.Stopwatch();
s.Start();

var input = File
    .ReadAllLines("input.txt")
    .ToList();

var template = input.First();

var rules = input.Where(x => x.Contains("->")).Select(x => x.Split("->")).ToLookup(x => x[0].Trim(), x => new[] { x[0].Trim()[0] + x[1].Trim(), x[1].Trim() + x[0].Trim()[1] });

var pairs = (Enumerable.Range(0, template.Length - 1).Select(i => template.Substring(i, 2)));

var currentStep = new Dictionary<string, long>();

foreach (var p in pairs.GroupBy(x => x))
{
    AddOrInc(currentStep, p.Key, p.Count());
}

// NN always becomes the pair NC and CN on the next step so use that to count the number of pairs for each step.
for (var step = 0; step < 40; step++)
{
    var nextStep = new Dictionary<string, long>();

    foreach(var p in currentStep)
    {
        var pairGives = rules[p.Key].SelectMany(x => x);

        foreach(var newPair in pairGives)
        {
            AddOrInc(nextStep, newPair, p.Value);
        }
    }

    currentStep = nextStep;
}

// Since we are counting pairs the expand of NN with C create two pairs NC and CN but it's actually only NCN so for counting
// only count the first letter in each pair and add the last char in the starting template manually
// NC CN -> N(c)+C(n)+N => 2N and 1C
var letterCount = new Dictionary<string, long>();
foreach(var k in currentStep.Keys)
{
    AddOrInc(letterCount, k[0].ToString(), currentStep[k]);
}
AddOrInc(letterCount, template.Last().ToString(), 1);

var max = letterCount.Select(l => l.Value).Max();
var min = letterCount.Select(l => l.Value).Min();

Console.WriteLine("Diff: " + (max-min));

Console.WriteLine("Total: " + s.Elapsed.TotalSeconds);

void AddOrInc(Dictionary<string, long> d, string key, long count)
{
    if(d.ContainsKey(key))
    {
        d[key]+= count;
    }
    else
    {
        d[key]= count;
    }
}