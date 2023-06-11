using MailKit.Net.Smtp;
using MimeKit;

namespace WePromoLink.Services.Email;

public class EmailSender : IEmailSender
{
    private readonly SmtpClient _client;
    private readonly string _server;
    private readonly int _port;
    private readonly string _sender;
    private readonly string _password;


    public EmailSender(string server, int port, string sender, string password)
    {
        _client = new SmtpClient();
        _server = server;
        _port = port;
        _sender = sender;
        _password = password;
    }

    public async Task Send(string recipentName, string recipentEmail, string subject, string body)
    {
        try
        {
            await _client.ConnectAsync(_server, _port);
            await _client.AuthenticateAsync(_sender, _password);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("WePromoLink", _sender));
            message.To.Add(new MailboxAddress(recipentName, recipentEmail));
            message.Subject = subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = body;
            message.Body = builder.ToMessageBody();

            await _client.SendAsync(message);
            await _client.DisconnectAsync(true);

        }
        catch (System.Exception)
        {
            throw;
        }
    }
}