using WePromoLink.DTO;
using WePromoLink.Models;

namespace WePromoLink.Services;

public interface IDataService
{
    Task<AvailableModel> GetAvailable();
}