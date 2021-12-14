using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

var s = new System.Diagnostics.Stopwatch();
s.Start();

var input = File
    .ReadAllLines("input.txt")
    .ToList();

var template = input.First();

// var rules = input.Where(x => x.Contains("->")).Select(x => x.Split("->")).ToDictionary(x => x[0].Trim(), x => x[1].Trim());
var rules = input.Where(x => x.Contains("->")).Select(x => x.Split("->")).ToLookup(x => x[0].Trim(), x => x[1].Trim());

var rules2 = input.Where(x => x.Contains("->")).Select(x => x.Split("->")).ToLookup(x => x[0].Trim(), x => x[0].Trim()[0] + x[1].Trim() + x[0].Trim()[1]);

string newTemplate = template;

// for(var step = 0; step < 10; step++)
// {
//     Console.WriteLine("Step " + (step+1) + ". Length: " + template.Length);

//     // foreach(var rule in rules2)
//     // {
//     //     newTemplate = newTemplate.Replace(rule.Key, rule.First());
//     // }

//     for(var i = template.Length-2; i >= 0; i--)
//     {

//         var p = template.Substring(i,2);

//         var r = rules[p].FirstOrDefault();

//         template = template.Insert(i+1, r);
//     }
// }



string Expand(string toExpand)
{
    for (var step = 0; step < 10; step++)
    {
        
        for (var i = toExpand.Length - 2; i >= 0; i--)
        {
            var p = toExpand.Substring(i, 2);
            var r = rules[p].FirstOrDefault();
            toExpand = toExpand.Insert(i + 1, r);
        }

        Console.WriteLine("After Step " + (step + 1) + ": " + toExpand.Length);
    }

    return toExpand.Substring(0, toExpand.Length-1);
}

var total = string.Empty;
for (var i = 0; i <= template.Length - 2; i++)
{
    // var expandedPair = Expand(template.Substring(i, 2));
    
    total +=  Expand(template.Substring(i, 2));
}

total += template.Last();



var c = total.GroupBy(x => x).Select(x => x.Count());

Console.WriteLine("result: " + (c.Max() - c.Min()));

Console.WriteLine("Done: " + s.Elapsed.TotalSeconds);
