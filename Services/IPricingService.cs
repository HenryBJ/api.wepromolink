using WePromoLink.DTO;

namespace WePromoLink.Services;

public interface IPricingService
{
    Task<PricingCard[]> GetAll();
}