using FilteringService.Application.Services.Abstract;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace FilteringService.Application.Services.Concrete
{
    public class UploadQueueService : IUploadQueueService
    {
        private readonly ConcurrentDictionary<string, ConcurrentQueue<string>> _queue = new();

        public void EnqueueChunk(string uploadId, string chunk)
        {
            var q = _queue.GetOrAdd(uploadId, _ => new ConcurrentQueue<string>());
            q.Enqueue(chunk);
        }

        public async IAsyncEnumerable<string> GetChunksAsync(
         string uploadId,
         [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            ConcurrentQueue<string>? q = null;

            if (!_queue.TryGetValue(uploadId, out  q) || q == null)
                yield break;
            

            while (!q.IsEmpty)
            {
                if (q.TryDequeue(out var chunk))
                {
                    yield return chunk;
                    await Task.Delay(5000);
                }
            }

            _queue.TryRemove(uploadId, out _);
        }
    }
}
