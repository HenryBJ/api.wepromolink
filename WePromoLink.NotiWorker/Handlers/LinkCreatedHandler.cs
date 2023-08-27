using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class LinkCreatedHandler : IRequestHandler<LinkCreatedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    public LinkCreatedHandler(IEmailSender senderEmail)
    {
        _senderEmail = senderEmail;
    }
    public Task<bool> Handle(LinkCreatedEvent request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}