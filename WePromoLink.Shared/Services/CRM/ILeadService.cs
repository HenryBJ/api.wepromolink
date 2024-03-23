using WePromoLink.DTO.CRM;

namespace WePromoLink.Services.CRM;

public interface ILeadService
{
    Task AddLead(AddLead data);
    Task EditLead(EditLead data);
    Task DeleteLead(string externalId);
    Task NextEvent(NextEvent data);
    Task<PaginationList<ReadLead>> GetAll(int? page, int? cant, string? filter);
    Task<ReadLead> GetDetails(string id);
}