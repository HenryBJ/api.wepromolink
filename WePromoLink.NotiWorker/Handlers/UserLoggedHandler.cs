using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class UserLoggedHandler : IRequestHandler<UserLoggedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public UserLoggedHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(UserLoggedEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}
