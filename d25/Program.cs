// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var input = File
    .ReadAllLines("input.txt")
    .ToArray();


var w = new Stopwatch();
w.Start();

var cucumbers = new Dictionary<(int x, int y), (int x, int y)>();

for (var y = 0; y < input.Length; y++)
{
    for (var x = 0; x < input[0].Length; x++)
    {
        var c = input[y][x];

        if (c == 'v')
        {
            cucumbers[(x, y)] = (0, 1);
        }
        else if (c == '>')
        {
            cucumbers[(x, y)] = (1, 0);
        }
    }
}

var yMax = input.Length;

var xMax = input[0].Length;

P1();

Console.WriteLine("Time: " + w.Elapsed.TotalSeconds);

void P1()
{
    long moved = 1000;
    var step = 1;
    do
    {
        moved = 0;

        var easts = cucumbers.Where(c => c.Value == (1, 0)).ToArray();
        var souths = cucumbers.Where(c => c.Value == (0, 1)).ToArray();

        var add = new List<((int x, int y), (int x, int y))>();
        var remove = new List<(int x, int y)>();


        foreach (var east in easts)
        {
            var currentPos = east.Key;
            var newPos = (east.Key.x + east.Value.x, east.Key.y + east.Value.y);

            if (newPos.Item1 >= xMax)
            {
                newPos.Item1 = 0;
            }

            if (!cucumbers.ContainsKey(newPos))
            {
                moved++;
                add.Add((newPos, east.Value));
                remove.Add(currentPos);
            }
        }

        remove.ForEach(x => cucumbers.Remove(x));
        add.ForEach(x => cucumbers[x.Item1] = x.Item2);

        remove.Clear();
        add.Clear();

        foreach (var south in souths)
        {
            var currentPos = south.Key;
            var newPos = (south.Key.x + south.Value.x, south.Key.y + south.Value.y);

            if (newPos.Item2 >= yMax)
            {
                newPos.Item2 = 0;
            }

            if (!cucumbers.ContainsKey(newPos))
            {
                moved++;

                add.Add((newPos, south.Value));
                remove.Add(currentPos);
            }
        }

        remove.ForEach(x => cucumbers.Remove(x));
        add.ForEach(x => cucumbers[x.Item1] = x.Item2);

        Console.WriteLine($"Step {step}. Moved: {moved}");
        //Print();

        step++;

    } while (moved > 0);


}

void Print()
{
    for (var y = 0; y < input.Length; y++)
    {
        for (var x = 0; x < input[0].Length; x++)
        {
            if (cucumbers.ContainsKey((x, y)))
            {
                Console.Write(cucumbers[(x, y)] == (1, 0) ? ">" : "v");
            }
            else
            {
                Console.Write(".");
            }
        }

        Console.WriteLine("");

    }

    Console.WriteLine("");

}