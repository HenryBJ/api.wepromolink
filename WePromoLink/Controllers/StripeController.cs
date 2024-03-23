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
    [Route("account/checkout")]
    public async Task<IActionResult> Checkout(CheckoutData data)
    {
        try
        {
            var url = await _service.Checkout(data);
            return new OkObjectResult(url);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPost]
    [Authorize]
    [Route("account/upgrade")]
    public async Task<IActionResult> Upgrade(UpgradeData data)
    {
        try
        {
            var url = await _service.Upgrade(data);
            return new OkObjectResult(url);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPost]
    [Authorize]
    [Route("account/create")]
    public async Task<IActionResult> CreateAccountLink()
    {
        try
        {
            var url = await _service.CreateAccountLink();
            return new OkObjectResult(url);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPost]
    [Authorize]
    [Route("invoice/{amount}")]
    public async Task<IActionResult> CreateInvoice(decimal amount)
    {
        try
        {
            var results = await _service.CreateInvoice((int)amount);
            return new OkObjectResult(results);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPost]
    [Authorize]
    [Route("withdraw/{amount}")]
    public async Task<IActionResult> CreateWithdrawRequest(decimal amount)
    {
        try
        {
            await _service.CreateWithdrawRequest((int)amount);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("account/hasExpressStripe")]
    public async Task<IActionResult> HasExpressStripe()
    {
        try
        {
            var response = await _service.HasExpressStripe();
            return new OkObjectResult(response);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPost]
    [Authorize]
    [Route("account/login")]
    public async Task<IActionResult> GetStripeDashboardLink()
    {
        try
        {
            var response = await _service.GetStripeDashboardLink();
            return new OkObjectResult(response);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

}
