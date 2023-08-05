using Microsoft.EntityFrameworkCore;
using WePromoLink.Models;

namespace WePromoLink.Repositories;

public partial class DataRepository
{

    public async Task UpdateCampaign(Guid campaignId)
    {
        var campaign = await _db.Campaigns
        .Include(e => e.ClicksLastWeekOnCampaign)
        .Include(e => e.ClicksTodayOnCampaign)
        .Include(e => e.HistoryClicksByCountriesOnCampaign)
        .Include(e => e.HistoryClicksOnCampaign)
        .Include(e => e.HistorySharedByUsersOnCampaign)
        .Include(e => e.HistorySharedOnCampaign)
        .Include(e => e.SharedLastWeekOnCampaign)
        .Include(e => e.SharedTodayOnCampaignModel)
        .Where(e => e.Id == campaignId)
        .SingleOrDefaultAsync();

        await Update(campaign!.ClicksLastWeekOnCampaign);
        await Update(campaign!.ClicksTodayOnCampaign);
        await Update(campaign!.HistoryClicksByCountriesOnCampaign);
        await Update(campaign!.HistoryClicksOnCampaign);
        await Update(campaign!.HistorySharedByUsersOnCampaign);
        await Update(campaign!.HistorySharedOnCampaign);
        await Update(campaign!.SharedLastWeekOnCampaign);
        await Update(campaign!.SharedTodayOnCampaignModel);
    }

