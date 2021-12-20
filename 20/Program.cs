using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace AoCTwentyOne
{

    public class Tests
    {

        private readonly ITestOutputHelper output;

        public Tests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Parse()
        {
            var input = File
                .ReadAllText("input.txt")
                .Split($"{Environment.NewLine}")
                .ToArray();

            var algo = input.First();

            var points = new Dictionary<(int x, int y), string>();

            for (var y = 0; y < input.Skip(2).Count(); y++)
            {
                for (var x = 0; x < input[2].Length; x++)
                {
                    points.Add((x, y), input.ElementAt(y + 2).ElementAt(x).ToString());
                }
            }

            for (var steps = 0; steps < 50; steps++)
            {
                var enchanced = new Dictionary<(int x, int y), string>();

                var minX = points.Keys.Select(k => k.x).Min() - 2;
                var maxX = points.Keys.Select(k => k.x).Max() + 2;
                var minY = points.Keys.Select(k => k.y).Min() - 2;
                var maxY = points.Keys.Select(k => k.y).Max() + 2;

                for (var y = minY; y <= maxY; y++)
                {
                    for (var x = minX; x <= maxX; x++)
                    {
                        enchanced[(x, y)] = ForPoint(points, x, y, steps % 2 == 0 ? "." : "#");
                    }
                }

                points = enchanced;

            }

            output.WriteLine("Pixels lit: " + points.Keys.Count(k => points[k] == "#"));

            string ForPoint(Dictionary<(int x, int y), string> points, int x, int y, string defaultValue)
            {
                var p1 = ToBin(points, x - 1, y - 1, defaultValue);
                var p2 = ToBin(points, x, y - 1, defaultValue);
                var p3 = ToBin(points, x + 1, y - 1, defaultValue);
                var p4 = ToBin(points, x - 1, y, defaultValue);
                var p5 = ToBin(points, x, y, defaultValue);
                var p6 = ToBin(points, x + 1, y, defaultValue);
                var p7 = ToBin(points, x - 1, y + 1, defaultValue);
                var p8 = ToBin(points, x, y + 1, defaultValue);
                var p9 = ToBin(points, x + 1, y + 1, defaultValue);

                var bin = $"{p1}{p2}{p3}{p4}{p5}{p6}{p7}{p8}{p9}".Replace("#", "1").Replace(".", "0");

                var index = Convert.ToInt32(bin, 2);

                return algo[index].ToString();
            }

            string ToBin(Dictionary<(int x, int y), string> points, int x, int y, string defaultValue)
            {
                if (!points.ContainsKey((x, y)))
                {
                    return defaultValue;
                }

                return points[(x, y)];
            }

            void Print(Dictionary<(int x, int y), string> points)
            {
                var minX = points.Keys.Select(k => k.x).Min();
                var maxX = points.Keys.Select(k => k.x).Max();
                var minY = points.Keys.Select(k => k.y).Min();
                var maxY = points.Keys.Select(k => k.y).Max();

                string s = "---------" + Environment.NewLine;

                for (var y = minY; y < maxY + 1; y++)
                {
                    for (var x = minX; x < maxX + 1; x++)
                    {
                        if (!points.ContainsKey((x, y)))
                        {
                            s += ".";
                        }
                        else
                        {
                            s += points[(x, y)];
                        }


                    }

                    s += Environment.NewLine;
                }

                s += "---------" + Environment.NewLine;

                output.WriteLine(s);
            }

        }

        [Fact]
        public void Overlap()
        {
        }
    }
}