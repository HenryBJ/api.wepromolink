using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WePromoLink.DTO;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class StripeController : ControllerBase
{

    private readonly ILogger<StripeController> _logger;
    private readonly StripeService _service;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public StripeController(ILogger<StripeController> logger, IHttpContextAccessor httpContextAccessor, StripeService service)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _service = service;
    }

    [HttpPost]
    [Authorize]
    [Route("account/create")]
    public async Task<IActionResult> CreateAccountLink()
    {
        try
        {
            var url = await _service.CreateOrUpdateAccountLink(false);
            return new OkObjectResult(url);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPut]
    [Authorize]
    [Route("account/update")]
    public async Task<IActionResult> UpdateAccountLink()
    {
        try
        {
            var url = await _service.CreateOrUpdateAccountLink(true);
            return new OkObjectResult(url);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }
    
}
