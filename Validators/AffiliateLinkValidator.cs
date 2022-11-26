using FluentValidation;

namespace WePromoLink.Validators;

public class AffiliateLinkValidator: AbstractValidator<CreateAffiliateLink>
{
    public AffiliateLinkValidator()
    {
        RuleFor(x=>x.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(x=>x.BTCAddress).NotNull().NotEmpty();
        RuleFor(x=>x.SponsoredLinkId).NotNull().NotEmpty();
    }
}