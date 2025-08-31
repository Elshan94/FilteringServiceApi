using FilteringService.Application.Services.Abstract;
using FilteringService.Application.Models;
using System;
using System.Collections.Concurrent;
using System.Text;

namespace FilteringService.Application.Services.Concrete
{
    public class SessionManagingService : ISessionManagingService
    {
        private readonly ConcurrentDictionary<string, SortedDictionary<int, string>> _sessions = new();

        public void SaveChunk(string uploadId, int index, string chunk)
        {
            var chunks = _sessions.GetOrAdd(uploadId, _ => new SortedDictionary<int, string>());
            chunks.TryAdd(index, chunk);
        }

        public SortedDictionary<int, string>? GetChunks(string uploadId)
            => _sessions.TryGetValue(uploadId, out var chunks) ? chunks : null;

        public void RemoveSession(string uploadId)
            => _sessions.TryRemove(uploadId, out _);
    }
}
