using System.Runtime.CompilerServices;

namespace FilteringService.Application.Services.Abstract
{
    public interface IUploadQueueService
    {
        public void EnqueueChunk(string uploadId, string chunk);
        public IAsyncEnumerable<string> GetChunksAsync(string uploadId, [EnumeratorCancellation] CancellationToken cancellationToken);
    }
}
