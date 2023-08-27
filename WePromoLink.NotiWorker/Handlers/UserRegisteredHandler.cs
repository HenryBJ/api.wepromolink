using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class UserRegisteredHandler : IRequestHandler<UserRegisteredEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public UserRegisteredHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(UserRegisteredEvent request, CancellationToken cancellationToken)
    {
        _senderEmail.Send(request.Name, request.Email, "Welcome to WePromoLink", Templates.Welcome(new { user = request.Name })).GetAwaiter().GetResult();
        return Task.FromResult(true);
    }
}
