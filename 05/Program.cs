using System;
using System.Collections.Generic;
using System.Linq;

namespace _05
{
    class Program
    {
        static Dictionary<string, int> cross = new Dictionary<string, int>();

        static void Main(string[] args)
        {

            // p1();
            Cleaned();
        }

        static void Cleaned()
        {
            var dotter = new List<(int, int)>();

            var input = System.IO.File.ReadAllLines("input.txt")
                .Select(x => x.Replace("->", ",").Split(","))
                .Select(x =>
                {
                    var l = (x: int.Parse(x[0]), y: int.Parse(x[1]));
                    var r = (x: int.Parse(x[2]), y: int.Parse(x[3]));

                    return (l: l, r: r);
                })
                .ToList();

            foreach (var d in input)
            {
                var xSig = d.l.x == d.r.x ? 0 : d.l.x < d.r.x ? 1 : -1;
                var ySig = d.l.y == d.r.y ? 0 : d.l.y < d.r.y ? 1 : -1;

                var diff = new[] { Math.Abs(d.l.x - d.r.x), Math.Abs(d.l.y - d.r.y) }.Max();

                for (var del = 0; del <= diff; del++)
                {
                    dotter.Add((d.l.x + del * xSig, d.l.y + del * ySig));
                }
            }

            Console.WriteLine("Crossing: " + dotter.GroupBy(x => $"{x.Item1},{x.Item2}").Count(x => x.Count() > 1));
        }

        static void p1()
        {
            var dotter = new List<(int, int)>();

            var input = System
                .IO
                .File
                .ReadAllLines("input.txt")
                .Select(x =>
                {
                    var v = x.Split("->");
                    var l = (x: int.Parse(v[0].Split(",")[0]), y: int.Parse(v[0].Split(",")[1]));
                    var r = (x: int.Parse(v[1].Split(",")[0]), y: int.Parse(v[1].Split(",")[1]));

                    return (l: l, r: r);
                })
                .ToList();

            foreach (var d in input)
            {
                var xSig = d.l.x == d.r.x ? 0 : d.l.x < d.r.x ? 1 : -1;
                var ySig = d.l.y == d.r.y ? 0 : d.l.y < d.r.y ? 1 : -1;

                var diff = new[] { Math.Abs(d.l.x - d.r.x), Math.Abs(d.l.y - d.r.y) }.Max();

                for (var del = 0; del <= diff; del++)
                {
                    dotter.Add((d.l.x + del * xSig, d.l.y + del * ySig));
                }

                // var xs = new[] { d.l.x, d.r.x}.OrderBy(x => x);
                // var ys = new[] { d.l.y, d.r.y}.OrderBy(y => y);

                // if(d.l.x == d.r.x || d.l.y == d.r.y)
                // {

                //     for(var y = ys.ElementAt(0); y<= ys.ElementAt(1); y++  )
                //     {
                //         for(var x = xs.ElementAt(0); x<= xs.ElementAt(1); x++  )
                //         {
                //             dotter.Add((x,y));
                //         }
                //     }
                // }
                // else
                // {
                //     var xSig = d.l.x < d.r.x ? 1 : -1;
                //     var ySig = d.l.y < d.r.y ? 1 : -1;

                //     var diff = Math.Abs(d.l.x - d.r.x);

                //     for(var del = 0; del <= diff; del++)
                //     {
                //         dotter.Add((d.l.x + del*xSig, d.l.y + del*ySig));
                //     }
                // }

            }

            // var result = cross.Count(c => c.Value >= 2);

            // Console.WriteLine("Crossing: " + result);
            Console.WriteLine("Crossing: " + dotter.GroupBy(x => $"{x.Item1},{x.Item2}").Count(x => x.Count() > 1));

            for (var y = 0; y < 10; y++)
            {
                for (var x = 0; x < 10; x++)
                {
                    if (cross.ContainsKey($"{x},{y}"))
                    {
                        Console.Write(cross[$"{x},{y}"]);
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }

                Console.WriteLine(" ");
            }

        }

        // static void AddOrUpdate(int x, int y)
        // {
        //     var key = $"{x},{y}";
        //     if (cross.ContainsKey(key))
        //     {
        //         cross[key] = cross[key] + 1;
        //     }
        //     else
        //     {
        //         cross[key] = 1;

        //     }
        // }


    }
}
