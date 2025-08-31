using FilteringService.Application.Services.Abstract;
using System.Collections.Concurrent;
using System.Reflection;

namespace FilteringService.Application.Services.Concrete
{
    public class MemoryFilteredStorage : IMemoryFilteredStorage
    {
        private readonly ConcurrentDictionary<string, string> _results = new();

        public void Add(string uploadId, string filteredText)
        {
            _results.AddOrUpdate(
                uploadId,                  
                filteredText,               
                (key, oldValue) => filteredText  
            );
        }

        public string? GetByUploadId(string uploadId)
        {
            _results.TryGetValue(uploadId, out var text);
            return text;
        }

        public bool Remove(string uploadId)
        {
            return _results.TryRemove(uploadId, out _);
        }

    }
}
