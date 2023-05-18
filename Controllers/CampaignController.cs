using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WePromoLink.DTO;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class CampaignController : ControllerBase
{

    private readonly ICampaignService _campaignService;
    private readonly CampaignValidator _validator;

    public CampaignController(ICampaignService campaignService, CampaignValidator validator, IHttpContextAccessor httpContextAccessor)
    {
        _campaignService = campaignService;
        _validator = validator;
    }

    [HttpGet]
    [Authorize]
    [Route("all/{page=1}/{cant=50}/{filter=''}")]
    public async Task<IResult> GetAll(int? page, int? cant, string? filter)
    {
        try
        {
            var results = await _campaignService.GetAll(page, cant, filter);
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
    [Route("create")]
    public async Task<IResult> Create(Campaign campaign)
    {
        if (campaign == null) return Results.BadRequest();
        try
        {
            var validationResult = await _validator.ValidateAsync(campaign);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.ToDictionary());
            }

            var result = await _campaignService.CreateCampaign(campaign);
            return Results.Ok(result);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }
}
