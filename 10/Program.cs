using System;
using System.Collections.Generic;
using System.Linq;

namespace _10
{
    class Program
    {

        static Dictionary<string, string> ender = new Dictionary<string, string>
        {
            {"(",")"},
            {"[","]"},
            {"{","}"},
            {"<",">"}
        };

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
                .ToList();

            var r = new List<string>();


            var results = new List<string>();

            var points = new Dictionary<string, int>
            {
                { ")", 3},
                { "]", 57},
                { "}", 1197},
                { ">", 25137},
            };

            foreach(var i in input)
            {
                var chunks = new List<string>();
                chunks.Add(i);

                var chunk = chunks.First(); 

                do
                {
                    var p = chunk.Length;
                    
                    chunk = chunk.Replace("()","").Replace("[]","").Replace("{}","").Replace("<>","");

                    if(chunk.Length == p)
                    {

                        var l = chunk.IndexOfAny(new [] {')',']','}','>'});

                        // incomplete
                        if(l < 0)
                        {
                            break;
                        }

                        var lf = chunk.Substring(l).LastIndexOfAny(new [] {'(','[','{','<'});

                        Console.WriteLine($"Unexpected {chunk[l]}");

                        results.Add(chunk[l].ToString());

                        break;
                    }

                } while(true);
            }

            Console.WriteLine(results.Select(x => points[x]).Sum());
        }




        static void p2()
        {
            var input = System
                .IO
                .File
                .ReadAllLines("input.txt")
                .ToList();

            var r = new List<string>();


            var results = new List<long>();

            var points = new Dictionary<string, long>
            {
                { ")", 1},
                { "]", 2},
                { "}", 3},
                { ">", 4},
            };

            foreach(var i in input)
            {
                var chunks = new List<string>();
                chunks.Add(i);

                var chunk = chunks.First(); 

                do
                {
                    var p = chunk.Length;
                    
                    chunk = chunk.Replace("()","").Replace("[]","").Replace("{}","").Replace("<>","");

                    if(chunk.Length == p)
                    {

                        var l = chunk.IndexOfAny(new [] {')',']','}','>'});

                        if(l < 0)
                        {
                            var missing = chunk.Reverse().Select(x => ender[x.ToString()].ToString()).ToArray();

                            long total = 0;

                            foreach(var c in missing)
                            {
                                total = (total*5 + points[c]);
                            }

                            results.Add(total);

                            break;
                        }

                        break;
                    }

                } while(true);
            }

            results = results.OrderBy(x => x).ToList();

            Console.WriteLine(results[results.Count / 2]);
        }
    }
}
