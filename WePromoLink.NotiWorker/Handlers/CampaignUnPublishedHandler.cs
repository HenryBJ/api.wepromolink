using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class CampaignUnPublishedHandler : IRequestHandler<CampaignUnPublishedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public CampaignUnPublishedHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(CampaignUnPublishedEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}