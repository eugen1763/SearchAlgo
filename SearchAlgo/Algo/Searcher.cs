namespace SearchAlgo.Algo;

public sealed class Searcher<TSearchable> where TSearchable : ISearchable
{
    private sealed record SearcherEntry(TSearchable Obj, string[] Terms);

    private sealed record SearcherResult(TSearchable Obj, int Accuracy);

    private readonly List<SearcherEntry> _entries = new();

    public void RegisterSearchable(TSearchable obj)
    {
        var terms = obj.ComparisonString.Split(new[] { ' ', ',', '.', '\n' },
            StringSplitOptions.RemoveEmptyEntries);
        _entries.Add(new SearcherEntry(obj, terms));
    }

    public TSearchable[] Find(string input)
    {
        var hits = from entry in _entries
            let bestHit = entry.Terms.Select(x => NGram.Split(x, input.Length))
                .SelectMany(x => x.Select(y => new { query = y, distance = Levenshtein.CalculateDistance(y, input) }))
                .MinBy(x => x.distance)
            where bestHit is not null
            select new SearcherResult(entry.Obj, bestHit.distance);

        return hits
            .OrderBy(x => x.Accuracy)
            .Select(x => x.Obj)
            .ToArray();
    }
}