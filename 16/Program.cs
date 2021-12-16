using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var s = new System.Diagnostics.Stopwatch();
s.Start();

var input = File
        .ReadAllLines("input.txt")
        .First();

var d = new Dictionary<string, string>
{
{"0","0000"},
{"1","0001"},
{"2","0010"},
{"3","0011"},
{"4","0100"},
{"5","0101"},
{"6","0110"},
{"7","0111"},
{"8","1000"},
{"9","1001"},
{"A","1010"},
{"B","1011"},
{"C","1100"},
{"D","1101"},
{"E","1110"},
{"F","1111"}
};

var k = string.Join("", input.Select(x => d[x.ToString()]));

var p = new Package(k, 1);

Console.WriteLine("Versions: " + p.TotalVersion);
Console.WriteLine("Value: " + p.Value);
Console.WriteLine("Total: " + s.Elapsed.TotalSeconds);