using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WePromoLink.Enums;

namespace WePromoLink.Models;

public class AbuseReportModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? CampaignId { get; set; }
    public CampaignModel? Campaign { get; set; }
    public UserModel User { get; set; }
    public string Reason { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}