using System.Text.Json;
using System.Text.Json.Serialization;

namespace xxx
{
    public class SNumber
    {
        public long LiteralLeft = -1;
        public long LiteralRight = -1;
        public long Literal = -1;

        [JsonIgnore]
        public int ParentCount { get; set; }

        [JsonInclude]
        public List<SNumber> Children = new List<SNumber>();

        [JsonIgnore]
        public SNumber? Parent = null;

        public SNumber(string s) : this(System.Text.Json.JsonDocument.Parse(s).RootElement)
        {

        }

        public SNumber(JsonElement j, SNumber? parent = null)
        {
            this.Parent = parent;

            ParentCount = parent == null ? 0 : parent.ParentCount + 1;

            if (j.ValueKind == JsonValueKind.Array)
            {
                foreach (var je in j.EnumerateArray())
                {
                    Children.Add(new SNumber(je, this));
                }
            }
            else if (j.ValueKind == JsonValueKind.Number)
            {
                Literal = j.GetInt64();
            }
        }

        public SNumber[] Flatten()
        {
            return new[] { this }.Concat(Children.SelectMany(x => x.Flatten())).ToArray();
        }

        public static SNumber Add(SNumber a, SNumber b)
        {
            return new SNumber(new SNumber(a, b).ToString());
        }

        public SNumber(SNumber a, SNumber b)
        {
            Children.Add(a);
            Children.Add(b);
        }

        public bool Explode()
        {
            var z = this.Flatten();

            for (var i = 0; i < z.Length; i++)
            {
                var sni = z[i];

                if (sni.ParentCount >= 4 && sni.Children.Count(c => c.Literal > -1) == 2)
                {
                    var ll = z.Take(i).Where(x => x.Literal > -1).ToArray();
                    var rr = z.Skip(i).Where(x => x.Literal > -1).Skip(2).ToArray();

                    if (ll.Any()) ll.Last().Literal += sni.Children[0].Literal;
                    if (rr.Any()) rr.First().Literal += sni.Children[1].Literal;

                    sni.Literal = 0;
                    sni.Children = Enumerable.Empty<SNumber>().ToList();

                    return true;
                }

            }

            return false;
        }

        public bool Split()
        {
            var z = this.Flatten();

            for (var i = 0; i < z.Length; i++)
            {
                var sni = z[i];

                if (sni.Literal >= 10)
                {
                    var l = Math.Floor((double)sni.Literal / 2);
                    var r = Math.Ceiling((double)sni.Literal / 2);

                    sni.Literal = -1;
                    sni.Children.Add(new SNumber(l.ToString()));
                    sni.Children.Add(new SNumber(r.ToString()));

                    return true;
                }

            }

            return false;
        }

        public long Magnitude()
        {
            if (Literal > -1) return Literal;

            return 3 * Children[0].Magnitude() + 2 * Children[1].Magnitude();
        }

        public override string ToString()
        {
            if (Literal > -1)
            {
                return Literal.ToString();
            }

            var s = "[";

            if (LiteralLeft >= 0) s += LiteralLeft + ",";

            s += string.Join(",", Children.Select(x => x.ToString()));

            if (LiteralRight >= 0) s += "," + LiteralRight;

            s += "]";

            return s.Replace(",,", ",");
        }
    }
}