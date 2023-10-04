
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WePromoLink.Services;

namespace WePromoLink.Backoffice.Controllers;

[Route("[controller]")]
[ApiController]
public class WithdrawController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly StripeService _stripeService;
    private readonly ILogger<WithdrawController> _logger;

    public WithdrawController(IConfiguration configuration, StripeService stripeService, ILogger<WithdrawController> logger)
    {
        _configuration = configuration;
        _stripeService = stripeService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet("getAll/{page}/{cant}")]
    public async Task<IActionResult> GetAll(int page = 1, int cant = 50)
    {
        try
        {
            var result = await _stripeService.GetAllWitdrawRequests(page, cant);
            return new OkObjectResult(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }

    }

}
