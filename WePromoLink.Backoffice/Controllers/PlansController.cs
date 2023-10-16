
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WePromoLink.DTO.SubscriptionPlan;
using WePromoLink.Services;
using WePromoLink.Services.SubscriptionPlan;

namespace WePromoLink.Backoffice.Controllers;

[Route("[controller]")]
[ApiController]
public class PlansController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ISubPlanService _service;
    private readonly ILogger<PlansController> _logger;

    public PlansController(IConfiguration configuration, ISubPlanService service, ILogger<PlansController> logger)
    {
        _configuration = configuration;
        _service = service;
        _logger = logger;
    }

    [Authorize]
    [HttpGet("getAll/{page}/{cant}")]
    public async Task<IActionResult> GetAll(int page = 1, int cant = 50)
    {
        try
        {
            var result = await _service.GetAll(page, cant);
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
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var result = await _service.Get(id);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create(SubscriptionPlanCreate subPlan)
    {
        try
        {
            var result = await _service.Create(subPlan);
            return new CreatedResult($"/plans/{result}", result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPut("edit")]
    public async Task<IActionResult> Edit(SubscriptionPlanEdit subPlan)
    {
        try
        {
            await _service.Edit(subPlan);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(SubscriptionPlanDelete subPlan)
    {
        try
        {
            await _service.Delete(subPlan);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPost("feature/create")]
    public async Task<IActionResult> CreateFeature(SubscriptionPlanFeatureCreate feature)
    {
        try
        {
            var result = await _service.Create(feature);
            return new CreatedResult($"/plans/feature/{result}", result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpPut("feature/edit")]
    public async Task<IActionResult> EditFeature(SubscriptionPlanFeatureEdit feature)
    {
        try
        {
            await _service.Edit(feature);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [Authorize]
    [HttpDelete("feature/delete")]
    public async Task<IActionResult> DeleteFeature(SubscriptionPlanFeatureDelete feature)
    {
        try
        {
            await _service.Delete(feature);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

}
