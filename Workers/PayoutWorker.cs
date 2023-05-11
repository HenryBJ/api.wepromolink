using BTCPayServer.Client;
using BTCPayServer.Client.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WePromoLink.Data;
using WePromoLink.Models;
using WePromoLink.Settings;

namespace WePromoLink.Workers;

public class PayoutWorker : BackgroundService
{
    const int WAIT_TIME_HOURS = 3;
    private readonly BTCPayServerClient _client;
    private readonly DataContext _db;
    private readonly IOptions<BTCPaySettings> _settings;
    private readonly ILogger<PayoutWorker> _logger;


    public PayoutWorker(IServiceScopeFactory fac, ILogger<PayoutWorker> logger)
    {
        using var scope = fac.CreateScope();
        _client = scope.ServiceProvider.GetRequiredService<BTCPayServerClient>();
        _db = scope.ServiceProvider.GetRequiredService<DataContext>();
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // while (true)
        // {
        //     await Task.Delay(TimeSpan.FromHours(WAIT_TIME_HOURS));
        //     var pullpayments = await _client.GetPullPayments(_settings.Value.StoreId);
        //     if (pullpayments == null || pullpayments.Length == 0) continue;

        //     var pullpaymentList = new List<PullPaymentData>(pullpayments);
        //     await UpdatePayoutSatus(pullpaymentList);
        //     await CreatePayouts(pullpaymentList);
        // }
    }

    private async Task CreatePayouts(List<PullPaymentData> pullpaymentList)
    {
        // var affiliateLinks = await _db.AffiliateLinks.Where(e => e.Available > e.Threshold).ToListAsync();
        // var pullpayment = pullpaymentList.Where(e => !e.Archived).FirstOrDefault();
        // if (pullpayment == null)
        // {
        //     _logger.LogWarning("NOT PULLPAYMENT DEFINED");
        //     return;
        // }
        // foreach (AffiliateLinkModel alink in affiliateLinks)
        // {
        //     using var transaction = await _db.Database.BeginTransactionAsync();
        //     CreatePayoutRequest pout = new CreatePayoutRequest
        //     {
        //         Amount = alink.Threshold,
        //         Destination = alink.BTCAddress,
        //         PaymentMethod = "BTC"
        //     };
        //     var response = await _client.CreatePayout(pullpayment.Id, pout);

        //     _db.PaymentTransactions.Add(new PaymentTransaction
        //     {
        //         Title = "WITHDRAW",
        //         Amount = alink.Threshold,
        //         Status = response.State.ToString().ToUpper(),
        //         CreatedAt = DateTime.UtcNow,
        //         IsDeposit = false,
        //         EmailModelId = alink.EmailModelId,
        //         SponsoredLinkId = alink.SponsoredLinkModelId,
        //         AffiliateLinkId = alink.Id,
        //         PayoutId = response.Id
        //     });

        //     alink.Available-=alink.Threshold;
        //     _db.AffiliateLinks.Update(alink);
                        
        //     await transaction.CommitAsync();
        //     _db.SaveChanges();
        // }
    }

    private async Task UpdatePayoutSatus(List<PullPaymentData> pullPaymentDatas)
    {
        // var payments = await _db.PaymentTransactions.Where(e => (e.Status != "COMPLETED") && (e.Status != "CANCELLED") && (e.Title == "WITHDRAW")).ToListAsync();
        // foreach (var pullpayment in pullPaymentDatas)
        // {
        //     var list = await _client.GetPayouts(pullpayment.Id, includeCancelled: true);
        //     foreach (var payout in list)
        //     {
        //         var payoutId = payout.Id;
        //         var paytran = payments.Where(e => e.PayoutId != null && e.PayoutId == payoutId).SingleOrDefault();

        //         if (paytran == null)
        //         {
        //             _logger.LogWarning($"Payout with ID: {payoutId} does not have a PaymentTransaction");
        //             continue;
        //         }

        //         var NewState = Enum.GetName<PayoutState>(payout.State);
        //         if (NewState == null) continue;

        //         paytran.Status = NewState.ToUpper();
        //         _db.PaymentTransactions.Update(paytran);
        //     }
        // }
        // await _db.SaveChangesAsync();
    }
}