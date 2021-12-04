using System;
using System.Collections.Generic;
using System.Linq;

namespace _04
{
    class Program
    {
        static void Main(string[] args)
        {

            p1();

        }

        static void p1()
        {
            var input = System
                .IO
                .File
                .ReadAllLines("input.txt")
                .ToList();

            var numbers = input[0].Split(",").Select(x => int.Parse(x));

            var bingos = input.Skip(2);

            var bingoBoards = new List<BingoBoard>();

            for(var i = 0; i < bingos.Count(); i+=6)
            {

                var bingoBoard = new BingoBoard();

                for(var br = 0; br < 5; br++)
                {
                    var row = bingos.ElementAt(i+br);

                    var bingoRow = new List<int>();

                    for(var b = 0; b < 5; b++)
                    {
                        bingoRow.Add(int.Parse(string.Join("", row.Skip(b*3).Take(2)).Trim()));
                    }

                    bingoBoard.AddRow(bingoRow);
                }

                bingoBoards.Add(bingoBoard);
            }

            var winners = new List<(int,int)>();

            foreach(var number in numbers)
            {
                foreach(var bingo in bingoBoards.Where(x => !x.HasWon))
                {
                    var win = bingo.Win(number);

                    if(win)
                    {
                        winners.Add((bingo.Sum(), number));
                    }
                }
            }

            var firstWinner = winners.First();
            Console.WriteLine(firstWinner.ToString());
            Console.WriteLine(firstWinner.Item1 * firstWinner.Item2);

            Console.WriteLine("------------------");

            var lastWinner = winners.Last();

            Console.WriteLine(lastWinner.ToString());
            Console.WriteLine(lastWinner.Item1 * lastWinner.Item2);
        }

        public class BingoBoard
        {
            List<List<int>> data = new List<List<int>>();
            public bool HasWon = false;
            public BingoBoard()
            {
                
            }

            public void AddRow(List<int> row)
            {
                data.Add(row);
            }

            public bool Win(int number)
            {
                for(var y = 0; y < 5; y++)
                {
                    for(var x = 0; x < 5; x++)
                    {
                        if(data[y][x] == number)
                        {
                            data[y][x] = -1;

                            var sumy = Enumerable.Range(0,5).Sum(i => (data[y][i]));
                            var sumx = Enumerable.Range(0,5).Sum(i => (data[i][x]));

                            if(sumx == -5 || sumy == -5)
                            {
                                HasWon = true;
                                return true;
                            }
                        }
                    }
                }

                return false;
            }

            public int Sum()
            {
                return data.SelectMany(x => x.Select(y => y)).Where(x => x >= 0).Sum();
            }
        }
    }
}
