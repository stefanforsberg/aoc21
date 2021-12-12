using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = new List<List<int>>();

P1();
P2();

void P1()
{
    Parse();

    var totalFlashed = 0;

    for (var s = 0; s < 100; s++)
    {
        totalFlashed += Step(s);
    }

    Console.WriteLine("Total flashes: " + totalFlashed);
}

void P2()
{
    Parse();

    var flashedForStep = 0;

    var s = 0;

    do
    {
        flashedForStep = Step(s);
        s++;
    } while (flashedForStep != 100);

    Console.WriteLine("All flashed on: " + s);
}

void Parse()
{
    input = File
        .ReadAllLines("input.txt")
        .Select(x => Enumerable.Range(0, x.Length).Select(y => int.Parse(x[y].ToString())).ToList())
        .ToList();
}

int Step(int s)
{
    for (var y = 0; y < input.Count; y++)
    {
        for (var x = 0; x < input[y].Count; x++)
        {
            IncFlash(x, y);
        }
    }

    var numberOfFlashesForTurn = 0;

    for (var y = 0; y < input.Count; y++)
    {
        for (var x = 0; x < input[y].Count; x++)
        {
            if (input[y][x] == -1)
            {
                input[y][x] = 0;

                numberOfFlashesForTurn++;
            }
        }
    }

    return numberOfFlashesForTurn;
}

void IncFlash(int x, int y)
{

    if (x < 0 || x > input.Count - 1 || y < 0 || y > input[0].Count - 1)
    {
        return;
    }

    if (input[y][x] == -1)
    {
        return;
    }

    input[y][x] += 1;

    var flashed = input[y][x] > 9;

    if (flashed)
    {
        input[y][x] = -1;

        foreach (var cy in new[] { -1, 0, 1 })
        {
            foreach (var cx in new[] { -1, 0, 1 })
            {
                IncFlash(x + cx, y + cy);
            }
        }
    }
}