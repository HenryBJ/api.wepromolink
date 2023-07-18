namespace WePromoLink.Services.Email;

public interface IEmailSender
{
    Task Send(string recipentName, string recipentEmail, string subject, string body);
}