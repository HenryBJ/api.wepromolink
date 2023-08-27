using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class UserBlockedHandler : IRequestHandler<UserBlockedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public UserBlockedHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(UserBlockedEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}