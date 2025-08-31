using FilteringService.Application.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilteringServiceTest
{
    public class LevensteinFilterServiceTests
    {
        private readonly LevenshteinFilterService _service = new LevenshteinFilterService();

        [Theory]
        [InlineData("test1", "test1", true)]   
        [InlineData("test2", "test2", true)]   
        [InlineData("t3st1", "test1", true)]   
        [InlineData("best1", "test1", true)]  
        [InlineData("tes", "test1", false)]    
        [InlineData("random", "test1", false)] 
        public void IsSimilar_WorksCorrectly(string word, string filterWord, bool expected)
        {
            double threshold = 0.8;

            bool result = _service.IsSimilar(word, filterWord, threshold);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("hello", "hallo", 1)]
        [InlineData("hello", "helloo", 1)]
        [InlineData("hello", "helo", 1)]
        [InlineData("abc", "xyz", 3)]
        [InlineData("test1", "test1", 0)]
        public void CallculateSimilarity_ReturnsCorrectDistance(string text, string filter, int expectedDistance)
        {
            int distance = _service.CallculateSimilarity(text, filter);

            Assert.Equal(expectedDistance, distance);
        }

    }
}
