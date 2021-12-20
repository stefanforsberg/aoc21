using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace AoCTwentyOne
{

    public class Parts
    {
        private readonly ITestOutputHelper output;

        public Parts(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Part1()
        {
            var input = File
                .ReadAllLines("input.txt")
                .ToArray();

            this.output.WriteLine("Done");
        }

        [Fact]
        public void Part2()
        {
            var input = File
                .ReadAllLines("input.txt")
                .ToArray();
        }
    }

    public class Tests
    {

        [Fact]
        public void Parse()
        {
            var scanners = File
                .ReadAllText("input.txt")
                .Split($"{Environment.NewLine}{Environment.NewLine}")
                .Select(x => new Scanner(x));

            scanners.Count().ShouldBe(2);

            scanners.First().Name.ShouldBe("--- scanner 0 ---");
            scanners.First().Points.First().ShouldBe((-817, -765, 856));
        }

        [Fact]
        public void Overlap()
        {
            var scanners = File
                .ReadAllText("input.txt")
                .Split($"{Environment.NewLine}{Environment.NewLine}")
                .Select(x => new Scanner(x));

            var o = scanners.First().Overlap(scanners.Last());
        }
    }
}