    private async Task Update(HistoryClicksOnCampaignModel model)
    {

        var campaign = await _db.Campaigns
        .Include(e => e.Links)
        .ThenInclude(e => e.Hits)
        .Where(e => e.Id == model.CampaignModelId)
        .SingleOrDefaultAsync();

        var hits = campaign.Links.SelectMany(e => e.Hits);

        DateTime currentDay = DateTime.UtcNow;
        DateTime startDate = currentDay.AddDays(-9);
        var dates = Enumerable.Range(0, 10).Select(offset => startDate.AddDays(offset).Date);

        var list = dates.GroupJoin(hits, date => date.Date, hit => hit.CreatedAt.Date, (date, hit_list) => new { Date = date, Hits = hit_list.Count() })
        .OrderByDescending(e => e.Date)
        .Take(10)
        .ToList();

        if (list.Count >= 1) { model.L0 = list[0].Date; model.X0 = list[0].Hits; } else { model.L0 = DateTime.Now; }
        if (list.Count >= 2) { model.L1 = list[1].Date; model.X1 = list[1].Hits; } else { model.L1 = DateTime.Now; }
        if (list.Count >= 3) { model.L2 = list[2].Date; model.X2 = list[2].Hits; } else { model.L2 = DateTime.Now; }
        if (list.Count >= 4) { model.L3 = list[3].Date; model.X3 = list[3].Hits; } else { model.L3 = DateTime.Now; }
        if (list.Count >= 5) { model.L4 = list[4].Date; model.X4 = list[4].Hits; } else { model.L4 = DateTime.Now; }
        if (list.Count >= 6) { model.L5 = list[5].Date; model.X5 = list[5].Hits; } else { model.L5 = DateTime.Now; }
        if (list.Count >= 7) { model.L6 = list[6].Date; model.X6 = list[6].Hits; } else { model.L6 = DateTime.Now; }
        if (list.Count >= 8) { model.L7 = list[7].Date; model.X7 = list[7].Hits; } else { model.L7 = DateTime.Now; }
        if (list.Count >= 9) { model.L8 = list[8].Date; model.X8 = list[8].Hits; } else { model.L8 = DateTime.Now; }
        if (list.Count >= 10) { model.L9 = list[9].Date; model.X9 = list[9].Hits; } else { model.L9 = DateTime.Now; }

        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.HistoryClicksOnCampaigns.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(HistoryClicksByCountriesOnCampaignModel model)
    {
        var campaign = await _db.Campaigns
         .Include(e => e.Links)
         .ThenInclude(e => e.Hits)
         .Where(e => e.Id == model.CampaignModelId)
         .SingleOrDefaultAsync();

        var list = campaign.Links
        .SelectMany(e => e.Hits)
        .GroupBy(e => e.Country)
        .Select(e => new
        {
            Country = e.Key,
            Clicks = e.Count()
        }).OrderByDescending(e => e.Clicks)
        .Take(10)
        .ToList();

        if (list.Count >= 1) { model.L0 = list[0].Country; model.X0 = list[0].Clicks; } else { model.L0 = ""; }
        if (list.Count >= 2) { model.L1 = list[1].Country; model.X1 = list[1].Clicks; } else { model.L1 = ""; }
        if (list.Count >= 3) { model.L2 = list[2].Country; model.X2 = list[2].Clicks; } else { model.L2 = ""; }
        if (list.Count >= 4) { model.L3 = list[3].Country; model.X3 = list[3].Clicks; } else { model.L3 = ""; }
        if (list.Count >= 5) { model.L4 = list[4].Country; model.X4 = list[4].Clicks; } else { model.L4 = ""; }
        if (list.Count >= 6) { model.L5 = list[5].Country; model.X5 = list[5].Clicks; } else { model.L5 = ""; }
        if (list.Count >= 7) { model.L6 = list[6].Country; model.X6 = list[6].Clicks; } else { model.L6 = ""; }
        if (list.Count >= 8) { model.L7 = list[7].Country; model.X7 = list[7].Clicks; } else { model.L7 = ""; }
        if (list.Count >= 9) { model.L8 = list[8].Country; model.X8 = list[8].Clicks; } else { model.L8 = ""; }
        if (list.Count >= 10) { model.L9 = list[9].Country; model.X9 = list[9].Clicks; } else { model.L9 = ""; }

        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.HistoryClicksByCountriesOnCampaigns.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(ClicksTodayOnCampaignModel model)
    {
        var campaign = await _db.Campaigns
        .Include(e => e.Links)
        .ThenInclude(e => e.Hits)
        .Where(e => e.Id == model.CampaignModelId)
        .SingleOrDefaultAsync();

        var count = campaign.Links
        .SelectMany(e => e.Hits)
        .Where(e => e.CreatedAt.Date == DateTime.UtcNow.Date)
        .Count();

        model.Value = count;
        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.ClicksTodayOnCampaigns.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(ClicksLastWeekOnCampaignModel model)
    {
        DateTime weekAgo = DateTime.UtcNow.AddDays(-8);

        var campaign = await _db.Campaigns
        .Include(e => e.Links)
        .ThenInclude(e => e.Hits)
        .Where(e => e.Id == model.CampaignModelId)
        .SingleOrDefaultAsync();

        var count = campaign.Links
        .SelectMany(e => e.Hits)
        .Where(e => e.CreatedAt.Date >= weekAgo && e.CreatedAt.Date <= DateTime.UtcNow.Date.AddDays(-1))
        .Count();

        model.Value = count;
        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.ClicksLastWeekOnCampaigns.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(HistorySharedByUsersOnCampaignModel model)
    {
        var campaign = await _db.Campaigns
        .Include(e => e.Links)
        .ThenInclude(e => e.User)
        .Where(e => e.Id == model.CampaignModelId)
        .SingleOrDefaultAsync();

        var list = campaign!.Links
        .GroupBy(e => e.User)
        .Select(g => new
        {
            User = g.Key,
            LinkCount = g.Count()
        }).OrderByDescending(e => e.LinkCount)
        .Take(10)
        .ToList();

        if (list.Count >= 1) { model.L0 = list[0].User.Fullname; model.X0 = list[0].LinkCount; } else { model.L0 = ""; }
        if (list.Count >= 2) { model.L1 = list[1].User.Fullname; model.X1 = list[1].LinkCount; } else { model.L1 = ""; }
        if (list.Count >= 3) { model.L2 = list[2].User.Fullname; model.X2 = list[2].LinkCount; } else { model.L2 = ""; }
        if (list.Count >= 4) { model.L3 = list[3].User.Fullname; model.X3 = list[3].LinkCount; } else { model.L3 = ""; }
        if (list.Count >= 5) { model.L4 = list[4].User.Fullname; model.X4 = list[4].LinkCount; } else { model.L4 = ""; }
        if (list.Count >= 6) { model.L5 = list[5].User.Fullname; model.X5 = list[5].LinkCount; } else { model.L5 = ""; }
        if (list.Count >= 7) { model.L6 = list[6].User.Fullname; model.X6 = list[6].LinkCount; } else { model.L6 = ""; }
        if (list.Count >= 8) { model.L7 = list[7].User.Fullname; model.X7 = list[7].LinkCount; } else { model.L7 = ""; }
        if (list.Count >= 9) { model.L8 = list[8].User.Fullname; model.X8 = list[8].LinkCount; } else { model.L8 = ""; }
        if (list.Count >= 10) { model.L9 = list[9].User.Fullname; model.X9 = list[9].LinkCount; } else { model.L9 = ""; }

        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.HistorySharedByUsersOnCampaigns.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(HistorySharedOnCampaignModel model)
    {
        DateTime currentDay = DateTime.UtcNow;
        DateTime startDate = currentDay.AddDays(-9);

        var dates = Enumerable.Range(0, 10).Select(offset => startDate.AddDays(offset).Date);

        var list = dates.GroupJoin(_db.Links.Where(e => e.CampaignModelId == model.CampaignModelId),
        date => date.Date,
        link => link.CreatedAt.Date,
        (date, links) => new
        {
            CreatedAt = date,
            LinksCounter = links.Count(),
        })
        .OrderBy(e => e.CreatedAt)
        .ToList();

        model.L0 = list[0].CreatedAt; model.X0 = list[0].LinksCounter;
        model.L1 = list[1].CreatedAt; model.X1 = list[1].LinksCounter;
        model.L2 = list[2].CreatedAt; model.X2 = list[2].LinksCounter;
        model.L3 = list[3].CreatedAt; model.X3 = list[3].LinksCounter;
        model.L4 = list[4].CreatedAt; model.X4 = list[4].LinksCounter;
        model.L5 = list[5].CreatedAt; model.X5 = list[5].LinksCounter;
        model.L6 = list[6].CreatedAt; model.X6 = list[6].LinksCounter;
        model.L7 = list[7].CreatedAt; model.X7 = list[7].LinksCounter;
        model.L8 = list[8].CreatedAt; model.X8 = list[8].LinksCounter;
        model.L9 = list[9].CreatedAt; model.X9 = list[9].LinksCounter;

        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.HistorySharedOnCampaigns.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(SharedLastWeekOnCampaignModel model)
    {
        DateTime weekAgo = DateTime.UtcNow.AddDays(-8);
        var sharedsLastWeek = await _db.Links
        .Where(e => e.CampaignModelId == model.CampaignModelId)
        .Where(e => e.CreatedAt.Date >= weekAgo && e.CreatedAt.Date <= DateTime.UtcNow.Date.AddDays(-1))
        .CountAsync();

        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);
        model.Value = sharedsLastWeek;

        _db.SharedLastWeekOnCampaigns.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(SharedTodayOnCampaignModel model)
    {
        var sharedsToday = await _db.Links
        .Where(e => e.CampaignModelId == model.CampaignModelId)
        .Where(e => e.CreatedAt.Date == DateTime.UtcNow.Date)
        .CountAsync();

        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.Value = sharedsToday;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.SharedTodayOnCampaigns.Update(model);
        await _db.SaveChangesAsync();
    }


}