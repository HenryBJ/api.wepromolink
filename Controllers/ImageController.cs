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

    private readonly ILogger<ImageController> _logger;
    private readonly IImageService _service;

    public ImageController(ILogger<ImageController> logger, IImageService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost]
    [Authorize]
    [Route("upload")]
    public async Task<IActionResult> Upload(IFormFile image, CancellationToken cancellationToken)
    {
        try
        {
            if (image == null || image.Length <= 0) return BadRequest("No image uploaded.");
            var result = await _service.ProcessImage(image);          
            return new OkObjectResult(result);
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

    [HttpGet]
    [Authorize]
    [Route("get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            var result = await _service.GetImage(id);          
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

}
