using System;
using System.Collections.Generic;
using System.Linq;

namespace _07
{
    class Program
    {
        static Dictionary<string, int> cross = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch();
            w.Start();
            // p1();
            //p2();
            p2Clean();

            Console.WriteLine(w.Elapsed.TotalSeconds);
        }

        static void p1()
        {
            var input = System
                .IO
                .File
                .ReadAllLines("input.txt")
                .First()
                .Split(",")
                .Select(x => int.Parse(x))
                .ToList();

            var min = input.Min();
            var max = input.Max();

            var moves = new int[max];

            for(var i = 0; i < moves.Length; i++)
            {
                for(var c = 0; c < input.Count; c++)
                {
                    moves[i] += Math.Abs(input[c] - i);
                }
            }

            Console.WriteLine("Min: " + moves.Min());

        }

        static void p2()
        {
            var input = System
                .IO
                .File
                .ReadAllLines("input.txt")
                .First()
                .Split(",")
                .Select(x => int.Parse(x))
                .ToList();

            var min = input.Min();
            var max = input.Max();

            var moves = new int[max];

            for(var i = 0; i < moves.Length; i++)
            {
                for(var c = 0; c < input.Count; c++)
                {
                    var diff = Math.Abs(input[c] - i);

                    var cost = 0;
                    for(var d = 1; d <= diff; d++)
                    {
                        cost+= (d);
                    }

                    moves[i] += cost;
                }
            }

            Console.WriteLine("Min: " + moves.Min());

        }

        static void p2Clean()
        {
            // 85015836
            var input = System
                .IO
                .File
                .ReadAllLines("input.txt")
                .First()
                .Split(",")
                .Select(x => int.Parse(x))
                .ToList();

            var max = input.Max();

            var moves = new int[max];

            for(var i = 0; i < moves.Length; i++)
            {
                for(var c = 0; c < input.Count; c++)
                {
                    var diff = Math.Abs(input[c] - i);

                    // // 6.5sec
                    // moves[i] += Enumerable.Range(1,diff).Sum(x => x); 

                    // // 2.5 sec
                    // var cost = 0;
                    // for(var d = 1; d <= diff; d++)
                    // {
                    //     cost+= (d);
                    // } 
                    // moves[i] += cost; 

                    // 0,03702 sec
                    moves[i] += diff*(diff+1) / 2;
                }
            }

            Console.WriteLine("Min: " + moves.Min());

        }
    }
}
