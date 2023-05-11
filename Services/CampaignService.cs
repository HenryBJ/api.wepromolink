using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Models;
using WePromoLink.Settings;

namespace WePromoLink.Services;

public class CampaignService : ICampaignService
{
    private readonly IPaymentService _client;
    private readonly IOptions<BTCPaySettings> _options;
    private readonly DataContext _db;
    public CampaignService(DataContext db, IOptions<BTCPaySettings> options, IPaymentService client)
    {
        _db = db;
        _options = options;
        _client = client;
    }

    public async Task<string> CreateCampaign(Campaign campaign)
    {
        var item = new CampaignModel
        {
            Budget = campaign.Budget,
            CreatedAt = DateTime.UtcNow,
            Description = campaign.Description,
            EPM = campaign.EPM,
            ExternalId = await Nanoid.Nanoid.GenerateAsync(size: 12),
            ImageUrl = campaign.ImageUrl,
            Title = campaign.Title,
            Url = campaign.Url
        };

        return "ok"; // for testing

        // Asignarle el usuario que mando a crear la campaign

        // Validar que de verdad se le pueda asignar ese budget
        
        // Asignarle el budget

        // Generar una transaccion de : Creacion de campaign si el budget es > 0

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