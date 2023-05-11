using System.ComponentModel.DataAnnotations;

namespace WePromoLink.DTO;

public class SignUpData
{

    [Required]
    [StringLength(500)]
    public string Fullname { get; set; }
    
    [Required]
    [StringLength(500)]
    [EmailAddress]
    public string Email { get; set; }

    [StringLength(500)]
    public string? FirebaseId { get; set; }

    [StringLength(500)]
    public string? SubscriptionPlanId { get; set; }
    
}