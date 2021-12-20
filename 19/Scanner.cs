namespace AoCTwentyOne
{
    public class Scanner
    {
        public List<(int x, int y, int z)> Points = new List<(int x, int y, int z)>();

        public List<List<(int x, int y, int z)>> PointsVariations = new List<List<(int x, int y, int z)>>();

        public Scanner(String scannerdata)
        {
            var rows = scannerdata.Split(Environment.NewLine);

            Name = rows.First();

            Points = rows
                .Skip(1)
                .Select(x => x.Split(","))
                .Select(x => (int.Parse(x[0]), int.Parse(x[1]), int.Parse(x[2])))
                .ToList();

            foreach (var p in Points)
            {
                var pa = new List<(int x, int y, int z)>();
                for (var d = -1; d < 2; d += 2)
                {
                    for (var z = -1; z < 2; z += 2)
                    {
                        for (var y = -1; y < 2; y += 2)
                        {
                            for (var x = -1; x < 2; x += 2)
                            {
                                pa.Add((x * p.x, y * p.y, z * p.z));
                            }
                        }
                    }
                }

                PointsVariations.Add(pa);
            }
        }

        public bool Overlap(Scanner other)
        {
            return true;
        }

        public string Name { get; }
    }
}