using WePromoLink.Interfaces;

namespace WePromoLink.Models;

public class ClicksLastWeekOnCampaignModel: StatsBaseModel, IHasValue<int>
{
    public Guid Id { get; set; }
    public virtual CampaignModel Campaign { get; set; }
    public Guid CampaignModelId { get; set; }
    public int Value { get; set; }
}