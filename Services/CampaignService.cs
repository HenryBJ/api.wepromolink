using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Models;
using WePromoLink.Settings;
using WePromoLink;
using WePromoLink.Enums;

namespace WePromoLink.Services;

public class CampaignService : ICampaignService
{
    private readonly IPaymentService _client;
    private readonly IOptions<BTCPaySettings> _options;
    private readonly DataContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<CampaignService> _logger;
    public CampaignService(DataContext db, IOptions<BTCPaySettings> options, IPaymentService client, IHttpContextAccessor httpContextAccessor, ILogger<CampaignService> logger)
    {
        _db = db;
        _options = options;
        _client = client;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<string> CreateCampaign(Campaign campaign)
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.Available).SingleOrDefaultAsync();

        if (user == null) throw new Exception("User does not exits");
        if (user.Available.Value < campaign.Budget) throw new Exception("Insufficient balance");
        if(!user.IsSubscribed) throw new Exception("Subscription is not active");
        if(user.IsBlocked) throw new Exception("User is blocked");

        var externalId = await Nanoid.Nanoid.GenerateAsync(size: 12);
        var available = user.Available;

        var item = new CampaignModel
        {
            Id = Guid.NewGuid(),
            Budget = campaign.Budget,
            CreatedAt = DateTime.UtcNow,
            LastUpdated = DateTime.UtcNow,
            Description = campaign.Description,
            EPM = campaign.EPM,
            ExternalId = externalId,
            ImageUrl = campaign.ImageUrl,
            Title = campaign.Title,
            Url = campaign.Url,
            IsArchived = false,
            IsBlocked = false,
            SharedLastWeekOnCampaign = new SharedLastWeekOnCampaignModel(),
            Status = campaign.Budget >= 10,
            SharedTodayOnCampaignModel = new SharedTodayOnCampaignModel(),
            UserModelId = user.Id,
            ClicksLastWeekOnCampaign = new ClicksLastWeekOnCampaignModel(),
            ClicksTodayOnCampaign = new ClicksTodayOnCampaignModel(),
            HistoryClicksByCountriesOnCampaign = new HistoryClicksByCountriesOnCampaignModel(),
            HistoryClicksOnCampaign = new HistoryClicksOnCampaignModel(),
            HistorySharedByUsersOnCampaign = new HistorySharedByUsersOnCampaignModel(),
            HistorySharedOnCampaign = new HistorySharedOnCampaignModel()
        };

