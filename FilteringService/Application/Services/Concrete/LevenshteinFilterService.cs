using FilteringService.Application.Services.Abstract;

namespace FilteringService.Application.Services.Concrete
{
    public class LevenshteinFilterService : IFilterStrategy
    {
        public LevenshteinFilterService()
        {
        }

        public bool IsSimilar(string word, string filterWord, double threshold)
        {
            var distance = CallculateSimilarity(word, filterWord);
            int maxLength = Math.Max(word.Length, filterWord.Length);
            double similarity = 1.0 - ((double)distance / maxLength);
            return similarity >= threshold;
        }

        public int CallculateSimilarity(string text, string filter)
        {
            int n = text.Length;

            int m = filter.Length;

            int[,] dp = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
                dp[i, 0] = i;

            for (int j = 0; j <= m; j++)
                dp[0, j] = j;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = text[i - 1] == filter[j - 1] ? 0 : 1;
                    dp[i, j] = Math.Min(
                        Math.Min(dp[i - 1, j] + 1,
                                 dp[i, j - 1] + 1),
                        dp[i - 1, j - 1] + cost
                    );
                }
            }

            return dp[n, m];
        }
    }
}
