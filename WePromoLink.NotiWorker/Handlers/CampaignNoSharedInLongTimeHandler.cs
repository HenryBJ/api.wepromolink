using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class CampaignNoSharedInLongTimeHandler : IRequestHandler<CampaignNoSharedInLongTimeEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public CampaignNoSharedInLongTimeHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(CampaignNoSharedInLongTimeEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}