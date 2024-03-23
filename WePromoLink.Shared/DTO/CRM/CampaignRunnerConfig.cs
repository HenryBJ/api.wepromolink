namespace WePromoLink.DTO.CRM;
public class CampaignRunnerConfig 
{
    public int Initial { get; set; }
    public ICollection<CampaignRunnerConfigStep> Steps { get; set; }

}

public class CampaignRunnerConfigStep 
{
    public int Step { get; set; }
    public string TemplateUrl { get; set; }
    public string Subject { get; set; }
    public ICollection<CampaignRunnerConfigTransition> Transitions { get; set; }

}

public class CampaignRunnerConfigTransition 
{
    public int Event { get; set; }
    public int Next_step { get; set; }
    public CampaignRunnerConfigDelay Delay { get; set; }
}

public class CampaignRunnerConfigDelay 
{
    public int Day { get; set; }
    public int Min { get; set; }
}