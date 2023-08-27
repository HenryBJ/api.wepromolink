using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class WithdrawFailureHandler : IRequestHandler<WithdrawFailureEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public WithdrawFailureHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(WithdrawFailureEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}