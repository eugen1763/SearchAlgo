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
        var hits = new List<SearcherResult>();
        
        var queries = NGram.Split(input, (int)(input.Length * .8)).ToArray();

        foreach (var entry in _entries)
        {
            if (entry.Terms.Any(x => x == input))
            {
                hits.Add(new SearcherResult(entry.Obj, 0));
                continue;
            }

            var bestDistance = input.Length;
            var bestObj = default(TSearchable);

            foreach (var term in entry.Terms)
            {
                foreach (var query in queries)
                {
                    var distance = Levenshtein.CalculateDistance(term, query);
                    if (distance >= bestDistance) 
                        continue;
                    bestDistance = distance;
                    bestObj = entry.Obj;
                }
            }
            
            if (bestObj is null)
                continue;
            
            hits.Add(new SearcherResult(entry.Obj, bestDistance));
        }

        return hits
            .OrderBy(x => x.Accuracy)
            .Select(x => x.Obj)
            .ToArray();
    }
}