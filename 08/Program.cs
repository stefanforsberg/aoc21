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
