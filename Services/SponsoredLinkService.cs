using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Models;
using WePromoLink.Settings;

namespace WePromoLink.Services;

public class SponsoredLinkService : ISponsoredLinkService
{
    private readonly IPaymentService _client;
    private readonly IOptions<BTCPaySettings> _options;
    private readonly DataContext _db;
    public SponsoredLinkService(DataContext db, IOptions<BTCPaySettings> options, IPaymentService client)
    {
        _db = db;
        _options = options;
        _client = client;
    }
    public async Task<string> CreateSponsoredLink(CreateSponsoredLink link)
    {
        var email = _db.Emails.Where(e => e.Email.ToLower() == link.Email!.ToLower()).SingleOrDefault();
        if (email == null)
        {
            email = new EmailModel { CreatedAt = DateTime.UtcNow, Email = link.Email!.ToLower() };
            _db.Emails.Add(email);
            await _db.SaveChangesAsync();
        }

        string externalId = await Nanoid.Nanoid.GenerateAsync(size: 12);

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
        using (var dbTrans = _db.Database.BeginTransaction())
        {
            var email = await _db.Emails.Where(e => e.Email.ToLower() == fundLink.Email!.ToLower()).SingleOrDefaultAsync();
            if (email == null)
            {
                email = new EmailModel { CreatedAt = DateTime.UtcNow, Email = fundLink.Email!.ToLower() };
                _db.Emails.Add(email);
                await _db.SaveChangesAsync();
            }

            var slink = await _db.SponsoredLinks.Where(e => e.ExternalId == fundLink.SponsoredLinkId).SingleOrDefaultAsync();
            if (slink == null) throw new Exception("Sponsored link not found");

            PaymentTransaction pay = new PaymentTransaction
            {
                Title = "DEPOSIT BTC",
                SponsoredLinkId = slink.Id,
                Amount = fundLink.Amount,
                CreatedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(5),
                EmailModelId = email.Id,
                IsDeposit = true,
                Status = "PENDING"
            };
            _db.PaymentTransactions.Add(pay);
            await _db.SaveChangesAsync();

            string link = await _client.CreateInvoice(pay);
            if(String.IsNullOrEmpty(link)) throw new Exception("Empty or null link");
            pay.PaymentLink = link;
            await _db.SaveChangesAsync();
            await dbTrans.CommitAsync();
            return link ;
        }

    }

    public async Task<SponsoredLinkList> ListSponsoredLinks(int? page)
    {
        SponsoredLinkList list = new SponsoredLinkList();
        int cant = 50;
        page = page ?? 1;
        page = page <= 0 ? 1 : page;

        list.SponsoredLinks = await _db.SponsoredLinks
        .OrderByDescending(e => e.CreatedAt)
        .Skip((page.Value! - 1) * cant)
        .Take(cant)
        .Select(e => new SponsoredLink
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