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
        public void P2()
        {
            var input = File
                .ReadAllText("input.txt")
                .ToArray();

            long p1wins = 0;
            long p2wins = 0;

            var universes = new Stack<((int p, int score) p1, (int p, int score) p2)>();

            universes.Push((p1: (4, 0), p2: (8, 0)));

            var possibleRolls = new List<(int[] Rolls, int Sum)>();

            for (var x = 1; x < 4; x++)
            {
                for (var y = 1; y < 4; y++)
                {
                    for (var z = 1; z < 4; z++)
                    {
                        var rolls = new int[] { x, y, z };
                        possibleRolls.Add((rolls, rolls.Sum()));
                    }
                }
            }

            var turns = 0;

            var winnersP1 = new HashSet<string>();
            var winnersP2 = new HashSet<string>();

            var skipped = 0;
            var totalVisited = 0;

            do
            {
                // output.WriteLine("Universes: " + universes.Count);

                // if (turns > 1000000)
                // {
                //     break;
                // }

                var round = universes.Pop();

                totalVisited++;

                foreach (var c1 in possibleRolls)
                {
                    foreach (var c2 in possibleRolls.Take(2))
                    {
                        var key = $"{round.p1.score}{round.p1.p}{c1.Sum}{round.p2.score}{round.p2.p}{c2.Sum}";

                        if (winnersP1.Contains(key))
                        {
                            // output.WriteLine("p1 wins from Hash");
                            skipped++;
                            p1wins++;
                            continue;
                        }

                        if (winnersP2.Contains(key))
                        {
                            // output.WriteLine("p2 wins from Hash");
                            p2wins++;
                            skipped++;
                            continue;
                        }

                        var p1 = Inc(c1.Sum, round.p1);
                        var p2 = Inc(c2.Sum, round.p2);

                        if (p1.score >= 21)
                        {
                            // output.WriteLine("p1 wins: " + round.p1.score + ". " + string.Join(",", c1));

                            p1wins++;

                            winnersP1.Add(key);
                        }
                        else if (p2.score >= 21)
                        {
                            // output.WriteLine("p2 wins: " + round.p2.score + string.Join(",", c2));

                            p2wins++;

                            winnersP2.Add(key);
                        }
                        else
                        {
                            // output.WriteLine("No wins: " + round.p1.score + ", " + round.p2.score);

                            universes.Push((p1, p2));
                        }

                    }
                }

                turns++;



            } while (universes.Count > 0);

            // for (var i = 1; i < 4; i += 3)
            // {

            output.WriteLine("Done");

            output.WriteLine("P1: " + p1wins);
            output.WriteLine("P2: " + p2wins);
            output.WriteLine("Skipped: " + skipped);
            output.WriteLine("TotalVisited: " + totalVisited);


            (int p, int score) Inc(int diceSum, (int p, int score) p)
            {
                var newPos = p.p += diceSum;

                while (newPos > 10)
                {
                    newPos = newPos - 10;
                }

                p.p = newPos;
                p.score += newPos;

                return p;
            }


            // public (int turn, int score) Until(int score, int turn)
            // {
            //     var result = (turn: 1, score: 0):
            //     do
            //     {

            //     } while true;
            // }

        }

        // [Fact]
        // public void P1()
        // {
        //     var input = File
        //         .ReadAllText("input.txt")
        //         .ToArray();


        //     var p1 = (p: 4, t: 0, score: 0);
        //     var p2 = (p: 8, t: 0, score: 0);

        //     // for (var i = 1; i < 4; i += 3)
        //     // {

        //     var dice = 0;

        //     var turn = 1;
        //     do
        //     {
        //         var d1 = RollDice();
        //         var d2 = RollDice();
        //         var d3 = RollDice();

        //         p1 = Inc(new[] { d1, d2, d3 }, p1);

        //         if (p1.score >= 1000) break;

        //         d1 = RollDice();
        //         d2 = RollDice();
        //         d3 = RollDice();

        //         p2 = Inc(new[] { d1, d2, d3 }, p2);

        //         if (p2.score >= 1000) break;

        //         turn++;

        //     } while (true);
        //     // }

        //     output.WriteLine("Done");

        //     output.WriteLine("P1: " + p1);
        //     output.WriteLine("P1: " + p2);
        //     output.WriteLine("Dice roll: " + (p1.t * 3 + p2.t * 3));

        //     var dicedRolled = (p1.t * 3 + p2.t * 3);

        //     if (p1.score > p2.score) output.WriteLine("Result: " + (p2.score * dicedRolled));
        //     if (p2.score > p1.score) output.WriteLine("Result: " + (p1.score * dicedRolled));

        //     int RollDice()
        //     {
        //         dice++;
        //         if (dice > 100) dice = 1;
        //         return dice;
        //     }

        //     (int p, int t, int score) Inc(int[] dice, (int p, int t, int score) p)
        //     {
        //         var newPos = p.p += dice.Sum();

        //         while (newPos > 10)
        //         {
        //             newPos = newPos - 10;
        //         }

        //         p.p = newPos;
        //         p.t++;
        //         p.score += newPos;

        //         return p;
        //     }


        //     // public (int turn, int score) Until(int score, int turn)
        //     // {
        //     //     var result = (turn: 1, score: 0):
        //     //     do
        //     //     {

        //     //     } while true;
        //     // }

        // }
    }
}