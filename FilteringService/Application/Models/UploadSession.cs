namespace FilteringService.Application.Models
{
    public class UploadSessionModel
    {
        public string UploadId { get; set; } = null!;
        public string Data { get; set; } = null!;
        public int Index { get; set; }
        public bool IsLastChunk { get; set; }
    }
}
