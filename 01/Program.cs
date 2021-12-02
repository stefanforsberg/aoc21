using System;
using System.Linq;

namespace _01
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System
                .IO
                .File
                .ReadAllLines("input.txt")
                .Select(x => int.Parse(x));


            var increase = input
                .Where( (d, i) => i == 0 ? false : d > input.ElementAt(i-1))
                .Count();

            // var increase = 0;

            // for(var i = 1; i < input.Count(); i++) {
            //     if(input.ElementAt(i) > input.ElementAt(i-1))
            //     {
            //         increase++;
            //     }
            // }

            Console.WriteLine("Increased: " + increase);

            var increase3Measurement = 0;

            for(var i = 1; i < input.Count()-2; i++) {
                // var g1 = input.ElementAt(i-1) + input.ElementAt(i) + input.ElementAt(i+1);
                // var g2 = input.ElementAt(i) + input.ElementAt(i+1) + input.ElementAt(i+2);

                // i and i+1 cancel each other
                var g1 = input.ElementAt(i-1);
                var g2 = input.ElementAt(i+2);
                
                if(g2> g1)
                {
                    increase3Measurement++;
                }
            }

            Console.WriteLine("Increased with three-measurement sliding window: " + increase3Measurement);
        }
    }
}
