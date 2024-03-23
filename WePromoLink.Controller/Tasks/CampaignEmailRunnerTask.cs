using System.Net.Http.Json;
using Azure.Storage.Blobs;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Scriban;
using WePromoLink.Data;
using WePromoLink.DTO.CRM;
using WePromoLink.Enums.CRM;

namespace WePromoLink.Controller.Tasks;

public class CampaignEmailRunnerTask
{
    private const int WAIT_SECONDS = 2;
    private readonly IServiceScopeFactory _fac;
    private readonly ILogger<CampaignEmailRunnerTask> _logger;
    private HttpClient _client;
    private readonly IConfiguration _globalconfig;

    public CampaignEmailRunnerTask(IServiceScopeFactory fac, ILogger<CampaignEmailRunnerTask> logger, IConfiguration globalconfig)
    {
        _fac = fac;
        _logger = logger;
        _globalconfig = globalconfig;
    }

    public async Task Update()
    {
        _client = new HttpClient();
        using var scope = _fac.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<CRMDataContext>();

        var runners = _db.CampaignRunnerStates
        .Include(e => e.LeadModel)
        .Include(e => e.CampaignRunner)
        .ToList();

        foreach (var runner in runners)
        {
            switch (runner.Status)
            {
                case RunnerStatusEnum.Running:
                    await ManageRunning(runner, _db);
                    break;

                case RunnerStatusEnum.Paused:
                    await ManagePaused(runner, _db);
                    break;

                case RunnerStatusEnum.Stopped:
                    await ManageStopped(runner, _db);
                    break;

                default: break;
            }
            await Task.Delay(TimeSpan.FromSeconds(WAIT_SECONDS));
        }
        _db.SaveChanges();
        _client.Dispose();
    }

    private async Task ManageRunning(Models.CRM.CampaignRunnerState runner, CRMDataContext _db)
    {

        if (!runner.StepExecuted)
        {
            if (runner.NoExecuteBefore < DateTime.UtcNow)
            {
                await SendEmail(runner);
                runner.StepExecuted = true;
                runner.LastTimeExecute = DateTime.UtcNow;
                _db.CampaignRunnerStates.Update(runner);
                await _db.SaveChangesAsync();
            }
        }
    }

    private async Task ManagePaused(Models.CRM.CampaignRunnerState runner, CRMDataContext _db)
    {
    }

    private async Task ManageStopped(Models.CRM.CampaignRunnerState runner, CRMDataContext _db)
    {
        if (runner.LastTimeExecute == null || (DateTime.UtcNow - runner.LastTimeExecute).Value.TotalHours >= 1)
        {
            _db.CampaignRunnerStates.Remove(runner);
            await _db.SaveChangesAsync();
        }
    }

    private async Task SendEmail(Models.CRM.CampaignRunnerState runner)
    {
        var config = await _client.GetFromJsonAsync<CampaignRunnerConfig>(runner.CampaignRunner.ConfigurationJSONUrl);
        var currentStep = config.Steps.Where(e => e.Step == runner.Step).Single();

        var template = await _client.GetStringAsync(currentStep.TemplateUrl);
        var senderEmail = runner.CampaignRunner.SenderEmail;
        var subject = currentStep.Subject;

        var lead = runner.LeadModel;
        var t = Template.Parse(template);
        var model = new
        {
            Name = lead.Name,
            Runner = runner.CampaignRunner.ExternalId
        };
        var body = t.Render(model);

        await Send(lead.Name, lead.Email, subject, senderEmail, body);
    }

    private async Task Send(string recipentName, string recipentEmail, string senderEmail, string subject, string body)
    {
        try
        {
            SmtpClient _smtpClient = new SmtpClient();
            string _server = _globalconfig["Email:Server"];
            int _port = Convert.ToInt32(_globalconfig["Email:Port"]);
            string _password = _globalconfig["Email:Password"];

            await _smtpClient.ConnectAsync(_server, _port);
            await _smtpClient.AuthenticateAsync(senderEmail, _password);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("WePromoLink", senderEmail));
            message.To.Add(new MailboxAddress(recipentName, recipentEmail));
            message.Subject = subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = body;
            message.Body = builder.ToMessageBody();

            await _smtpClient.SendAsync(message);
            await _smtpClient.DisconnectAsync(true);

        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }


}