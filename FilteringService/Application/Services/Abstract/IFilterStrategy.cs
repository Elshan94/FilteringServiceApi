namespace FilteringService.Application.Services.Abstract
{
    public interface IFilterStrategy
    {
        bool IsSimilar(string word, string filterWord, double threshold);
    }
}
