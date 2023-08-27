using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class CampaignClickedHandler : IRequestHandler<CampaignClickedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public CampaignClickedHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(CampaignClickedEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}