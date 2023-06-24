using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WePromoLink.DTO;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{

    private readonly BlobServiceClient _service;
    private readonly ILogger<PricingController> _logger;

    public ImageController(BlobServiceClient service, ILogger<PricingController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    [Authorize]
    [Route("upload")]
    public async Task<IActionResult> Upload(IFormFile image, CancellationToken cancellationToken)
    {
        try
        {
            if (image == null || image.Length <= 0) return BadRequest("No image uploaded.");
            var _client = _service.GetBlobContainerClient("campaigns");
            if (_client == null) throw new Exception("Container not found");

            string ext = Path.GetExtension(image.FileName);
            string name = $"image{Nanoid.Nanoid.Generate("0123456789", 15)}{ext}";
            var _blobclient = _client.GetBlobClient(name);
            await _blobclient.UploadAsync(image.OpenReadStream(), true, cancellationToken);

            var url = _blobclient.Uri.ToString();

            return new OkObjectResult(url);
        }
        
        catch (TaskCanceledException)
        {
            _logger.LogInformation("Image upload canceled.");
            return new StatusCodeResult(499); // Estado personalizado: tarea cancelada
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

}
