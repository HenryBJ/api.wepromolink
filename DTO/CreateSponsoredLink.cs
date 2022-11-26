using System.ComponentModel.DataAnnotations;

namespace WePromoLink;

public class CreateSponsoredLink
{

    [StringLength(100)]
    [Required]
    public string? Title { get; set; }
    
    [StringLength(500)]
    [Required]
    public string? Url { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(500)]
    public string? ImageUrl { get; set; }

    [StringLength(500)]
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public decimal EPM { get; set; }
}