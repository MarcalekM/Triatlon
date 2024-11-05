using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triatlon;

internal class Competitor
{
    public string Name { get; set; }
    public int YearOfBirth { get; set; }
    public string RaceNumber { get; set; }
    public bool Gender { get; set; }
    public string AgeCategory { get; set; }
    public Dictionary<string, TimeSpan> RaceTimes { get; set; }
    public double TotalTimeInSec => RaceTimes.Values.Sum(x => x.TotalSeconds);

    public Competitor(string row)
    {
        var v = row.Split(';');
        Name = v[0];
        YearOfBirth = int.Parse(v[1]);
        RaceNumber = v[2];
        Gender = v[3] == "f";
        AgeCategory = v[4];
        RaceTimes = new()
        {
            {"swimming", TimeSpan.Parse(v[5])},
            {"1st depot", TimeSpan.Parse(v[6])},
            {"cycling", TimeSpan.Parse(v[7])},
            {"2nd depot", TimeSpan.Parse(v[8])},
            {"running", TimeSpan.Parse(v[9])}
        };
    }

    public override string ToString()
    {
        return $"\t[{RaceNumber}] {Name} {AgeCategory} ({(Gender ? "férfi" : "nő")})\n";
    }
}