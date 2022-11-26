using FluentValidation;
using WePromoLink.DTO;

namespace WePromoLink.Validators;

public class SponsoredLinkValidator: AbstractValidator<CreateSponsoredLink>
{
    public SponsoredLinkValidator()
    {
        RuleFor(x=>x.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(x=>x.Title).NotNull().NotEmpty();
        RuleFor(x=>x.EPM).NotNull().NotEmpty();
        RuleFor(x=>x.EPM).InclusiveBetween(0.00009m,2m);
        RuleFor(x=>x.Url).NotNull().NotEmpty();
    }
}