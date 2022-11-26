using FluentValidation;
using WePromoLink.DTO;

namespace WePromoLink.Validators;

public class FundSponsoredLinkValidator:AbstractValidator<FundSponsoredLink>
{
    public FundSponsoredLinkValidator()
    {
        RuleFor(x=>x.Email).NotEmpty().NotNull().EmailAddress();
        // RuleFor(x=>x.Amount).NotNull().NotEmpty();
        // RuleFor(x=>x.Amount).GreaterThan(0.0012m);
        RuleFor(x=>x.SponsoredLinkId).NotNull().NotEmpty();
    }
}