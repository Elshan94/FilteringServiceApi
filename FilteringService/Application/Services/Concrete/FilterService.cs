using F23.StringSimilarity;
using FilteringService.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilteringService.Application.Services.Concrete
{
    public class FilterService : IFilterService
    {
        private readonly List<string> _filterWords;
        private readonly double _similarityThreshold;
        private readonly IFilterStrategy _filterStrategy = null!;

        public FilterService(IConfiguration configuration)
        {
            _filterWords = configuration.GetSection("Filtering:ExcludedWords").Get<List<string>>() ?? new List<string>();

            _similarityThreshold = configuration.GetValue<double>("Filtering:SimilarityThreshold", 0.8);

            var algorithmType = configuration.GetValue<string>("Filtering:Algorithm", "JaroWinkler");

            _filterStrategy = algorithmType switch
            {
                "Levenshtein" => new LevenshteinFilterService(),
                _ => new JaroWinklerFilterService()
            };
        }

        public string FilterChunk(string chunk)
        {
            if (string.IsNullOrWhiteSpace(chunk))
                return chunk;

            var words = chunk.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var filtered = new List<string>();

            foreach (var word in words)
            {
                var isSimilar = _filterWords.Any(filter =>
                    _filterStrategy.IsSimilar(word, filter, _similarityThreshold));

                if (!isSimilar)
                    filtered.Add(word);
            }

            return string.Join(' ', filtered);
        }
    }
}
