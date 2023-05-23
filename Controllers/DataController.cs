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
    private readonly ILogger<DataController> _logger;

    public DataController(IHttpContextAccessor httpContextAccessor, IDataService service, ILogger<DataController> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _service = service;
        _logger = logger;
    }


    [HttpGet]
    [Authorize]
    [Route("available")]
    public async Task<IActionResult> GetAvailable()
    {
        try
        {
            return await _service.GetAvailable();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("budget")]
    public async Task<IActionResult> GetBudget()
    {
        try
        {
            return await _service.GetBudget();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("locked")]
    public async Task<IActionResult> GetLocked()
    {
        try
        {
            return await _service.GetLocked();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("payout")]
    public async Task<IActionResult> GetPayout()
    {
        try
        {
            return await _service.GetPayoutStats();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("profit")]
    public async Task<IActionResult> GetProfit()
    {
        try
        {
            return await _service.GetProfit();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("earntoday")]
    public async Task<IActionResult> GetEarnToday()
    {
        try
        {
            return await _service.GetEarnToday();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }


    [HttpGet]
    [Authorize]
    [Route("earnlastweek")]
    public async Task<IActionResult> GetEarnLastWeek()
    {
        try
        {
            return await _service.GetEarnLastWeek();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("clickstodayonlinks")]
    public async Task<IActionResult> GetClickTodayOnLinks()
    {
        try
        {
            return await _service.GetClickTodayOnLinks();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("clickslastweekonlinks")]
    public async Task<IActionResult> GetClickLastWeekOnLinks()
    {
        try
        {
            return await _service.GetClickLastWeekOnLinks();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("clickstodayoncampaigns")]
    public async Task<IActionResult> GetClickTodayOnCampaigns()
    {
        try
        {
            return await _service.GetClickTodayOnCampaigns();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }


    [HttpGet]
    [Authorize]
    [Route("clickslastweekoncampaigns")]
    public async Task<IActionResult> GetClickLastWeekOnCampaigns()
    {
        try
        {
            return await _service.GetClickLastWeekOnCampaigns();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }
    // From here below

    [HttpGet]
    [Authorize]
    [Route("sharedtoday")]
    public async Task<IActionResult> GetSharedToday()
    {
        try
        {
            return await _service.GetSharedToday();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }


    [HttpGet]
    [Authorize]
    [Route("sharedlastweek")]
    public async Task<IActionResult> GetSharedLastWeek()
    {
        try
        {
            return await _service.GetSharedLastWeek();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }


    [HttpGet]
    [Authorize]
    [Route("historicalclicksonlink")]
    public async Task<IActionResult> GetHistoricalClickOnLinks()
    {
        try
        {
            return await _service.GetHistoricalClickOnLinks();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("historicalearnonlink")]
    public async Task<IActionResult> GetHistoricalEarnOnLinks()
    {
        try
        {
            return await _service.GetHistoricalEarnOnLinks();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("historicalclickoncampaigns")]
    public async Task<IActionResult> GetHistoricalClickOnCampaigns()
    {
        try
        {
            return await _service.GetHistoricalClickOnCampaigns();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("historicalclickonshares")]
    public async Task<IActionResult> GetHistoricalClickOnShares()
    {
        try
        {
            return await _service.GetHistoricalClickOnShares();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("historicalclickbycountriesonlinks")]
    public async Task<IActionResult> GetHistoricalClickByCountriesOnLinks()
    {
        try
        {
            return await _service.GetHistoricalClickByCountriesOnLinks();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("historicalearnbycountries")]
    public async Task<IActionResult> GetHistoricalEarnByCountries()
    {
        try
        {
            return await _service.GetHistoricalEarnByCountries();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }


    [HttpGet]
    [Authorize]
    [Route("historicalclickbycountriesoncampaigns")]
    public async Task<IActionResult> GetHistoricalClickByCountriesOnCampaigns()
    {
        try
        {
            return await _service.GetHistoricalClickByCountriesOnCampaigns();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("historicalsharedbyusers")]
    public async Task<IActionResult> GetHistoricalSharedByUser()
    {
        try
        {
            return await _service.GetHistoricalSharedByUsers();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    // Campaigns

    [HttpGet]
    [Authorize]
    [Route("clickslastweekoncampaign/{id}")]
    public async Task<IActionResult> GetClicksLastWeekOnCampaign(string id)
    {
        try
        {
            return await _service.GetClicksLastWeekOnCampaign(id);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("clickstodayoncampaign/{id}")]
    public async Task<IActionResult> GetClicksTodayOnCampaign(string id)
    {
        try
        {
            return await _service.GetClicksTodayOnCampaign(id);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("historyclicksbycountriesoncampaign/{id}")]
    public async Task<IActionResult> GetHistoryClicksByCountriesOnCampaign(string id)
    {
        try
        {
            return await _service.GetHistoryClicksByCountriesOnCampaign(id);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("historyclicksoncampaign/{id}")]
    public async Task<IActionResult> GetHistoryClicksOnCampaign(string id)
    {
        try
        {
            return await _service.GetHistoryClicksOnCampaign(id);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("historysharedbyusersoncampaign/{id}")]
    public async Task<IActionResult> GetHistorySharedByUsersOnCampaign(string id)
    {
        try
        {
            return await _service.GetHistorySharedByUsersOnCampaign(id);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("historysharedoncampaign/{id}")]
    public async Task<IActionResult> GetHistorySharedOnCampaign(string id)
    {
        try
        {
            return await _service.GetHistorySharedOnCampaign(id);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("sharedlastweekoncampaign/{id}")]
    public async Task<IActionResult> GetSharedLastWeekOnCampaign(string id)
    {
        try
        {
            return await _service.GetSharedLastWeekOnCampaign(id);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("sharedtodayoncampaignmodel/{id}")]
    public async Task<IActionResult> GetSharedTodayOnCampaignModel(string id)
    {
        try
        {
            return await _service.GetSharedTodayOnCampaignModel(id);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return new StatusCodeResult(500);
        }
    }


}
