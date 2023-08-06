using System.ComponentModel.DataAnnotations;
using WePromoLink.Enums;

namespace WePromoLink.DTO;

public class AbuseReport
{
        
    [Required]
    public string CampaignExternalId { get; set; }

    [Required]
    public string Reason { get; set; }
}