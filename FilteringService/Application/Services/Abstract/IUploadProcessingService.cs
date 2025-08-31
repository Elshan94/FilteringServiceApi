using CSharpFunctionalExtensions;
using FilteringService.Application.Models;

namespace FilteringService.Application.Services.Abstract
{
    public interface IUploadProcessingService
    {
        public Result<bool> ProcessChunk(UploadSessionModel chunkModel);
    }
}
