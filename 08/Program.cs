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
                var d0 = new string[7];
                var d4 = new string[7];
                var d7 = new string[7];
                var d8 = new string[7];

                var solution = new[] {"-","-","-","-","-","-","-"} ;

                var sol = new string[10];

                var n = new Dictionary<string, int>();

                

                var four = i.input.FirstOrDefault(x => x.Length == 4);
                sol[4] = four;
                n[four] = 4;

                var one = i.input.FirstOrDefault(x => x.Length == 2);
                sol[1] = one;
                n[one] = 1;


                var eight = i.input.FirstOrDefault(x => x.Length == 7);
                sol[8] = eight;
                n[eight] = 8;


                var seven = i.input.FirstOrDefault(x => x.Length == 3);
                sol[7] = seven;
                n[seven] = 7;

                var c690 = i.input.Where(x => x.Length == 6);
                var six = i.input.First(x => x.Length == 6 && x.Intersect(one).Count() == 1);
                sol[6] = six;
                n[six] = 6;

                var nine = i.input.First(x => x.Length == 6 && x.Intersect(four).Count() == 4);
                sol[9] = nine;
                n[nine] = 9;


                var zero = c690.Except(new[] { six, nine}).First();
                sol[0] = zero;
                n[zero] = 0;


                var c235 = i.input.Where(x => x.Length == 5);
                var three = c235.First(x => x.Intersect(one).Count() == 2);

                var five = c235.First(x => x.Intersect(six).Count() == 5);

                var two = c235.Except(new[] { three, five}).First();

                sol[3] = three;
                sol[5] = five;
                sol[2] = two;

                n[three] = 3;
                n[five] = 5;
                n[two] = 2;



                total += int.Parse(string.Join("", i.output.Select(x => n[x])));








                // var seven = i.input.FirstOrDefault(x => x.Length == 3);
                // if(seven != null)
                // {
                //     var d = seven.Except(one).ToArray();

                //     d7[0] = d[0].ToString();

                //     solution[0] =  d[0].ToString();

                // }

                // if(four != null)
                // {
                //     var d = four.Except(zero).ToArray();

                //     solution[3] = d[0].ToString();

                //     // var r = four.Where(x => x.ToString() != solution[2] && x.ToString() != solution[5]);

                //     // d4[1] = four.Substring(0,1);
                //     // d4[2] = four.Substring(1,1);
                //     // d4[3] = four.Substring(2,1);
                //     // d4[5] = four.Substring(3,1);
                // }



                // var eight = i.input.FirstOrDefault(x => x.Length == 7);
                // if(eight != null)
                // {
                //     d8[0] = eight.Substring(0,1);
                //     d8[1] = eight.Substring(1,1);
                //     d8[2] = eight.Substring(2,1);
                //     d8[3] = eight.Substring(3,1);
                //     d8[4] = eight.Substring(4,1);
                //     d8[5] = eight.Substring(5,1);
                //     d8[6] = eight.Substring(6,1);

                // }

                // Console.WriteLine("-" + solution[0] + "-");
                // Console.WriteLine(solution[1] + "-" + solution[2]);
                // Console.WriteLine("-" + solution[3] + "-");
                // Console.WriteLine(solution[4] + "-" + solution[5]);
                // Console.WriteLine("-" + solution[6] + "-");








            }

            Console.WriteLine("Total: " + total);


        }
    }
}
