using Microsoft.AspNetCore.Mvc;
using WePromoLink.Services;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IStatsLinkService _statsLinkService;

    public DashboardController(IStatsLinkService statsLinkService)
    {
        _statsLinkService = statsLinkService;
    }

    [HttpGet]
    [Route("link/{id}")]
    public async Task<IResult> LinkDetail(string id)
    {
        if (String.IsNullOrEmpty(id)) return Results.BadRequest();

        try
        {
            // Get Stats for affiliate link
            var result = await _statsLinkService.AffiliateLinkStats(id);
            return Results.Ok(result);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }

    }

    [HttpGet]
    [Route("campaign/{id}")]
    public async Task<IResult> CampaingDetail(string id)
    {
        if (String.IsNullOrEmpty(id)) return Results.BadRequest();
        try
        {            
            var result = await _statsLinkService.SponsoredLinkStats(id);
            return Results.Ok(result);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }

    }
}