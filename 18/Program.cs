using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace xxx
{
    public class Tests2
    {
        private readonly ITestOutputHelper output;

        public Tests2(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Part1()
        {
            var input = File
                .ReadAllLines("input.txt")
                .Select(x => System.Text.Json.JsonDocument.Parse(x))
                .Select(x => new SNumber(x.RootElement))
                .ToArray();

            var current = input[0];

            for (var i = 1; i <= input.Length - 1; i++)
            {
                current = AddReduce(current, input[i]);
            }

            this.output.WriteLine("Done: " + current.ToString());

            current.Magnitude().ShouldBe(4433);
        }

        [Fact]
        public void Part2()
        {
            var input = File
                .ReadAllLines("input.txt")
                .Select(x => System.Text.Json.JsonDocument.Parse(x))
                .Select(x => new SNumber(x.RootElement))
                .ToArray();

            var current = input[0];

            long currentMax = -1000;

            for (var i = 0; i < input.Length; i++)
            {

                for (var i2 = i + 1; i2 < input.Length; i2++)
                {
                    if (i2 == i) continue;

                    var a = AddReduce(new SNumber(input[i].ToString()), new SNumber(input[i2].ToString()));
                    var b = AddReduce(new SNumber(input[i2].ToString()), new SNumber(input[i].ToString()));

                    var aMag = a.Magnitude();
                    var bMag = b.Magnitude();

                    if (aMag > currentMax) currentMax = aMag;
                    if (bMag > currentMax) currentMax = bMag;
                }
            }

            this.output.WriteLine("Done: " + current.ToString());

            currentMax.ShouldBe(4559);
        }

        public SNumber AddReduce(SNumber a, SNumber b)
        {
            a = SNumber.Add(a, b);

            while (true)
            {
                if (a.Explode())
                {
                    a = new SNumber(a.ToString());
                    continue;
                }

                if (a.Split())
                {
                    a = new SNumber(a.ToString());
                    continue;
                }

                break;
            }

            return a;

        }

    }

    public class Tests
    {

        [Fact]
        public void Stringify()
        {
            var result = new SNumber("[[1,9],[8,5]]");
            result.ToString().ShouldBe("[[1,9],[8,5]]");
        }

        [Fact]
        public void Parse()
        {
            var result = new SNumber("[[1,9],[8,5]]");
            result.Children.Count().ShouldBe(2);

            result = new SNumber("[[3,4],5]");
            result.Children.Count().ShouldBe(2);
            result.Children[0].Children[0].Literal.ShouldBe(3);
            result.Children[0].Children[1].Literal.ShouldBe(4);
            result.Children[1].Literal.ShouldBe(5);

            result = new SNumber("[1,2]");
            result.Children[0].Literal.ShouldBe(1);
            result.Children[1].Literal.ShouldBe(2);
            result.Children.Count().ShouldBe(2);
        }

        [Fact]
        public void Add()
        {
            var a = new SNumber("[1,2]");
            var b = new SNumber("[[3,4],5]");

            var result = SNumber.Add(a, b);

            a = new SNumber("[[[[4,3],4],4],[7,[[8,4],9]]]");
            b = new SNumber("[1,1]");

            result = SNumber.Add(a, b);
            result.ToString().ShouldBe("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]");

        }

        [Fact]
        public void Explode()
        {
            var result = new SNumber("[[[[[9,8],1],2],3],4]");
            var didExplode = result.Explode();
            didExplode.ShouldBeTrue();
            result.ToString().ShouldBe("[[[[0,9],2],3],4]");
            didExplode = result.Explode();
            didExplode.ShouldBeFalse();

            result = new SNumber("[7,[6,[5,[4,[3,2]]]]]");
            result.Explode();
            result.ToString().ShouldBe("[7,[6,[5,[7,0]]]]");

            result = new SNumber("[[6,[5,[4,[3,2]]]],1]");
            result.Explode();
            result.ToString().ShouldBe("[[6,[5,[7,0]]],3]");

            result = new SNumber("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]");
            result.Explode();
            result.ToString().ShouldBe("[[3,[2,[8,0]]],[9,[5,[7,0]]]]");

            result = new SNumber("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]");
            result.Explode();
            result.ToString().ShouldBe("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]");
        }

        [Fact]
        public void Split()
        {
            var result = new SNumber("[[[[0,7],4],[15,[0,13]]],[1,1]]");
            var didSplit = result.Split();
            didSplit.ShouldBeTrue();
            result.ToString().ShouldBe("[[[[0,7],4],[[7,8],[0,13]]],[1,1]]");

            didSplit = result.Split();
            didSplit.ShouldBeTrue();
            result.ToString().ShouldBe("[[[[0,7],4],[[7,8],[0,[6,7]]]],[1,1]]");

            didSplit = result.Split();
            didSplit.ShouldBeFalse();

        }

        [Fact]
        public void Magnitude()
        {
            var result = new SNumber("[9,1]");
            result.Magnitude().ShouldBe(29);

            result = new SNumber("[[9,1],[1,9]]");
            result.Magnitude().ShouldBe(129);

            result = new SNumber("[[1,2],[[3,4],5]]");
            result.Magnitude().ShouldBe(143);

            result = new SNumber("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]");
            result.Magnitude().ShouldBe(3488);


        }
    }
}