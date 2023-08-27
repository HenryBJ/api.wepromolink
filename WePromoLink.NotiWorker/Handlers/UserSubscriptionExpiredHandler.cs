using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class UserSubscriptionExpiredHandler : IRequestHandler<UserSubscriptionExpiredEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public UserSubscriptionExpiredHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(UserSubscriptionExpiredEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}