using System;
using System.Collections.Generic;
using System.Linq;

namespace _05
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

        }
    }
}
