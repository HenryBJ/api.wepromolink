using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class LinkController : ControllerBase
{
    private readonly ILinkService _linkService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<LinkController> _logger;
    public LinkController(ILinkService affiliateLinkService, IHttpContextAccessor httpContextAccessor, ILogger<LinkController> logger)
    {
        _linkService = affiliateLinkService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    [HttpGet]
    [Route("all/{page=1}")]
    public async Task<IResult> GetAll(int? page)
    {
        try
        {
            // List afiliate links
            var results = await _linkService.ListAffiliateLinks(page);
            return Results.Ok(results);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

    [HttpPost]
    [Authorize]
    [Route("create/{id}")]
    public async Task<IActionResult> Create(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();
        try
        {
            var result = await _linkService.Create(id);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}