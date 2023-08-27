using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class LinkClickedHandler : IRequestHandler<LinkClickedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public LinkClickedHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(LinkClickedEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}