
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WePromoLink.DTO.CRM;
using WePromoLink.DTO.StaticPage;
using WePromoLink.DTO.SubscriptionPlan;
using WePromoLink.Services;
using WePromoLink.Services.CRM;
using WePromoLink.Services.StaticPages;
using WePromoLink.Services.SubscriptionPlan;

namespace WePromoLink.Backoffice.Controllers;

[Route("[controller]")]
[ApiController]
public class RunnerController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<RunnerController> _logger;
    private readonly ICampaignRunnerService _service;

    public RunnerController(IConfiguration configuration, ILogger<RunnerController> logger, ICampaignRunnerService service)
    {
        _configuration = configuration;
        _service = service;
    }

    [Authorize]
    [HttpGet("getAll/{page=1}/{cant=50}/{filter?}")]
    public async Task<IActionResult> GetAll(int? page, int? cant, string? filter = "")
    {
        try
        {
            var result = await _service.GetAll(page!.Value, cant!.Value, filter!);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        try
        {
            var result = await _service.GetDetails(id);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpGet("getAllRunner/{page=1}/{cant=50}/{filter?}")]
    public async Task<IActionResult> GetAllRunner(int? page, int? cant, string? filter = "")
    {
        try
        {
            var result = await _service.GetAllRunnerState(page!.Value, cant!.Value, filter!);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpGet("getRunner/{id}")]
    public async Task<IActionResult> GetRunner(string id)
    {
        try
        {
            var result = await _service.GetDetailsRunnerState(id);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPost("run")]
    public async Task<IActionResult> Run(RunBundle data)
    {
        try
        {
            await _service.Run(data);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(CampaignRunner data)
    {
        try
        {
            await _service.AddCampaignRunner(data);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPost("play/{id}")]
    public async Task<IActionResult> Play(string id)
    {
        try
        {
            await _service.Play(id);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPost("pause/{id}")]
    public async Task<IActionResult> Pause(string id)
    {
        try
        {
            await _service.Pause(id);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPost("stop/{id}")]
    public async Task<IActionResult> Stop(string id)
    {
        try
        {
            await _service.Stop(id);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }


    [Authorize]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await _service.DeleteCampaignRunner(id);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }
    
}
