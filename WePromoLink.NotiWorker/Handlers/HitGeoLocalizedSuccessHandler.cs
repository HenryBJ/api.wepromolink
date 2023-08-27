using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class HitGeoLocalizedSuccessHandler : IRequestHandler<HitGeoLocalizedSuccessEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public HitGeoLocalizedSuccessHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(HitGeoLocalizedSuccessEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}