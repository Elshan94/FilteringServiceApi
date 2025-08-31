using FilteringService.Application.Models;
using FilteringService.Application.Services.Abstract;
using System.Collections.Concurrent;
using CSharpFunctionalExtensions;

namespace FilteringService.Application.Services.Concrete
{
    public class UploadProcessingService : IUploadProcessingService
    {
        private readonly ISessionManagingService _sessionManager;
        private readonly BlockingCollection<QueueItemModel> _backgroundQueue;

        public UploadProcessingService(ISessionManagingService sessionManager, BlockingCollection<QueueItemModel> backgroundQueue)
        {
            _sessionManager = sessionManager;
            _backgroundQueue = backgroundQueue;
        }

        public Result<bool> ProcessChunk(UploadSessionModel model)
        {
            try
            {
                _sessionManager.SaveChunk(model.UploadId, model.Index, model.Data);

                if (model.IsLastChunk)
                {
                    var chunks = _sessionManager.GetChunks(model.UploadId);

                    foreach (var kv in chunks!)
                    {
                        _backgroundQueue.Add(new QueueItemModel { UploadId = model.UploadId, Chunk = kv.Value });
                    }

                    _sessionManager.RemoveSession(model.UploadId);
                }

                return Result.Success(true);
            }
            catch (Exception ex)
            {
                return Result.Failure<bool>(ex.Message);
            }
        }
    }
}
