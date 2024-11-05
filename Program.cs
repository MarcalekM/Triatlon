using System.Collections.Concurrent;
using Triatlon;

List <Competitor> competitors = new();
using StreamReader sr = new(
    path: @"../../../src/forras.txt",
    encoding: System.Text.Encoding.UTF8);
while(!sr.EndOfStream)competitors.Add(new(sr.ReadLine()));
Console.WriteLine($"Célba érkezők száma: {competitors.Count}");

//hány %-a férfi a versenyzőknek?
var f1 = competitors.Where(c => c.Gender).ToList().Count / (float)competitors.Count * 100;
Console.WriteLine(f1 + "%-a férfi a versenyzőknek\n");

//első célbaérkező női versenyző
var f2 = competitors.Where(c => !c.Gender).MinBy(c => c.TotalTimeInSec);
Console.WriteLine($"Az első célba érkező nő: {f2.ToString()}\n");

//átlagos depóban töltött idő
var f3 = competitors.Average(c  => (c.RaceTimes["1st depot"] + c.RaceTimes["2nd depot"]).TotalSeconds / 2);
Console.WriteLine($"Átlagos depóban töltött idő: {f3:0.00} másodperc\n");

//korkategóriánként a versenyzők száma
var f4 = competitors.GroupBy(c => c.AgeCategory).OrderBy(c => c.Key);
Console.WriteLine("Versenyzők száma kategóriánként:");
foreach(var f in f4) Console.WriteLine($"\t{f.Key,11}: {f.Count(),2} fő");

//átlagos versenyidő férfiaknél korkategóriánként
var f5 = competitors
    .Where(c => c.Gender)
    .GroupBy(c => c.AgeCategory)
    .ToDictionary(g => g.Key, g => g.Average(c => c.TotalTimeInSec))
    .OrderBy(d =>d.Value);
Console.WriteLine($"Átlagos versenyidő a férfiaknál korkategóriánként:");
foreach(var f in f5) Console.WriteLine($"\t{f.Key,11}: {f.Value / 60:0.0000} perc");
