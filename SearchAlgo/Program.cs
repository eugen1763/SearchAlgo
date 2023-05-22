// See https://aka.ms/new-console-template for more information

using SearchAlgo.Algo;

var searcher = new Searcher<Searchable>();
var objs = new[]
{
    new Searchable("Mattermost"),
    new Searchable("Microsoft Teams"),
    new Searchable("Windows Explorer"),
    new Searchable("Microsoft Word"),
};

foreach (var searchable in objs)
{
    searcher.RegisterSearchable(searchable);
}

while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine();

    var hits = searcher.Find(input ?? "");

    if (hits.Length == 0)
    {
        Console.WriteLine("No hits");
    }
    else
    {
        foreach (var hit in hits)
        {
            Console.WriteLine(hit.ComparisonString);
        }
    }

    Console.WriteLine();
}

internal sealed class Searchable : ISearchable
{
    public Searchable(string comparisonString)
    {
        ComparisonString = comparisonString;
    }

    public string ComparisonString { get; }
}