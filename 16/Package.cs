using System;
using System.Collections.Generic;
using System.Linq;

public class Package
{
    public long Version { get; set; }
    public string Type { get; set; }

    public string Rest { get; set; }

    public long LiteralValue { get; set; }

    public string LengthTypeId { get; set; }

    public long LengthTypeValue { get; set; }

    public int CharsUsedCount { get; set; }
    public string CharsUsed { get; set; }

    public int Level { get; set; }

    public Package[] SubPackages { get; set; }

    public long Value { get; set; }

    public long TotalVersion { get; set; }

    public Package(string s, int level)
    {
        Level = level;
        Version = long.Parse(ToHex(s.Substring(0, 3)));
        Type = ToHex(s.Substring(3, 3));
        Rest = s.Substring(6);
        SubPackages = new Package[0];

        if (Type != "4")
        {
            LengthTypeId = Rest.Substring(0, 1);

            if (LengthTypeId == "0")
            {
                LengthTypeValue = Convert.ToInt64(Rest.Substring(1, 15), 2);
                Rest = Rest.Substring(16);
            }
            else
            {
                LengthTypeValue = Convert.ToInt64(Rest.Substring(1, 11), 2);
                Rest = Rest.Substring(12);
            }
        }
        else
        {

            var rest = Rest;
            var value = "";
            do
            {
                var batch = rest.Substring(0, 5);
                value += rest.Substring(1, 4);

                rest = rest.Substring(5);

                if (batch[0] == '0')
                {
                    break;
                }

            } while (true);

            LiteralValue = Convert.ToInt64(value, 2);

            Rest = rest;
        }



        if (!Rest.All(x => x == '0') && Type != "4")
        {
            CreateSubPackages();
        }

        var values = SubPackages.Select(p => p.Value);

        Value = Type switch
        {
            "0" => values.Sum(),
            "1" => values.Skip(1).Aggregate(values.First(), (long a, long b) => (a * b)),
            "2" => values.Min(),
            "3" => values.Max(),
            "4" => LiteralValue,
            "5" => values.First() > values.Last() ? 1 : 0,
            "6" => values.First() < values.Last() ? 1 : 0,
            "7" => values.First() == values.Last() ? 1 : 0,
            _ => throw new Exception($"Unexpected type value {Type}")
        };

        TotalVersion = Version + SubPackages.Sum(x => x.TotalVersion);

        CharsUsedCount = s.Length - Rest.Length;
        CharsUsed = s.Substring(0, CharsUsedCount);
    }

    public void CreateSubPackages()
    {


        var list = new List<Package>();

        var totalChars = 0;
        if (LengthTypeId == "0")
        {
            var rest = Rest.Substring(0, (int)LengthTypeValue);
            do
            {
                var x = new Package(rest, Level + 1);
                totalChars += x.CharsUsedCount;
                rest = x.Rest;
                x.Rest = string.Empty;
                list.Add(x);
            } while (totalChars <= LengthTypeValue && !rest.All(x => x == '0'));
        }
        else if (LengthTypeId == "1")
        {
            var rest = Rest;
            for (var i = 0; i < LengthTypeValue; i++)
            {
                var x = new Package(rest, Level + 1);
                rest = x.Rest;
                list.Add(x);
            }
        }
        else
        {
            throw new Exception("Unexpected length type id " + LengthTypeId);
        }

        SubPackages = list.ToArray();

        Rest = Rest.Substring(SubPackages.Sum(x => x.CharsUsedCount));
    }

    string ToHex(string s)
    {
        return Convert.ToInt32(s, 2).ToString("X");
    }

    public override string ToString()
    {
        return string.Join("-", Enumerable.Range(0, Level).Select(x => "")) + $"Version: {Version} - Type: {Type}. Value: {Value}";
    }
}
