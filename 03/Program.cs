using System;
using System.Collections.Generic;
using System.Linq;

namespace _03
{
    class Program
    {
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

            var result = new int[input[0].Count()].ToList();

            Console.WriteLine(string.Join("", result) + ", " + input.Count() / 2);

            input
                .ForEach(r =>
                {

                    for (var i = 0; i < r.Length; i++)
                    {
                        result[i] += int.Parse(r.Substring(i, 1));

                        //Console.WriteLine(result[i]);
                    }
                });

            Console.WriteLine("Result: " + string.Join("", result));

            var mostCommon = result.Select(x => x > input.Count() / 2 ? 1 : 0);
            var mostCommonDecimal = Convert.ToInt32(string.Join("", mostCommon), 2);

            var leastCommon = mostCommon.Select(x => x == 0 ? 1 : 0);
            var leastCommonDecimal = Convert.ToInt32(string.Join("", leastCommon), 2);

            Console.WriteLine($"mostCommon: {string.Join("", mostCommon)}, mostCommonDecimal: {string.Join("", mostCommonDecimal)}");
            Console.WriteLine($"leastCommon: {string.Join("", leastCommon)}, leastCommonDecimal: {string.Join("", leastCommonDecimal)}");

            Console.WriteLine("Combined: " + mostCommonDecimal * leastCommonDecimal);

            // P1 answer: 3429254
        }

        static void p2()
        {
            var input = System
                .IO
                .File
                .ReadAllLines("input.txt")
                .ToList();

            var oxygen = Reduce(input, 1);
            var c02 = Reduce(input, 0);

            var oxygenDecimal = Convert.ToInt32(string.Join("", oxygen), 2);
            var c02Decimal = Convert.ToInt32(string.Join("", c02), 2);

            Console.WriteLine($"Oxygen: {string.Join("", oxygen)}, c02: {string.Join("", c02)}");
            Console.WriteLine($"Oxygen: {oxygenDecimal}, c02: {c02Decimal}");
            Console.WriteLine("Combined: " + oxygenDecimal * c02Decimal);

            string Reduce(List<string> input, int fallbackValue)
            {
                var numberOfDigits = input[0].Length;

                for (var n = 0; n < numberOfDigits; n++)
                {
                    var bitSum = 0;

                    for (var r = 0; r < input.Count; r++)
                    {
                        bitSum += int.Parse(input[r].Substring(n, 1));
                    }

                    int toLookFor;

                    var midpointValue = Math.Ceiling(((decimal)input.Count / 2));

                    if (bitSum == midpointValue)
                    {
                        // Same number of both bits, use the common value
                        toLookFor = fallbackValue;
                    }
                    else
                    {

                        toLookFor = bitSum > midpointValue ? 1 : 0;

                        // If we're looking for the least common we need to switch the resulting bit to look for.
                        if (fallbackValue == 0)
                        {
                            toLookFor = toLookFor == 1 ? 0 : 1;
                        }
                    }

                    // Select all rows which matches the bit for the current index and set the list to only contain the matches
                    input = input.Where(x => x.Substring(n, 1) == toLookFor.ToString()).ToList();

                    if (input.Count == 1)
                    {
                        break;
                    }
                }

                return input[0];

            }

        }
    }
}
