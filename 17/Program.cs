using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var s = new System.Diagnostics.Stopwatch();
s.Start();

var input = File
    .ReadAllLines("input.txt");

var targetX = Enumerable.Range(138, 184 - 138 + 1).ToArray();
var targetY = Enumerable.Range(-125, 125 - 71 + 1).ToArray();

var velx = 0;
var vely = 0;
var pos = (x: 0, y: 0);

var maxX = targetX.Max();
var minY = targetY.Min();

var init = new HashSet<((int, int), int)>();

for (var x = 0; x < maxX + 1; x++)
{
    for (var y = minY; y < -1 * minY; y++)
    {
        var maxy = int.MinValue;
        pos.x = 0;
        pos.y = 0;

        velx = x;
        vely = y;

        do
        {
            pos.x += velx;
            pos.y += vely;

            if (pos.y > maxy)
            {
                maxy = pos.y;
            }

            if (velx > 0) velx--;
            if (velx < 0) velx++;
            vely--;

            if (targetX.Contains(pos.x) && targetY.Contains(pos.y))
            {
                init.Add(((x, y), maxy));
                break;
            }

            if (pos.x > maxX && velx >= 0)
            {
                break;
            }

            if (pos.y < minY && vely < 0)
            {
                break;
            }

        } while (true);
    }
}

Console.WriteLine("Max Y: " + init.Select(x => x.Item2).Max());
Console.WriteLine("Distinct: " + init.Count());
Console.WriteLine("Total: " + s.Elapsed.TotalSeconds);