namespace WePromoLink.DTO.CRM;
public class ReadCampaignRunnerState 
{
    public string Name { get; set; }
    public string ExternalId { get; set; }
    public string LeadExternalId { get; set; }
    public string Status { get; set; }
    public string? LeadEmail { get; set; }
    public int Step { get; set; }
    public bool StepExecuted { get; set; }
    public DateTime? LastTimeExecute { get; set; }
    public DateTime? NoExecuteBefore { get; set; }

}