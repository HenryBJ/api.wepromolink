using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class DepositFailureHandler : IRequestHandler<DepositFailureEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public DepositFailureHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(DepositFailureEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}