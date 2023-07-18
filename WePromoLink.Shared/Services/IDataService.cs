using Microsoft.AspNetCore.Mvc;
using WePromoLink.DTO;
using WePromoLink.Models;

namespace WePromoLink.Services;

public interface IDataService
{
    //Users Statistics
    Task<IActionResult> GetAvailable();
    Task<IActionResult> GetBudget();
    Task<IActionResult> GetLocked();
    Task<IActionResult> GetPayoutStats();
    Task<IActionResult> GetProfit();
    Task<IActionResult> GetEarnToday();
    Task<IActionResult> GetEarnLastWeek();
    Task<IActionResult> GetClickTodayOnLinks();
    Task<IActionResult> GetClickLastWeekOnLinks();
    Task<IActionResult> GetClickTodayOnCampaigns();
    Task<IActionResult> GetClickLastWeekOnCampaigns();
    Task<IActionResult> GetSharedToday();
    Task<IActionResult> GetSharedLastWeek();
    Task<IActionResult> GetHistoricalClickOnLinks();
    Task<IActionResult> GetHistoricalEarnOnLinks();
    Task<IActionResult> GetHistoricalClickOnCampaigns();
    Task<IActionResult> GetHistoricalClickOnShares();
    Task<IActionResult> GetHistoricalClickByCountriesOnLinks();
    Task<IActionResult> GetHistoricalEarnByCountries();
    Task<IActionResult> GetHistoricalClickByCountriesOnCampaigns();
    Task<IActionResult> GetHistoricalSharedByUsers();

    //Campaign Statistics
    Task<IActionResult> GetClicksLastWeekOnCampaign(string campaignId);
    Task<IActionResult> GetClicksTodayOnCampaign(string campaignId);
    Task<IActionResult> GetHistoryClicksByCountriesOnCampaign(string campaignId);
    Task<IActionResult> GetHistoryClicksOnCampaign(string campaignId);
    Task<IActionResult> GetHistorySharedByUsersOnCampaign(string campaignId);
    Task<IActionResult> GetHistorySharedOnCampaign(string campaignId);
    Task<IActionResult> GetSharedLastWeekOnCampaign(string campaignId);
    Task<IActionResult> GetSharedTodayOnCampaignModel(string campaignId);

    // Link Statistics
    Task<IActionResult> GetClicksLastWeekOnLink(string linkId);
    Task<IActionResult> GetClicksTodayOnLink(string linkId);
    Task<IActionResult> GetEarnLastWeekOnLink(string linkId);
    Task<IActionResult> GetEarnTodayOnLink(string linkId);
    Task<IActionResult> GetHistoryClicksByCountriesOnLink(string linkId);
    Task<IActionResult> GetHistoryEarnByCountriesOnLink(string linkId);
    Task<IActionResult> GetHistoryEarnOnLink(string linkId);
    Task<IActionResult> GetHistoryClicksOnLink(string linkId);
}