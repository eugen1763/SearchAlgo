namespace SearchAlgo.Algo;

public static class Levenshtein
{
    public static int CalculateDistance(string str1, string str2)
    {
        var dp = new int[str1.Length + 1, str2.Length + 1];

        // Initialize the first row and column of the dp matrix
        for (var i = 0; i <= str1.Length; i++)
        {
            dp[i, 0] = i;
        }

        for (var j = 0; j <= str2.Length; j++)
        {
            dp[0, j] = j;
        }

        // Fill in the rest of the dp matrix
        for (var i = 1; i <= str1.Length; i++)
        {
            for (var j = 1; j <= str2.Length; j++)
            {
                var substitutionCost = (str1[i - 1] == str2[j - 1]) ? 0 : 1;

                dp[i, j] = Math.Min(
                    dp[i - 1, j] + 1,                     // Deletion
                    Math.Min(dp[i, j - 1] + 1,             // Insertion
                        dp[i - 1, j - 1] + substitutionCost)  // Substitution
                );
            }
        }

        // Return the minimum edit distance
        return dp[str1.Length, str2.Length];
    }
}