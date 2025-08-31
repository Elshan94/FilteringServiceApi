using FilteringService.Application.Models;
using System.Collections.Concurrent;

namespace FilteringService.Application.Services.Abstract
{
    public interface ISessionManagingService
    {
        public void SaveChunk(string uploadId, int index, string chunk);
        public SortedDictionary<int, string>? GetChunks(string uploadId);
        public void RemoveSession(string uploadId);
    }
}
