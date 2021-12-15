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

var combined = new List<List<int>>();

for (var yloop = 0; yloop < 5; yloop++)
{
    for (var y = 0; y < input.Count; y++)
    {
        var ylist = new List<int>();

        for (var xloop = 0; xloop < 5; xloop++)
        {
            for (var x = 0; x < input[0].Count; x++)
            {
                var c = input[y][x] + yloop + xloop;
                if (c > 9) c = c - 9;
                ylist.Add(c);
            }
        }
        combined.Add(ylist);
    }
}

Console.WriteLine("P1: " + LowestForGrid2(input));
Console.WriteLine("P2: " + LowestForGrid2(combined));

Console.WriteLine("Total: " + s.Elapsed.TotalSeconds);

int LowestForGrid2(List<List<int>> grid)
{
    var visited = new Dictionary<(int x, int y), int>();

    var paths = new PriorityQueue<((int x, int y) pos, int score), int>();

    var exit = (x: grid.Count - 1, y: grid[0].Count - 1);

    paths.Enqueue(((0, 0), 0), 0);

    var totalScore = new List<int>() { int.MaxValue };

    do
    {
        var path = paths.Dequeue();

        MoveTo((path.pos.x + 1, path.pos.y), path.score);
        MoveTo((path.pos.x - 1, path.pos.y), path.score);
        MoveTo((path.pos.x, path.pos.y + 1), path.score);
        MoveTo((path.pos.x, path.pos.y - 1), path.score);

    } while (paths.Count > 0);

    return visited[exit];

    int RiskAt(int x, int y)
    {

        if (x < 0 || x > grid.Count - 1 || y < 0 || y > grid[0].Count - 1)
        {
            return -1000;
        }

        return grid[y][x];
    }

    void MoveTo((int x, int y) pos, int currentScore)
    {
        var riskForPoint = RiskAt(pos.x, pos.y);

        if (riskForPoint > 0)
        {
            var score = currentScore + riskForPoint;
            if (!visited.ContainsKey(pos) || visited[pos] > (score))
            {
                visited[pos] = score;
                paths.Enqueue((pos, score), score);
            }
        }
    }
}