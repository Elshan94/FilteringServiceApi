namespace FilteringService.Application.Services.Abstract
{
    public interface IMemoryFilteredStorage
    {
        public void Add(string uploadId, string filteredText);
        string? GetByUploadId(string uploadId);
    }
}
