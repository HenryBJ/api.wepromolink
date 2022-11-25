using System.ComponentModel.DataAnnotations;

namespace WePromoLink;

public class CreateAffiliateLink
{
    [StringLength(200)]
    [Required]
    public string? SponsoredLinkId { get; set; }

    [StringLength(200)]
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [StringLength(200)]
    [Required]
    public string BTCAddress { get; set; }

    public CreateAffiliateLinkOptions? Options { get; set; }
}

public class CreateAffiliateLinkOptions
{
    public decimal Threshold { get; set; }
    public string? Group { get; set; }
}

