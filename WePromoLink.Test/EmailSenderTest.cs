using Microsoft.Extensions.DependencyInjection;
using WePromoLink.Services.Email;

namespace WePromoLink.Test;

public class EmailSenderTest:BaseTest
{

    private readonly IEmailSender? _emailSender;

    public EmailSenderTest()
    {
        _emailSender = _serviceProvider?.GetRequiredService<IEmailSender>();
    }

    [Fact]
    public void WelcomeEmailSend_ShouldReturnTrue()
    {
        if (_db == null) throw new Exception("Data context null");
        if (_emailSender == null) throw new Exception("EmailSender null");

        string email = "jose.devops@gmail.com";
        _emailSender.Send("Enrique", email, "Welcome to WePromoLink", Templates.Welcome(new { user = "Enrique", year = DateTime.Now.Year.ToString() })).GetAwaiter().GetResult();
        Assert.True(true);
    }
}