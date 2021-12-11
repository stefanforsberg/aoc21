using System;
using System.Collections.Generic;
using System.Linq;

namespace _08
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
                .Select(r => r.Split("|")[1].Split(" ").Select(x => x.Trim()).Where(x => x.Length > 0).ToArray())
                .ToList();

            var numbers = input.SelectMany(x => x.Where(y => y.Length == 4 || y.Length == 2 || y.Length == 3 || y.Length == 7)).Count();

            Console.WriteLine("Count: " + numbers);

        }

        static void p2()
        {
            var input = System
                .IO
                .File
                .ReadAllLines("input.txt")
                .Select(r => 
                    (
                        input: r.Split("|")[0].Split(" ").Select(x => string.Join("", x.Trim().OrderBy(x => x)) ).Where(x => x.Length > 0).ToArray(), 
                        output: r.Split("|")[1].Split(" ").Select(x => string.Join("", x.Trim().OrderBy(x => x))  ).Where(x => x.Length > 0).ToArray()
                    )
                )
                .ToList();


            long total = 0;

            foreach(var i in input)
            {
                var cu = i.input;

                var n = new Dictionary<string, int>();
                var four = cu.First(x => x.Length == 4);
                var one = cu.First(x => x.Length == 2);
                var eight = cu.First(x => x.Length == 7);
                var seven = cu.First(x => x.Length == 3);

                n[four] = 4;
                n[one] = 1;
                n[eight] = 8;
                n[seven] = 7;

                var c690 = cu.Where(x => x.Length == 6);
                var six = cu.First(x => x.Length == 6 && x.Intersect(one).Count() == 1);
                var nine = cu.First(x => x.Length == 6 && x.Intersect(four).Count() == 4);
                var zero = c690.Except(new[] { six, nine}).First();

                n[six] = 6;
                n[nine] = 9;
                n[zero] = 0;

                var c235 = cu.Where(x => x.Length == 5);
                var three = c235.First(x => x.Intersect(one).Count() == 2);
                var five = c235.First(x => x.Intersect(six).Count() == 5);
                var two = c235.Except(new[] { three, five}).First();

                n[three] = 3;
                n[five] = 5;
                n[two] = 2;

                total += int.Parse(string.Join("", i.output.Select(x => n[x])));
            }

            Console.WriteLine("Total: " + total);


        }
    }
}
