using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WePromoLink.DTO;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class CampaignController : ControllerBase
{

    private readonly ICampaignService _campaignService;
    private readonly CampaignValidator _validator;
    private readonly ILogger<CampaignController> _logger;

    public CampaignController(ICampaignService campaignService, CampaignValidator validator, IHttpContextAccessor httpContextAccessor, ILogger<CampaignController> logger)
    {
        _campaignService = campaignService;
        _validator = validator;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    [Route("all/{page=1}/{cant=15}/{filter?}")]
    public async Task<IActionResult> GetAll(int? page, int? cant, string? filter)
    {
        try
        {
            var results = await _campaignService.GetAll(page, cant, filter);
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
    [Route("mobile/all/{page=1}/{cant=15}/{filter?}")]
    public async Task<IActionResult> GetAllWithImages(int? page, int? cant, string? filter)
    {
        try
        {
            var results = await _campaignService.GetAllWithImages(page, cant, filter);
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
            var results = await _campaignService.GetDetails(id);
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
    [Route("explore/{offset}/{limit}/{timestamp}")]
    public async Task<IActionResult> Explore(int offset, int limit, long timestamp)
    {
        try
        {
            return await _campaignService.Explore(offset, limit, timestamp);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPost]
    [Authorize]
    [Route("create")]
    public async Task<IActionResult> Create(Campaign campaign)
    {
        if (campaign == null) return BadRequest();
        try
        {
            var validationResult = await _validator.ValidateAsync(campaign);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }

            var result = await _campaignService.CreateCampaign(campaign);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize]
    [Route("edit/{id}")]
    public async Task<IActionResult> Edit(string id, [FromBody] Campaign campaign)
    {
        if (campaign == null || string.IsNullOrEmpty(id)) return BadRequest();
        try
        {
            var validationResult = await _validator.ValidateAsync(campaign);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }

            await _campaignService.Edit(id, campaign);
            return Ok();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Authorize]
    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            await _campaignService.Delete(id);
            return Ok();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize]
    [Route("publish/{id}/{status}")]
    public async Task<IActionResult> Publish(string id, bool status)
    {
        try
        {
            await _campaignService.Publish(id, status);
            return Ok();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("report-abuse")]
    public async Task<IActionResult> ReportAbuse([FromBody] AbuseReport abuseReport)
    {
        try
        {
            await _campaignService.ReportAbuse(abuseReport);
            return Ok();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }


}
