using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class HitGeoLocalizedFailureHandler : IRequestHandler<HitGeoLocalizedFailureEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public HitGeoLocalizedFailureHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(HitGeoLocalizedFailureEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}