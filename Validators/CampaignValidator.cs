using FluentValidation;
using WePromoLink.DTO;

namespace WePromoLink.Validators;

public class CampaignValidator: AbstractValidator<Campaign>
{
    public CampaignValidator()
    {
        RuleFor(x=>x.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(x=>x.Title).NotNull().NotEmpty();
        RuleFor(x=>x.EPM).NotNull().NotEmpty();
        RuleFor(x=>x.Budget).NotNull().NotEmpty();
        RuleFor(x=>x.EPM).InclusiveBetween(0.00009m,2m);
        RuleFor(x=>x.Url).NotNull().NotEmpty();
    }
}