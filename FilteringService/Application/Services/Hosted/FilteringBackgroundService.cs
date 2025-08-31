using FilteringService.Application.Services.Abstract;
using System.Collections.Concurrent;

namespace FilteringService.Application.Services.Hosted
{
    public class FilteringBackgroundService : BackgroundService
    {
        private readonly BlockingCollection<QueueItemModel> _backgroundQueue;
        private readonly IUploadQueueService _queueManager;
        private readonly IFilterService _filterService;

        public FilteringBackgroundService(BlockingCollection<QueueItemModel> backgroundQueue, IUploadQueueService queueManager, IFilterService filterService)
        {
            _backgroundQueue = backgroundQueue;
            _queueManager = queueManager;
            _filterService = filterService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var item = _backgroundQueue.Take(stoppingToken);
                    var filteredChunk = _filterService.FilterChunk(item.Chunk);
                    _queueManager.EnqueueChunk(item.UploadId, filteredChunk);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
       
    }
}
