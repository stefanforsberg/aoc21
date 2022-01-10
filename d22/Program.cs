// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var input = File
    .ReadAllLines("input.txt")
    .Select(x =>
    {
        var xstart = x.IndexOf("x=");
        var ystart = x.IndexOf("y=");
        var zstart = x.IndexOf("z=");

        var xPart = x.Substring(xstart + 2, ystart - 3 - xstart).Split("..");
        var yPart = x.Substring(ystart + 2, zstart - 3 - ystart).Split("..");
        var zPart = x.Substring(zstart + 2).Split("..");

        return new
        {
            on = x.Contains("on"),
            xMin = int.Parse(xPart[0]),
            xMax = int.Parse(xPart[1]),
            yMin = int.Parse(yPart[0]),
            yMax = int.Parse(yPart[1]),
            zMin = int.Parse(zPart[0]),
            zMax = int.Parse(zPart[1]),
        };
    })
    .ToArray();

var w = new Stopwatch();
w.Start();

P2();

Console.WriteLine("On: " + w.Elapsed.TotalSeconds);

void P1()
{
    var dic = new Dictionary<(int x, int y, int z), (int count, bool on)>();

    for (var x = -50; x <= 50; x++)
    {
        for (var y = -50; y <= 50; y++)
        {
            for (var z = -50; z <= 50; z++)
            {
                foreach (var box in input)
                {
                    if (box.xMax >= x && box.xMin <= x && box.yMax >= y && box.yMin <= y && box.zMax >= z && box.zMin <= z)
                    {
                        var key = (x, y, z);
                        if (!dic.ContainsKey(key))
                        {
                            dic[key] = (1, box.on);
                        }
                        else
                        {
                            var old = dic[key];
                            dic[key] = (old.count + 1, box.on);
                        }
                    }
                }
            }
        }
    }

    Console.WriteLine("On: " + dic.Count(d => d.Value.on));
}

void P2()
{
    (bool Overlaps, Cube? NewCube) Overlapping(Cube a, Cube b, bool on)
    {
        if (a.xMin > b.xMax || a.xMax < b.xMin
         || a.yMin > b.yMax || a.yMax < b.yMin
         || a.zMin > b.zMax || a.zMax < b.zMin)
        {
            return (false, null);
        }

        return
            (true, new Cube(on,
            Math.Max(a.xMin, b.xMin), Math.Min(a.xMax, b.xMax),
            Math.Max(a.yMin, b.yMin), Math.Min(a.yMax, b.yMax),
            Math.Max(a.zMin, b.zMin), Math.Min(a.zMax, b.zMax)
            ));
    }

    var allCubes = new List<Cube>();
    foreach (var cube in input.Select(x => new Cube(x.on, x.xMin, x.xMax, x.yMin, x.yMax, x.zMin, x.zMax)))
    {
        var cubesToAdd = new List<Cube>();
        if (cube.on)
        {
            cubesToAdd.Add(cube);
        }

        foreach (var alreadyAddedCube in allCubes)
        {
            var overlapResult = Overlapping(cube, alreadyAddedCube, !alreadyAddedCube.on);
            if (overlapResult.Overlaps)
            {
                cubesToAdd.Add(overlapResult.NewCube);
            }
        }

        allCubes.AddRange(cubesToAdd);
    }

    allCubes.ToList().ForEach(c => Console.WriteLine(c));

    var totalCubeVolumeOn = allCubes.Where(c => c.on).Select(c => c.Volume()).Sum();
    var totalCubeVolumeOff = allCubes.Where(c => !c.on).Select(c => c.Volume()).Sum();

    Console.WriteLine("totalCubeVolumeOn: " + totalCubeVolumeOn);
    Console.WriteLine("totalCubeVolumeOff: " + totalCubeVolumeOff);
    Console.WriteLine("Lit cubes: " + (totalCubeVolumeOn - totalCubeVolumeOff));

}

record Cube(bool on, int xMin, int xMax, int yMin, int yMax, int zMin, int zMax)
{
    public long Volume()
    {
        var xLength = xMax - xMin + 1;
        var yLength = yMax - yMin + 1;
        var zLength = zMax - zMin + 1;

        return (long)xLength * yLength * zLength;
    }
}

