using FluentValidation;

namespace WePromoLink.Validators;

public class SponsoredLinkValidator: AbstractValidator<CreateSponsoredLink>
{
    public SponsoredLinkValidator()
    {
        RuleFor(x=>x.Email).NotEmpty().NotNull();
        RuleFor(x=>x.Title).NotNull().NotEmpty();
        RuleFor(x=>x.EPM).NotNull().NotEmpty();
        RuleFor(x=>x.Url).NotNull().NotEmpty();
    }
}