using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class WithdrawCompletedHandler : IRequestHandler<WithdrawCompletedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public WithdrawCompletedHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(WithdrawCompletedEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}