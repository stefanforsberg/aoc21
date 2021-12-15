using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var s = new System.Diagnostics.Stopwatch();
s.Start();

var input = File
    .ReadAllLines("input.txt")
    .ToList();

Console.WriteLine("Total: " + s.Elapsed.TotalSeconds);