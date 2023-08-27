using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class CampaignPublishedHandler : IRequestHandler<CampaignPublishedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public CampaignPublishedHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(CampaignPublishedEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}