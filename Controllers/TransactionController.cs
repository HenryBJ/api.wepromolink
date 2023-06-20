using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WePromoLink.DTO;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{

    private readonly ITransactionService _service;
    private readonly ILogger<TransactionController> _logger;

    public TransactionController(ITransactionService service, IHttpContextAccessor httpContextAccessor, ILogger<TransactionController> logger)
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

}
