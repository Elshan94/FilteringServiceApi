using F23.StringSimilarity;
using FilteringService.Application.Services.Abstract;

namespace FilteringService.Application.Services.Concrete
{
    public class JaroWinklerFilterService : IFilterStrategy
    {
        private readonly JaroWinkler _jaroWinkler;

        public JaroWinklerFilterService()
        {
            _jaroWinkler = new JaroWinkler();
        }

        public bool IsSimilar(string word, string filterWord, double threshold)
        {
            return _jaroWinkler.Similarity(word, filterWord) >= threshold;
        }
    }
}
