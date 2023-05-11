using Microsoft.AspNetCore.Mvc;
using WePromoLink.DTO;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{

    private readonly FundSponsoredLinkValidator _validator;
    private readonly ICampaignService _campaignService;

    public TransactionController(FundSponsoredLinkValidator validator, ICampaignService campaignService)
    {
        _validator = validator;
        _campaignService = campaignService;
    }

    [HttpPost]
    [Route("deposit/paymentlink")]
    public async Task<IResult> CreatePaymentLink(FundSponsoredLink funLinkId)
    {
        try
        {
            // if (funLinkId == null) return Results.BadRequest();
            // var validationResult = await _validator.ValidateAsync(funLinkId);
            // if (!validationResult.IsValid)
            // {
            //     return Results.ValidationProblem(validationResult.ToDictionary());
            // }
            // var result = await _campaignService.FundSponsoredLink(funLinkId);
            return Results.Ok();//return Results.Ok(result);

        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }
}