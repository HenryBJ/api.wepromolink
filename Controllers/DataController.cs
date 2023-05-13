using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WePromoLink.DTO;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase
{
    private readonly IDataService _service;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DataController(IHttpContextAccessor httpContextAccessor, IDataService service)
    {
        _httpContextAccessor = httpContextAccessor;
        _service = service;
    }


    [HttpGet]
    [Authorize]
    [Route("available")]
    public async Task<IResult> GetAvailable()
    {
        try
        {
            return await _service.GetAvailable();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

    [HttpGet]
    [Authorize]
    [Route("budget")]
    public async Task<IResult> GetBudget()
    {
        try
        {
            return await _service.GetBudget();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

    [HttpGet]
    [Authorize]
    [Route("locked")]
    public async Task<IResult> GetLocked()
    {
        try
        {
            return await _service.GetLocked();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

    [HttpGet]
    [Authorize]
    [Route("payout")]
    public async Task<IResult> GetPayout()
    {
        try
        {
            return await _service.GetPayoutStats();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

    [HttpGet]
    [Authorize]
    [Route("profit")]
    public async Task<IResult> GetProfit()
    {
        try
        {
            return await _service.GetProfit();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

    [HttpGet]
    [Authorize]
    [Route("earntoday")]
    public async Task<IResult> GetEarnToday()
    {
        try
        {
            return await _service.GetEarnToday();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }


    [HttpGet]
    [Authorize]
    [Route("earnlastweek")]
    public async Task<IResult> GetEarnLastWeek()
    {
        try
        {
            return await _service.GetEarnLastWeek();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

    [HttpGet]
    [Authorize]
    [Route("clickstodayonlinks")]
    public async Task<IResult> GetClickTodayOnLinks()
    {
        try
        {
            return await _service.GetClickTodayOnLinks();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

    [HttpGet]
    [Authorize]
    [Route("clickslastweekonlinks")]
    public async Task<IResult> GetClickLastWeekOnLinks()
    {
        try
        {
            return await _service.GetClickLastWeekOnLinks();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }

    [HttpGet]
    [Authorize]
    [Route("clickstodayoncampaigns")]
    public async Task<IResult> GetClickTodayOnCampaigns()
    {
        try
        {
            return await _service.GetClickTodayOnCampaigns();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }


    [HttpGet]
    [Authorize]
    [Route("clickslastweekoncampaigns")]
    public async Task<IResult> GetClickLastWeekOnCampaigns()
    {
        try
        {
            return await _service.GetClickLastWeekOnCampaigns();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }
// From here below

    [HttpGet]
    [Authorize]
    [Route("sharedtoday")]
    public async Task<IResult> GetSharedToday()
    {
        try
        {
            return await _service.GetSharedToday();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }


    [HttpGet]
    [Authorize]
    [Route("sharedlastweek")]
    public async Task<IResult> GetSharedLastWeek()
    {
        try
        {
            return await _service.GetSharedLastWeek();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }


    [HttpGet]
    [Authorize]
    [Route("historicalclicksonlink")]
    public async Task<IResult> GetHistoricalClickOnLinks()
    {
        try
        {
            return await _service.GetHistoricalClickOnLinks();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Results.Problem();
        }
    }


}
