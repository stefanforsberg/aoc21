using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var s = new System.Diagnostics.Stopwatch();
s.Start();

var input = File
    .ReadAllLines("input.txt")
    .ToList();

var coords = input.Where(x => x.Contains(",")).Select(x => x.Split(",")).Select(x => (x: int.Parse(x[0]), y: int.Parse(x[1]))).ToList();

var maxX = coords.Select(x => x.x).Max();
var maxY = coords.Select(x => x.y).Max();

var folds = input.Where(x => x.Contains("fold along ")).Select(x => x.Replace("fold along ", "")).Select(x => x.Split("=")).Select(x => (dir: x[0], where: int.Parse(x[1]))).ToList();

// PAlt1();
PAlt2();

Console.WriteLine(s.Elapsed.TotalSeconds);

void PAlt2()
{
    var start = coords.ToHashSet();

    foreach(var fold in folds)
    {
        int DeltaY(int y) => fold.dir == "x" ? y : fold.where - Math.Abs(fold.where-y);
        int DeltaX(int x) => fold.dir == "y" ? x : fold.where - Math.Abs(fold.where-x);

        start = start.Select(tf => (DeltaX(tf.x), DeltaY(tf.y))).ToHashSet();
        
        Console.WriteLine("Number of points: " + start.Count);
    }

    PrintGrid(start);
    
}

void PrintGrid(HashSet<(int x, int y)> grid)
{
    var maxX = grid.Select(x => x.x).Max() + 1;
    var maxY = grid.Select(x => x.y).Max() + 1;

    for (var y = 0; y < maxY; y++)
    {
        for (var x = 0; x < maxX; x++)
        {
            if (grid.Any(g => g.x == x && g.y == y))
            {
                Console.Write("#");
            }
            else
            {
                Console.Write(" ");
            }
        }

        Console.WriteLine("");
    }

}

void PAlt1()
{

    var grid = new int[maxY + 1][];
    for (var y = 0; y <= maxY; y++)
    {
        grid[y] = new int[maxX + 1];

        for (var x = 0; x <= maxX; x++)
        {
            var c = coords.Any(c => c.x == x && c.y == y);
            if (c)
            {
                grid[y][x] = 1;
            }
            else
            {
                grid[y][x] = 0;
            }
        }
    }

    var currentGrid = grid;

    foreach (var fold in folds)
    {

        var count = 0;

        if (fold.dir == "y")
        {
            var newgrid = new int[fold.where][];

            for (var i = 0; i < fold.where; i++)
            {
                newgrid[i] = new int[currentGrid[0].Length];

                for (var x = 0; x < currentGrid[0].Length; x++)
                {
                    if (currentGrid[i][x] == 1 || currentGrid[currentGrid.Length - 1 - i][x] == 1)
                    {
                        newgrid[i][x] = 1;

                        count++;

                    }
                    else
                    {
                        newgrid[i][x] = 0;
                    }
                }
            }
            currentGrid = newgrid;

        }

        if (fold.dir == "x")
        {
            var newgrid = new int[currentGrid.Length][];

            for (var i = 0; i < newgrid.Length; i++)
            {
                newgrid[i] = new int[fold.where];

                for (var x = 0; x < fold.where; x++)
                {
                    if (currentGrid[i][x] == 1 || currentGrid[i][currentGrid[0].Length - 1 - x] == 1)
                    {
                        newgrid[i][x] = 1;

                        count++;

                    }
                    else
                    {
                        newgrid[i][x] = 0;
                    }
                }
            }
            currentGrid = newgrid;




        }

        Count(currentGrid);



    }

    Print(currentGrid);





}

void Count(int[][] grid)
{
    var total = 0;
    for (var y = 0; y < grid.Length; y++)
    {
        for (var x = 0; x < grid[0].Length; x++)
        {
            if (grid[y][x] == 1)
            {
                total++;
            }
        }

    }

    Console.WriteLine("Total dots: " + total);

}

void Print(int[][] grid)
{
    var total = 0;

    for (var y = 0; y < grid.Length; y++)
    {
        for (var x = 0; x < grid[0].Length; x++)
        {
            Console.Write(grid[y][x] == 1 ? "#" : " ");

            if (grid[y][x] == 1)
            {
                total++;
            }
        }

        Console.WriteLine("");
    }

    Console.WriteLine("Total points: " + total);
}