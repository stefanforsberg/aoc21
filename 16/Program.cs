using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var s = new System.Diagnostics.Stopwatch();
s.Start();

var input = File
        .ReadAllLines("input.txt")
        .Select(x => Enumerable.Range(0, x.Length).Select(y => int.Parse(x[y].ToString())).ToList())
        .ToList();

Console.WriteLine("Total: " + s.Elapsed.TotalSeconds);