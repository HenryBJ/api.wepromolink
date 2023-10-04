using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WePromoLink.DTO;
using WePromoLink.DTO.Marketing;
using WePromoLink.Services;
using WePromoLink.Services.Marketing;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class MarketingController : ControllerBase
{

    private readonly IMarketingService _service;
    private readonly ILogger<MarketingController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MarketingController(ILogger<MarketingController> logger, IHttpContextAccessor httpContextAccessor, IMarketingService service)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _service = service;
    }

    [HttpPost]
    [Route("join")]
    public async Task<IActionResult> JoinWaitingList([FromBody] JoinEmail data)
    {
        try
        {
            // var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
            await _service.JoinWaitingList(data.Email);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpPost]
    [Route("datapoint")]
    public async Task<IActionResult> AddDatapoint([FromBody] Datapoint data)
    {
        try
        {
            // var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
            await _service.AddSurveyEntry(data.Question, data.Answer);
            return new OkResult();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Route("waitinglist")]
    public async Task<IActionResult> GetWaitingList()
    {
        try
        {
            // var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
            var response = await _service.GetWaitingList();
            return new OkObjectResult(response);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Route("surveysummary")]
    public async Task<IActionResult> GetSurveySummary()
    {
        try
        {
            // var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
            var response = await _service.GetSurveySummary();
            return new OkObjectResult(response);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

}
