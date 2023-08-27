using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class HitClickedHandler : IRequestHandler<HitClickedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public HitClickedHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(HitClickedEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}