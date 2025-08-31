using FilteringService.Application.Models;
using FilteringService.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FilteringService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        public IUploadProcessingService UploadProcessingService { get; }
        public IUploadQueueService UploadQueueService { get; }

        public UploadController(IUploadProcessingService uploadProcessingService, IUploadQueueService uploadQueueService)
        {
            UploadProcessingService = uploadProcessingService;
            UploadQueueService = uploadQueueService;
        }


        [HttpPost("upload")]
        public IActionResult UploadChunk([FromBody] UploadSessionModel model)
        {
            if (string.IsNullOrEmpty(model.UploadId))
                return BadRequest("uploadId is required.");

            var result = UploadProcessingService.ProcessChunk(model);

            if (result.IsSuccess)
            {
                return Accepted(new { status = "Accepted" });
            }

            return BadRequest(new { status = "Failed", error = result.Error });
        }

        [HttpGet("results/download/{uploadId}")]
        public async Task DownloadFilteredResultAsync(string uploadId, CancellationToken cancellationToken)
        {
            Response.ContentType = "text/plain";
            Response.Headers["Content-Disposition"] = $"attachment; filename={uploadId}.txt";

            await foreach (var chunk in UploadQueueService.GetChunksAsync(uploadId, cancellationToken))
            {
                var bytes = Encoding.UTF8.GetBytes(chunk);
                await Response.Body.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);
            }
        }
 
    }
}
