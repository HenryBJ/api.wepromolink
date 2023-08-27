using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class CampaignAbuseReportedHandler : IRequestHandler<CampaignAbuseReportedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public CampaignAbuseReportedHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(CampaignAbuseReportedEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}