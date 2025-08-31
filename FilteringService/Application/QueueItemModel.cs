namespace FilteringService.Application
{
    public class QueueItemModel
    {
        public string UploadId { get; set; } = null!;
        public string FullText { get; set; } = null!;
        public string Chunk { get; set; } = null!;
    }
}
