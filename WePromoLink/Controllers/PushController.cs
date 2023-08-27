using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WePromoLink.DTO;
using WePromoLink.DTO.PushNotification;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class PushController : ControllerBase
{

    private readonly IPushService _service;
    private readonly ILogger<PushController> _logger;

    public PushController(IPushService service, ILogger<PushController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    [Route("get")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var results = await _service.GetPushNotification();
            return new OkObjectResult(results);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPut]
    [Authorize]
    [Route("put")]
    public async Task<IActionResult> Put(PushNotification push)
    {
        try
        {
            await _service.UpdatePushNotification(push);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }


}
