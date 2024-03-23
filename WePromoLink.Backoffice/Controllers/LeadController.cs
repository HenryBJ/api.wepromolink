
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
public class LeadController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<LeadController> _logger;
    private readonly ILeadService _service;

    public LeadController(IConfiguration configuration, ILogger<LeadController> logger, ILeadService service)
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

    [HttpPost("add")]
    public async Task<IActionResult> Add(AddLead data)
    {
        try
        {
            await _service.AddLead(data);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    // [HttpPost("next")]
    // public async Task<IActionResult> NextEvent(NextEvent data)
    // {
    //     try
    //     {
    //         await _service.NextEvent(data);
    //         return new OkResult();
    //     }
    //     catch (System.Exception ex)
    //     {
    //         _logger.LogError(ex.Message);
    //         return new StatusCodeResult(500);
    //     }
    // }

    [Authorize]
    [HttpPut("edit")]
    public async Task<IActionResult> Edit(EditLead data)
    {
        try
        {
            await _service.EditLead(data);
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
            await _service.DeleteLead(id);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }
    
}
