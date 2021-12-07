using System;
using System.Collections.Generic;
using System.Linq;

namespace _06
{
    class Program
    {
        static Dictionary<string, int> cross = new Dictionary<string, int>();

        static void Main(string[] args)
        {

            // p1();
            p2();

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

            var fishes = input.Select(i => new Fish(i)).ToList();

            var d = new List<int>();

            for (var day = 0; day < 80; day++)
            {
                var newFishes = new List<Fish>();
                foreach (var fish in fishes)
                {
                    fish.Pass();

                    var f = fish.NewFrom();

                    if (f != null)
                    {
                        newFishes.Add(f);
                    }
                }

                fishes.AddRange(newFishes);

                // Console.WriteLine("Day " + (day + 1) + ": " + string.Join(",", fishes.Select(f => f.Timer)));

                d.Add(fishes.Count());
            }

            Console.WriteLine("fishes: " + string.Join(",", d));

            Console.WriteLine("Fish count: " + fishes.Count());



        }

        public class Fish
        {
            int InternalTimer { get; set; }
            public int Timer { get; set; }

            public Fish(int timer)
            {
                InternalTimer = timer;
                Timer = timer;
            }


            public void Pass()
            {
                Timer--;
            }

            public Fish NewFrom()
            {
                if (Timer >= 0)
                {
                    return null;
                }

                Timer = 6;
                return new Fish(8);
            }
        }






























        static void p2()
        {
            var input = System.IO.File.ReadAllLines("input.txt")
                .First()
                .Split(",")
                .Select(x => long.Parse(x))
                .ToList();

            var lu = Enumerable.Range(0,9).Select(i => (long)input.Where(x => x == (long)i).Count()).ToArray();

            for (var day = 0; day < 256; day++)
            {

                var newFishes = lu[0];

                for(var m = 0; m < 8; m++)
                {
                    lu[m] = lu[m+1];
                }

                lu[6] += newFishes;
                lu[8] = newFishes;
            }

            Console.WriteLine("Fish count: " + lu.Sum(x => x));
        }
    }
}
