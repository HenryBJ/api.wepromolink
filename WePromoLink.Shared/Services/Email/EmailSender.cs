using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace WePromoLink.Services.Email;

public class EmailSender : IEmailSender
{
    private readonly ILogger<IEmailSender> _logger;
    private readonly SmtpClient _client;
    private readonly string _server;
    private readonly int _port;
    private readonly string _sender;
    private readonly string _password;
    private readonly IConfiguration _config;


    public EmailSender(IConfiguration config, ILogger<IEmailSender> logger)
    {
        _config = config;
        _client = new SmtpClient();
        _server = _config["Email:Server"];
        _port = Convert.ToInt32(_config["Email:Port"]);
        _sender = _config["Email:Sender"];
        _password = _config["Email:Password"];
        _logger = logger;
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
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}