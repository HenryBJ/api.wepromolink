using Microsoft.EntityFrameworkCore;
using WePromoLink.Models;

namespace WePromoLink.Repositories;

public partial class DataRepository
{
    public async Task UpdateUser(Guid userId)
    {
        var user = await _db.Users
        .Include(e => e.SharedToday)
        .Include(e => e.SharedLastWeek)
        .Include(e => e.ClickLastWeekOnLinksUser)
        .Include(e => e.ClicksLastWeekOnCampaignUser)
        .Include(e => e.ClicksTodayOnCampaignUser)
        .Include(e => e.ClicksTodayOnLinksUser)
        .Include(e => e.EarnLastWeekUser)
        .Include(e => e.EarnTodayUser)
        .Include(e => e.HistoryClicksByCountriesOnCampaignUser)
        .Include(e => e.HistoryClicksByCountriesOnLinkUser)
        .Include(e => e.HistoryClicksOnCampaignUser)
        .Include(e => e.HistoryClicksOnSharesUser)
        .Include(e => e.HistoryEarnByCountriesUser)
        .Include(e => e.HistoryEarnOnLinksUser)
        .Include(e => e.HistorySharedByUsersUser)
        .Include(e => e.HistoryClicksOnLinksUser)
        .Where(e => e.Id == userId)
        .SingleOrDefaultAsync();

        await Update(user!.SharedToday);
        await Update(user!.SharedLastWeek);
        // await Update(user!.ClickLastWeekOnLinksUser);
        // await Update(user!.ClicksLastWeekOnCampaignUser);
        // await Update(user!.ClicksTodayOnCampaignUser);
        // await Update(user!.ClicksTodayOnLinksUser);
        // await Update(user!.EarnLastWeekUser);
        // await Update(user!.EarnTodayUser);
        // await Update(user!.HistoryClicksByCountriesOnCampaignUser);
        // await Update(user!.HistoryClicksByCountriesOnLinkUser);
        // await Update(user!.HistoryClicksOnCampaignUser);
        // await Update(user!.HistoryClicksOnSharesUser);
        // await Update(user!.HistoryEarnByCountriesUser);
        // await Update(user!.HistoryEarnOnLinksUser);
        // await Update(user!.HistorySharedByUsersUser);
        // await Update(user!.HistoryClicksOnLinksUser);
    }

    private async Task Update(HistorySharedByUsersUserModel model)
    {
        var user = await _db.Users
        .Include(e => e.Campaigns)
        .ThenInclude(e => e.Links)
        .ThenInclude(e => e.User)
        .Where(e => e.Id == model.UserModelId)
        .FirstOrDefaultAsync();

        var links = user!.Campaigns.SelectMany(e => e.Links).ToList();

        var list = links
        .GroupBy(e => e.User)
        .Select(g => new
        {
            User = g.Key,
            LinkCount = g.Count()
        })
        .OrderByDescending(e => e.LinkCount)
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
        _db.HistorySharedByUsersUsers.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(SharedLastWeekUserModel model)
    {
        DateTime weekAgo = DateTime.UtcNow.AddDays(-7);

        var user = await _db.Users
        .Include(e => e.Campaigns)
        .ThenInclude(e => e.Links)
        .Where(e => e.Id == model.UserModelId)
        .SingleOrDefaultAsync();

        var links = user!.Campaigns.SelectMany(e => e.Links).ToList();

        var sharedsLastWeek = links
        .Where(e => e.CreatedAt.Date >= weekAgo && e.CreatedAt.Date <= DateTime.UtcNow.Date)
        .Count();

        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.Value = sharedsLastWeek;

        _db.SharedLastWeekUsers.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(SharedTodayUserModel model)
    {

        var user = await _db.Users
        .Include(e => e.Campaigns)
        .ThenInclude(e => e.Links)
        .Where(e => e.Id == model.UserModelId)
        .SingleOrDefaultAsync();

        var links = user!.Campaigns.SelectMany(e => e.Links).ToList();

        var sharedsToday = links
        .Where(e => e.CreatedAt.Date == DateTime.UtcNow.Date)
        .Count();

        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.Value = sharedsToday;

        _db.SharedTodayUsers.Update(model);
        await _db.SaveChangesAsync();
    }
}