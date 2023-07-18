using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WePromoLink.DTO;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class PricingController : ControllerBase
{

    private readonly IPricingService _service;
    private readonly ILogger<PricingController> _logger;

    public PricingController(IPricingService service, ILogger<PricingController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    [Route("all")]
    [ResponseCache(Duration = 1800)] // duración en segundos (30 minutos)
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var results = await _service.GetAll();
            return new OkObjectResult(results);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }
    
}
