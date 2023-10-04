using WePromoLink.DTO.Marketing;

namespace WePromoLink.Services.Marketing;

public interface IMarketingService
{
    Task JoinWaitingList(string email);
    Task AddSurveyEntry(Guid question, Guid response);
    Task<string[]> GetWaitingList();
    Task<SurveySummary> GetSurveySummary(); 
}