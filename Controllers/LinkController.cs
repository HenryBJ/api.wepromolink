using Microsoft.AspNetCore.Mvc;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class LinkController : ControllerBase
{
    private readonly IAffiliateLinkService _affiliateLinkService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AffiliateLinkValidator _validator;
    public LinkController(IAffiliateLinkService affiliateLinkService,AffiliateLinkValidator validator, IHttpContextAccessor httpContextAccessor)
    {
        _affiliateLinkService = affiliateLinkService;
        _validator = validator;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    [Route("all/{page=1}")]
    public async Task<IResult> GetAll(int? page)
    {
        try
        {
            // List afiliate links
            var results = await _affiliateLinkService.ListAffiliateLinks(page);
            return Results.Ok(results);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

    [HttpPost]
    [Route("create")]
    public async Task<IResult> CreateAffLink(CreateAffiliateLink link)
    {
        // if (link == null) return Results.BadRequest();

        // var validationResult = await _validator.ValidateAsync(link);
        // if (!validationResult.IsValid)
        // {
        //     return Results.ValidationProblem(validationResult.ToDictionary());
        // }
        // object result;
        // try
        // {
        //     result = await _affiliateLinkService.CreateAffiliateLink(link, _httpContextAccessor.HttpContext!);
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine(ex.Message);
        //     return Results.Problem(ex.Message);
        // }

        return Results.Ok();//return Results.Ok(result);
    }
}