using WePromoLink.DTO;
using WePromoLink.Models;

namespace WePromoLink.Services;

public interface IDataService
{
    Task<IResult> GetAvailable();
    Task<IResult> GetBudget();
    Task<IResult> GetLocked();
    Task<IResult> GetPayoutStats();
    Task<IResult> GetProfit();
    Task<IResult> GetEarnToday();
    Task<IResult> GetEarnLastWeek();
    Task<IResult> GetClickTodayOnLinks();
    Task<IResult> GetClickLastWeekOnLinks();
    Task<IResult> GetClickTodayOnCampaigns();
    Task<IResult> GetClickLastWeekOnCampaigns();
    Task<IResult> GetSharedToday();
    Task<IResult> GetSharedLastWeek();
    Task<IResult> GetHistoricalClickOnLinks();
}