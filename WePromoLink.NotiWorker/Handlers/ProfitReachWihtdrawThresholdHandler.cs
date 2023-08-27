using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class ProfitReachWihtdrawThresholdHandler : IRequestHandler<ProfitReachWihtdrawThresholdEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public ProfitReachWihtdrawThresholdHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(ProfitReachWihtdrawThresholdEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}