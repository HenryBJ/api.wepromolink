using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WePromoLink.DTO;
using WePromoLink.Services;
using WePromoLink.Services.Profile;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{

    private readonly ILogger<ProfileController> _logger;
    private readonly IProfileService _service;

    public ProfileController(ILogger<ProfileController> logger, IProfileService service)
    {
        _logger = logger;
        _service = service;
    }

    // [HttpPost]
    // [Authorize]
    // [Route("upload")]
    // public async Task<IActionResult> Upload(IFormFile image, CancellationToken cancellationToken)
    // {
    //     try
    //     {
    //         if (image == null || image.Length <= 0) return BadRequest("No image uploaded.");
    //         var result = await _service.ProcessImage(image);          
    //         return new OkObjectResult(result);
    //     }
        
    //     catch (TaskCanceledException)
    //     {
    //         _logger.LogInformation("Image upload canceled.");
    //         return new StatusCodeResult(499); // Estado personalizado: tarea cancelada
    //     }
    //     catch (System.Exception ex)
    //     {
    //         _logger.LogError(ex.Message);
    //         return new StatusCodeResult(500);
    //     }
    // }

    // [HttpGet]
    // [Authorize]
    // [Route("get/{id}")]
    // public async Task<IActionResult> Get(string id)
    // {
    //     try
    //     {
    //         var result = await _service.GetImage(id);          
    //         return new OkObjectResult(result);
    //     }
    //     catch (System.Exception ex)
    //     {
    //         _logger.LogError(ex.Message);
    //         return new StatusCodeResult(500);
    //     }
    // }

    [HttpGet]
    [Authorize]
    [Route("getid/{firebaseId}")]
    public async Task<IActionResult> Get(string firebaseId)
    {
        try
        {
            var result = await _service.GetExternalId(firebaseId);          
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

}
