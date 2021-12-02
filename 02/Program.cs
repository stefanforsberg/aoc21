using System;
using System.Linq;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {

            p1();
            p2();
           
        }

        static void p1() 
        {
            var x = 0;
            var y = 0;

            System
                .IO
                .File
                .ReadAllLines("input.txt")
                .ToList()
                .ForEach(r => { 
                    var row = r.Split(" ");
                    var value = int.Parse(row[1]);

                    switch(row[0]) 
                    {
                        case "forward": x += value; break;
                        case "up": y -= value; break;
                        case "down": y += value; break;
                    }
                });

            Console.WriteLine($"Horizontal: {x}, depth: {y}, Multiplied: {x*y}");
        }

        static void p2() 
        {
            var input = System
                .IO
                .File
                .ReadAllLines("input.txt")
                .Select(x => { 
                    var row = x.Split(" ");

                    return new {
                        Cmd = row[0],
                        Value = int.Parse(row[1])
                    };
                });

            var x = 0;
            var y = 0;
            var aim = 0;

            foreach(var i in input) 
            {
                if(i.Cmd == "forward") 
                {
                    x += i.Value;
                    y += aim*i.Value;
                }
                
                if(i.Cmd == "up") 
                {
                    aim -= i.Value;
                }
                
                if(i.Cmd == "down") 
                {
                    aim += i.Value;
                }
            }

            Console.WriteLine($"Horizontal: {x}, depth: {y}, aim: {aim}. Multiplied: {x*y}");
        }

        
    }
}
