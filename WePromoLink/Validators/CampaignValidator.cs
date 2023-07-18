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
        // RuleFor(x=>x.Budget).NotNull().NotEmpty();
        RuleFor(x=>x.EPM).InclusiveBetween(10m,1000m).WithMessage("CPM must be in range 10-1000");
        RuleFor(x=>x.Url).NotNull().NotEmpty();
    }
}