        using (var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                // Validate Available balance    
                available.Value = available.Value - campaign.Budget;
                _db.Availables.Update(available);
                await _db.SaveChangesAsync();
                if (available.Value < 0) throw new Exception("Negative balance");

                // Add Campaign
                await _db.Campaigns.AddAsync(item);
                await _db.SaveChangesAsync();

                // Create Payment Transaction
                var paymentTrans = new PaymentTransaction
                {
                    Id = Guid.NewGuid(),
                    Amount = campaign.Budget, 
                    CampaignModelId = item.Id,
                    CompletedAt = DateTime.UtcNow.AddSeconds(1),
                    CreatedAt = DateTime.UtcNow,
                    ExternalId = await Nanoid.Nanoid.GenerateAsync(size: 12),
                    Status = TransactionStatusEnum.Completed,
                    Title = $"Campaign {item.Title} created",
                    TransactionType = TransactionTypeEnum.CreateCampaign,
                    UserModelId = user.Id
                };
                await _db.PaymentTransactions.AddAsync(paymentTrans);
                await _db.SaveChangesAsync();

                //Create a Notification
                var noti = new NotificationModel {
                    Id = Guid.NewGuid(),
                    ExternalId = await Nanoid.Nanoid.GenerateAsync(size: 12),
                    Status = NotificationStatusEnum.Unread,
                    UserModelId = user.Id,
                    Title = "Campaign created",
                    Message = $"Your campaign called '{item.Title}' has been successfully created. It has been assigned a budget of {campaign.Budget.ToString("0.00")} USD. You have {available.Value.ToString("0.00")} USD remaining in your account to create more campaigns.",
                };
                await _db.Notifications.AddAsync(noti);
                await _db.SaveChangesAsync();

                //Create generic event
                var gEvent = new GenericEventModel {
                     Id = Guid.NewGuid(),
                     CreatedAt = DateTime.UtcNow,
                     EventType = "INFO",
                     Message = $"{user.Fullname} created campaign {campaign.Title} with {campaign.Budget} USD, balance remaining {available.Value} USD",
                     Source = "WePromoLink"
                };
                await _db.GenericEvent.AddAsync(gEvent);
                await _db.SaveChangesAsync();

                transaction.Commit();
                return externalId;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                transaction.Rollback();
                throw new Exception("Invalid Data");
            }
        }

    }

    // public async Task<string> CreateSponsoredLink(CreateSponsoredLink link)
    // {

    //     // FirebaseAuth auth = FirebaseAuth.DefaultInstance;
    //     // auth.GetUserAsync() .GetUserByEmailAsync(email);


    //     var email = _db.Emails.Where(e => e.Email.ToLower() == link.Email!.ToLower()).SingleOrDefault();
    //     if (email == null)
    //     {
    //         email = new EmailModel { CreatedAt = DateTime.UtcNow, Email = link.Email!.ToLower() };
    //         _db.Emails.Add(email);
    //         await _db.SaveChangesAsync();
    //     }

    //     string externalId = await Nanoid.Nanoid.GenerateAsync(size: 12);

    //     CampaignModel slink = new CampaignModel
    //     {
    //         ExternalId = externalId,
    //         Budget = 0,
    //         CreatedAt = DateTime.UtcNow,
    //         EmailModelId = email.Id,
    //         EPM = link.EPM,
    //         ImageUrl = link.ImageUrl,
    //         Title = link.Title!,
    //         Description = link.Description,
    //         Url = link.Url!
    //     };

    //     _db.Campaigns.Add(slink);
    //     await _db.SaveChangesAsync();

    //     return externalId;
    // }

    // public async Task<string> FundSponsoredLink(FundSponsoredLink fundLink)
    // {
    //     using (var dbTrans = _db.Database.BeginTransaction())
    //     {
    //         var email = await _db.Emails.Where(e => e.Email.ToLower() == fundLink.Email!.ToLower()).SingleOrDefaultAsync();
    //         if (email == null)
    //         {
    //             email = new EmailModel { CreatedAt = DateTime.UtcNow, Email = fundLink.Email!.ToLower() };
    //             _db.Emails.Add(email);
    //             await _db.SaveChangesAsync();
    //         }

    //         var slink = await _db.Campaigns.Where(e => e.ExternalId == fundLink.SponsoredLinkId).SingleOrDefaultAsync();
    //         if (slink == null) throw new Exception("Sponsored link not found");

    //         PaymentTransaction pay = new PaymentTransaction
    //         {
    //             Title = "DEPOSIT BTC",
    //             SponsoredLinkId = slink.Id,
    //             Amount = fundLink.Amount,
    //             CreatedAt = DateTime.UtcNow,
    //             ExpiredAt = DateTime.UtcNow.AddHours(5),
    //             EmailModelId = email.Id,
    //             IsDeposit = true,
    //             Status = "PENDING"
    //         };
    //         _db.PaymentTransactions.Add(pay);
    //         await _db.SaveChangesAsync();

    //         string link = await _client.CreateInvoice(pay);
    //         if(String.IsNullOrEmpty(link)) throw new Exception("Empty or null link");
    //         pay.PaymentLink = link;
    //         await _db.SaveChangesAsync();
    //         await dbTrans.CommitAsync();
    //         return link ;
    //     }

    // }

    public async Task<MyCampaignList> GetAll(int? page, int? cant, string? filter = "")
    {
        MyCampaignList list = new MyCampaignList();
        page = page ?? 1;
        page = page <= 0 ? 1 : page;

        cant = cant ?? 50;
        filter = filter ?? "";

        list.Items = await _db.Campaigns
        .OrderByDescending(e => e.CreatedAt)
        .Skip((page.Value! - 1) * cant!.Value)
        .Take(cant!.Value)
        .Select(e => new MyCampaign
        {
            Budget = e.Budget,
            EPM = e.EPM,
            Id = e.ExternalId,
            ImageUrl = e.ImageUrl,
            Title = e.Title,
            Url = e.Url,
            Status = e.Status,
            LastClick = e.LastClick,
            LastShared = e.LastShared
        })
        .ToListAsync();
        list.Pagination.Page = page.Value!;
        list.Pagination.TotalPages = ((await _db.Campaigns.CountAsync()) / cant!.Value) + 1;
        return list;
    }
}