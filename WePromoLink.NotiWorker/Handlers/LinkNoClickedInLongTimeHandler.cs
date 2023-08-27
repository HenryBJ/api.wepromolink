using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class LinkNoClickedInLongTimeHandler : IRequestHandler<LinkNoClickedInLongTimeEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public LinkNoClickedInLongTimeHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(LinkNoClickedInLongTimeEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}