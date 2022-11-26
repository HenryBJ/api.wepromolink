using BTCPayServer.Client;
using BTCPayServer.Client.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Models;
using WePromoLink.Settings;
using static BTCPayServer.Client.Models.InvoiceDataBase;

namespace WePromoLink.Services;

public class SponsoredLinkService : ISponsoredLinkService
{
    private readonly BTCPayServerClient _client;
    private readonly IOptions<BTCPaySettings> _options;
    private readonly DataContext _db;
    public SponsoredLinkService(DataContext db, BTCPayServerClient client, IOptions<BTCPaySettings> options)
    {
        _db = db;
        _client = client;
        _options = options;
    }
    public async Task<string> CreateSponsoredLink(CreateSponsoredLink link)
    {
        var email = _db.Emails.Where(e=>e.Email.ToLower() == link.Email!.ToLower()).SingleOrDefault();
        if(email == null)
        {
            email = new EmailModel{CreatedAt = DateTime.UtcNow, Email = link.Email!.ToLower()};
            _db.Emails.Add(email);
            await _db.SaveChangesAsync();
        }

        string  externalId = await Nanoid.Nanoid.GenerateAsync(size:12);

         SponsoredLinkModel slink = new SponsoredLinkModel
         {
            ExternalId = externalId,
            Budget = 0,
            CreatedAt = DateTime.UtcNow,
            EmailModelId = email.Id,
            EPM = link.EPM,
            ImageUrl = link.ImageUrl,
            Title = link.Title!,
            Description = link.Description,
            RemainBudget = 0,
            Url = link.Url!            
         }; 

         _db.SponsoredLinks.Add(slink);
         await _db.SaveChangesAsync();

         return externalId;
    }

    public async Task<string> FundSponsoredLink(FundSponsoredLink fundLink)
    {

        var email = await _db.Emails.Where(e=>e.Email.ToLower() == fundLink.Email!.ToLower()).SingleOrDefaultAsync();
        if(email == null)
        {
            email = new EmailModel{CreatedAt = DateTime.UtcNow, Email = fundLink.Email!.ToLower()};
            _db.Emails.Add(email);
            await _db.SaveChangesAsync();
        }

        var slink = await _db.SponsoredLinks.Where(e=>e.ExternalId == fundLink.SponsoredLinkId).SingleOrDefaultAsync();
        if(slink == null) throw new Exception("Sponsored link not found");


        CreateInvoiceRequest request = new CreateInvoiceRequest
        {
            Currency = "BTC",
            Amount = fundLink.Amount,
            Checkout = new CheckoutOptions
            {
                SpeedPolicy = SpeedPolicy.MediumSpeed,
                Expiration = TimeSpan.FromHours(5),
                RedirectURL = fundLink.RedirectUrl
            },
            Metadata = new Newtonsoft.Json.Linq.JObject(new {EmailId = email.Id, SponsoredId = slink.Id})
        };
        
        var response = await _client.CreateInvoice(_options.Value.StoreId, request);
        return response.CheckoutLink;
    }

    public async Task<SponsoredLinkList> ListSponsoredLinks(int? page)
    {
        SponsoredLinkList list = new SponsoredLinkList();
        int cant = 50;
        page = page ?? 1;
        page = page <= 0? 1: page;
        
        list.SponsoredLinks = await _db.SponsoredLinks
        .OrderByDescending(e=>e.CreatedAt)
        .Skip((page.Value!-1) * cant)
        .Take(cant)
        .Select(e=>new SponsoredLink
        {
            Budget = e.RemainBudget,
            EPM = e.EPM,
            Id = e.ExternalId,
            ImageUrl = e.ImageUrl,
            Title = e.Title,
            Url = e.Url
        })
        .ToListAsync();
        list.Page = page.Value!;
        list.TotalPages = ((await _db.SponsoredLinks.CountAsync()) / cant) + 1;
        return list;
    }
}