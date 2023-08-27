using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class HitCreatedHandler : IRequestHandler<HitCreatedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public HitCreatedHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(HitCreatedEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}