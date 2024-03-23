using WePromoLink.DTO.CRM;

namespace WePromoLink.Services.CRM;

public interface ICampaignRunnerService
{
    Task Run(RunBundle data);
    Task AddCampaignRunner(CampaignRunner data);
    Task DeleteCampaignRunner(string ExternalId);
    Task Play(string CampaignRunnerStateExternalId);
    Task Pause(string CampaignRunnerStateExternalId);
    Task Stop(string CampaignRunnerStateExternalId);
    Task<PaginationList<ReadCampaignRunner>> GetAll(int? page, int? cant, string? filter);
    Task<ReadCampaignRunner> GetDetails(string id);
    Task<PaginationList<ReadCampaignRunnerState>> GetAllRunnerState(int? page, int? cant, string? filter);
    Task<ReadCampaignRunnerState> GetDetailsRunnerState(string id);
}