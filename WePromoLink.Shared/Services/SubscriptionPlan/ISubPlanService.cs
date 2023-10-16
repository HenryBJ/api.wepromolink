using Microsoft.AspNetCore.Mvc;
using WePromoLink.DTO;
using WePromoLink.DTO.SubscriptionPlan;

namespace WePromoLink.Services.SubscriptionPlan;

public interface ISubPlanService
{
    Task<Guid> Create(SubscriptionPlanCreate subPlan);
    Task Delete(SubscriptionPlanDelete subPlan);
    Task Edit(SubscriptionPlanEdit subPlan);
    Task<PaginationList<SubscriptionPlanRead>> GetAll(int? page, int? cant);
    Task<SubscriptionPlanRead> Get(Guid Id);
    Task<Guid> Create(SubscriptionPlanFeatureCreate feature);
    Task Delete(SubscriptionPlanFeatureDelete feature);
    Task Edit(SubscriptionPlanFeatureEdit feature);

}