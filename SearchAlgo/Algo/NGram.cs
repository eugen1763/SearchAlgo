namespace SearchAlgo.Algo;

public static class NGram
{
    public static IEnumerable<string> Split(string input, int n)
    {
        var ngrams = new List<string>();

        // Remove any leading or trailing whitespace
        input = input.Trim();

        // Check if the input string is smaller than the specified n
        if (input.Length <= n)
        {
            ngrams.Add(input);
            return ngrams;
        }

        // Iterate over the input string
        for (var i = 0; i <= input.Length - n; i++)
        {
            // Extract the n-gram substring
            var ngram = input.Substring(i, n);

            // Add the n-gram to the list
            ngrams.Add(ngram);
        }

        return ngrams;
    }
}