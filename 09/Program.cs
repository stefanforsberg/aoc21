using System;
using System.Collections.Generic;
using System.Linq;

namespace _09
{
    class Program
    {
        static Dictionary<string, int> cross = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            // p1();
            p2();
        }

        static void p2()
        {
            var input = System
                .IO
                .File
                .ReadAllLines("input.txt")
                .Select(x =>
                {
                    var i = new List<int>();

                    for (var z = 0; z < x.Length; z++)
                    {
                        i.Add(int.Parse(x[z].ToString()));
                    }

                    return i;
                })
                .ToList();


            var riskLevel = 0;

            var basin = new List<List<int>>();

            var basinLoc = new List<(int x, int y)>();

            for (var y = 0; y < input.Count; y++)
            {
                var basinX = new List<int>();


                for (var x = 0; x < input[y].Count; x++)
                {
                    var c = new List<int>();
                    if (y > 0)
                    {
                        c.Add(input[y - 1][x]);
                    }
                    else
                    {
                        c.Add(100);
                    }

                    if (y < input.Count - 1)
                    {
                        c.Add(input[y + 1][x]);
                    }
                    else
                    {
                        c.Add(100);
                    }

                    if (x > 0)
                    {
                        c.Add(input[y][x - 1]);
                    }
                    else
                    {
                        c.Add(100);
                    }

                    if (x < input[0].Count - 1)
                    {
                        c.Add(input[y][x + 1]);
                    }
                    else
                    {
                        c.Add(100);
                    }

                    if (c.All(cn => cn > input[y][x]))
                    {
                        basinLoc.Add((x, y));

                        basinX.Add(1);

                        riskLevel += input[y][x] + 1;
                    }
                    else
                    {
                        basinX.Add(input[y][x] == 9 ? 0 : 1);
                    }

                }

                basin.Add(basinX);


            }

            var allBasins = new List<int>();

            var maxX = basin[0].Count;
            var maxY = basin.Count;

            foreach (var bl in basinLoc)
            {
                var candidates = new List<(int x, int y)>();
                candidates.Add(bl);

                var currentCount = 0;

                void Check(int x, int y) 
                {
                    if(x < 0 || x > maxX-1 || y < 0 || y > maxY-1)
                    {
                        return;
                    }

                    if(basin[y][x] == 1)
                    {
                        candidates.Add((x, y));
                        basin[y][x] = 2;
                    }
                }

                do 
                {
                    var current = candidates.First();
                    candidates.RemoveAt(0);

                    currentCount++;
                    basin[current.y][current.x] = 2;

                    Check(current.x-1, current.y);
                    Check(current.x+1, current.y);
                    Check(current.x, current.y-1);
                    Check(current.x, current.y+1);

                } while(candidates.Count > 0);

                allBasins.Add(currentCount);


                
            }

            var a = allBasins.OrderByDescending(x => x).Take(3).ToList();
            Console.WriteLine("Answer: " + a[0]*a[1]*a[2]);
        }






        static void p1()
        {
            var input = System
                .IO
                .File
                .ReadAllLines("input.txt")
                .Select(x =>
                {
                    var i = new List<int>();

                    for (var z = 0; z < x.Length; z++)
                    {
                        i.Add(int.Parse(x[z].ToString()));
                    }

                    return i;
                })
                .ToList();


            var riskLevel = 0;

            for (var y = 0; y < input.Count; y++)
            {
                for (var x = 0; x < input[y].Count; x++)
                {
                    var c = new List<int>();
                    if (y > 0)
                    {
                        c.Add(input[y - 1][x]);
                    }
                    else
                    {
                        c.Add(100);
                    }

                    if (y < input.Count - 1)
                    {
                        c.Add(input[y + 1][x]);
                    }
                    else
                    {
                        c.Add(100);
                    }

                    if (x > 0)
                    {
                        c.Add(input[y][x - 1]);
                    }
                    else
                    {
                        c.Add(100);
                    }

                    if (x < input[0].Count - 1)
                    {
                        c.Add(input[y][x + 1]);
                    }
                    else
                    {
                        c.Add(100);
                    }

                    // Console.WriteLine("Low: " + input[y][x] + " - " + string.Join(",", c));

                    if (c.All(cn => cn > input[y][x]))
                    {
                        Console.WriteLine("Low: " + x + "," + y);

                        riskLevel += input[y][x] + 1;
                    }

                }


            }

            Console.WriteLine("riskLevel: " + riskLevel);

        }
    }
}
