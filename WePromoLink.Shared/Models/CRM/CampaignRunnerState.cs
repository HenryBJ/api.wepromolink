namespace WePromoLink.Models.CRM;

public class CampaignRunnerState 
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; }
    public CampaignRunner CampaignRunner { get; set; }
    public Guid CampaignRunnerId { get; set; }
    public LeadModel? LeadModel { get; set; }
    public Guid? LeadModelId { get; set; }
    public string Status { get; set; } // Stoped, Pause, Running
    public int Step { get; set; }
    public bool StepExecuted { get; set; }
    public DateTime? LastTimeExecute { get; set; }
    public DateTime? NoExecuteBefore { get; set; }

    public CampaignRunnerState()
    {
        Id = Guid.NewGuid();
        ExternalId = Nanoid.Nanoid.Generate(size:12);
    }
}