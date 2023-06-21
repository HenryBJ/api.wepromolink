using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WePromoLink.DTO;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{

    private readonly INotificationService _service;
    private readonly ILogger<NotificationController> _logger;

    public NotificationController(INotificationService service, IHttpContextAccessor httpContextAccessor, ILogger<NotificationController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    [Route("get/{page=1}/{cant=15}")]
    public async Task<IActionResult> Get(int? page, int? cant)
    {
        try
        {
            var results = await _service.Get(page, cant);
            return new OkObjectResult(results);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("detail/{id}")]
    public async Task<IActionResult> GetDetails(string id)
    {
        try
        {
            var results = await _service.GetDetails(id);
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
    [Route("read/{id}")]
    public async Task<IActionResult> MarkAsRead(string id)
    {
        try
        {
            await _service.MarkAsRead(id);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpDelete]
    [Authorize]
    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await _service.Delete(id);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

}
