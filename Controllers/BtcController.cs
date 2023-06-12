using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WePromoLink.DTO;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class BtcController : ControllerBase
{

    private readonly IPaymentService _service;
    private readonly ILogger<BtcController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BtcController(IPaymentService service, ILogger<BtcController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _service = service;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    [Authorize]
    [Route("invoice/{amount}")]
    public async Task<IActionResult> CreateInvoice(decimal amount)
    {
        try
        {
            var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
            var results = await _service.CreateInvoice(amount, firebaseId);
            return new OkObjectResult(results);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }
    
}
