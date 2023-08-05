using Microsoft.EntityFrameworkCore;
using WePromoLink.Enums;
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
        await Update(user!.ClickLastWeekOnLinksUser);
        await Update(user!.ClicksLastWeekOnCampaignUser);
        await Update(user!.ClicksTodayOnCampaignUser);
        await Update(user!.ClicksTodayOnLinksUser);
        await Update(user!.EarnLastWeekUser);
        await Update(user!.EarnTodayUser);
        await Update(user!.HistoryClicksByCountriesOnCampaignUser);
        await Update(user!.HistoryClicksByCountriesOnLinkUser);
        await Update(user!.HistoryClicksOnCampaignUser);
        await Update(user!.HistoryClicksOnSharesUser);
        // await Update(user!.HistoryEarnByCountriesUser);
        await Update(user!.HistoryEarnOnLinksUser);
        await Update(user!.HistorySharedByUsersUser);
        await Update(user!.HistoryClicksOnLinksUser);
    }

    // private async Task Update(HistoryEarnByCountriesUserModel model)
    // {


    // }

    private async Task Update(HistoryClicksOnLinksUserModel model)
    {
        DateTime currentDay = DateTime.UtcNow;
        DateTime startDate = currentDay.AddDays(-9);
        var dates = Enumerable.Range(0, 10).Select(offset => startDate.AddDays(offset).Date);

        var links = await _db.Links
        .Include(e => e.Hits)
        .Where(e => e.UserModelId == model.UserModelId)
        .ToListAsync();

        var hits_list = links.SelectMany(e => e.Hits);

        var list = dates.GroupJoin(hits_list, date => date.Date, hit => hit.CreatedAt.Date,
            (date, hit) => new
            {
                CreatedAt = date,
                Amount = hit.Count(),
            })
            .OrderBy(e => e.CreatedAt)
            .ToList();

        model.L0 = list[0].CreatedAt; model.X0 = list[0].Amount;
        model.L1 = list[1].CreatedAt; model.X1 = list[1].Amount;
        model.L2 = list[2].CreatedAt; model.X2 = list[2].Amount;
        model.L3 = list[3].CreatedAt; model.X3 = list[3].Amount;
        model.L4 = list[4].CreatedAt; model.X4 = list[4].Amount;
        model.L5 = list[5].CreatedAt; model.X5 = list[5].Amount;
        model.L6 = list[6].CreatedAt; model.X6 = list[6].Amount;
        model.L7 = list[7].CreatedAt; model.X7 = list[7].Amount;
        model.L8 = list[8].CreatedAt; model.X8 = list[8].Amount;
        model.L9 = list[9].CreatedAt; model.X9 = list[9].Amount;

        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.HistoryClicksOnLinksUsers.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(HistoryEarnOnLinksUserModel model)
    {
        DateTime currentDay = DateTime.UtcNow;
        DateTime startDate = currentDay.AddDays(-9);
        var dates = Enumerable.Range(0, 10).Select(offset => startDate.AddDays(offset).Date);

        var links = await _db.Links
        .Include(e => e.Transactions)
        .Where(e => e.UserModelId == model.UserModelId)
        .ToListAsync();

        var trans = links.SelectMany(e => e.Transactions).Where(e => e.TransactionType == TransactionTypeEnum.Profit);

        var list = dates.GroupJoin(trans, date => date.Date, tran => tran.CreatedAt.Date,
            (date, tran) => new
            {
                CreatedAt = date,
                Amount = tran.Count(),
            })
            .OrderBy(e => e.CreatedAt)
            .ToList();

        model.L0 = list[0].CreatedAt; model.X0 = list[0].Amount;
        model.L1 = list[1].CreatedAt; model.X1 = list[1].Amount;
        model.L2 = list[2].CreatedAt; model.X2 = list[2].Amount;
        model.L3 = list[3].CreatedAt; model.X3 = list[3].Amount;
        model.L4 = list[4].CreatedAt; model.X4 = list[4].Amount;
        model.L5 = list[5].CreatedAt; model.X5 = list[5].Amount;
        model.L6 = list[6].CreatedAt; model.X6 = list[6].Amount;
        model.L7 = list[7].CreatedAt; model.X7 = list[7].Amount;
        model.L8 = list[8].CreatedAt; model.X8 = list[8].Amount;
        model.L9 = list[9].CreatedAt; model.X9 = list[9].Amount;

        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.HistoryEarnOnLinksUsers.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(HistoryClicksOnSharesUserModel model)
    {
        DateTime currentDay = DateTime.UtcNow;
        DateTime startDate = currentDay.AddDays(-9);

        var dates = Enumerable.Range(0, 10).Select(offset => startDate.AddDays(offset).Date);

        var links = await _db.Links
        .Include(e => e.Hits)
        .Where(e => e.UserModelId == model.UserModelId)
        .ToListAsync();

        var hit_lists = links.SelectMany(e => e.Hits);

        var list = dates.GroupJoin(hit_lists, date => date.Date, hit => hit.CreatedAt.Date,
            (date, hits) => new
            {
                CreatedAt = date,
                Clicks = hits.Count(),
            })
            .OrderBy(e => e.CreatedAt)
            .ToList();

        model.L0 = list[0].CreatedAt; model.X0 = list[0].Clicks;
        model.L1 = list[1].CreatedAt; model.X1 = list[1].Clicks;
        model.L2 = list[2].CreatedAt; model.X2 = list[2].Clicks;
        model.L3 = list[3].CreatedAt; model.X3 = list[3].Clicks;
        model.L4 = list[4].CreatedAt; model.X4 = list[4].Clicks;
        model.L5 = list[5].CreatedAt; model.X5 = list[5].Clicks;
        model.L6 = list[6].CreatedAt; model.X6 = list[6].Clicks;
        model.L7 = list[7].CreatedAt; model.X7 = list[7].Clicks;
        model.L8 = list[8].CreatedAt; model.X8 = list[8].Clicks;
        model.L9 = list[9].CreatedAt; model.X9 = list[9].Clicks;

        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.HistoryClicksOnSharesUsers.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(HistoryClicksOnCampaignUserModel model)
    {
        DateTime currentDay = DateTime.UtcNow;
        DateTime startDate = currentDay.AddDays(-9);

        var dates = Enumerable.Range(0, 10).Select(offset => startDate.AddDays(offset).Date);

        var user = _db.Users
        .Include(e => e.Campaigns)
        .ThenInclude(e => e.Links)
        .ThenInclude(e => e.Hits)
        .Where(e => e.Id == model.UserModelId)
        .SingleOrDefault();

        var hit_lists = user.Campaigns.SelectMany(e=>e.Links).SelectMany(e => e.Hits);

        var list = dates.GroupJoin(hit_lists,
        date => date.Date,
        hit => hit.CreatedAt.Date,
        (date, hits) => new
        {
            CreatedAt = date,
            Clicks = hits.Count(),
        })
        .OrderBy(e => e.CreatedAt)
        .ToList();

        model.L0 = list[0].CreatedAt; model.X0 = list[0].Clicks;
        model.L1 = list[1].CreatedAt; model.X1 = list[1].Clicks;
        model.L2 = list[2].CreatedAt; model.X2 = list[2].Clicks;
        model.L3 = list[3].CreatedAt; model.X3 = list[3].Clicks;
        model.L4 = list[4].CreatedAt; model.X4 = list[4].Clicks;
        model.L5 = list[5].CreatedAt; model.X5 = list[5].Clicks;
        model.L6 = list[6].CreatedAt; model.X6 = list[6].Clicks;
        model.L7 = list[7].CreatedAt; model.X7 = list[7].Clicks;
        model.L8 = list[8].CreatedAt; model.X8 = list[8].Clicks;
        model.L9 = list[9].CreatedAt; model.X9 = list[9].Clicks;

        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.HistoryClicksOnCampaignUsers.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(HistoryClicksByCountriesOnLinkUserModel model)
    {
        var user = await _db.Users
        .Include(e => e.Links)
        .ThenInclude(e => e.Hits)
        .Where(e => e.Id == model.UserModelId)
        .SingleOrDefaultAsync();

        var hit_list = user.Links.SelectMany(e => e.Hits);

        var list = hit_list
        .GroupBy(e => e.Country)
        .Select(e => new { Country = e.Key, Clicks = e.Count() })
        .OrderByDescending(e => e.Clicks)
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

        _db.HistoryClicksByCountriesOnLinkUsers.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(HistoryClicksByCountriesOnCampaignUserModel model)
    {
        var user = await _db.Users
        .Include(e => e.Campaigns)
        .ThenInclude(e => e.Links)
        .ThenInclude(e => e.Hits)
        .Where(e => e.Id == model.UserModelId)
        .SingleOrDefaultAsync();

        var hit_list = user.Campaigns.SelectMany(e => e.Links).SelectMany(e => e.Hits);

        var list = hit_list
        .GroupBy(e => e.Country)
        .Select(e => new { Country = e.Key, Clicks = e.Count() })
        .OrderByDescending(e => e.Clicks)
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

        _db.HistoryClicksByCountriesOnCampaignUsers.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(EarnTodayUserModel model)
    {
        var count = await _db.PaymentTransactions
        .Where(e => e.TransactionType == TransactionTypeEnum.Profit)
        .Where(e => e.Status == TransactionStatusEnum.Completed)
        .Where(e => e.Id == model.UserModelId)
        .Where(e => e.CreatedAt.Date == DateTime.UtcNow.Date)
        .Select(e => e.Amount)
        .CountAsync();

        model.Value = count;
        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.EarnTodayUsers.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(EarnLastWeekUserModel model)
    {
        DateTime weekAgo = DateTime.UtcNow.AddDays(-8);
        DateTime yesterday = DateTime.UtcNow.AddDays(-1);

        var count = await _db.PaymentTransactions
        .Where(e => e.TransactionType == TransactionTypeEnum.Profit)
        .Where(e => e.Status == TransactionStatusEnum.Completed)
        .Where(e => e.Id == model.UserModelId)
        .Where(e => e.CreatedAt.Date >= weekAgo && e.CreatedAt.Date <= yesterday)
        .Select(e => e.Amount)
        .CountAsync();

        model.Value = count;
        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.EarnLastWeekUsers.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(ClicksTodayOnLinksUserModel model)
    {
        var user = await _db.Users
        .Include(e => e.Links)
        .ThenInclude(e => e.Hits)
        .Where(e => e.Id == model.UserModelId)
        .SingleOrDefaultAsync();

        var count = user.Campaigns.SelectMany(e => e.Links).SelectMany(e => e.Hits)
            .Where(e => e.CreatedAt.Date == DateTime.UtcNow.Date)
            .Count();

        model.Value = count;
        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.ClicksTodayOnLinksUsers.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(ClicksTodayOnCampaignUserModel model)
    {
        var user = await _db.Users
        .Include(e => e.Campaigns)
        .ThenInclude(e => e.Links)
        .ThenInclude(e => e.Hits)
        .Where(e => e.Id == model.UserModelId)
        .SingleOrDefaultAsync();

        var count = user.Campaigns.SelectMany(e => e.Links).SelectMany(e => e.Hits)
            .Where(e => e.CreatedAt.Date == DateTime.UtcNow.Date)
            .Count();

        model.Value = count;
        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.ClicksTodayOnCampaignUsers.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(ClicksLastWeekOnCampaignUserModel model)
    {
        DateTime weekAgo = DateTime.UtcNow.AddDays(-8);

        var user = await _db.Users
        .Include(e => e.Campaigns)
        .ThenInclude(e => e.Links)
        .ThenInclude(e => e.Hits)
        .Where(e => e.Id == model.UserModelId)
        .SingleOrDefaultAsync();

        var count = user.Campaigns.SelectMany(e => e.Links).SelectMany(e => e.Hits)
       .Where(e => e.CreatedAt.Date >= weekAgo && e.CreatedAt.Date <= DateTime.UtcNow.Date.AddDays(-1))
       .Count();

        model.Value = count;
        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.ClicksLastWeekOnCampaignUsers.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(ClickLastWeekOnLinksUserModel model)
    {
        DateTime weekAgo = DateTime.UtcNow.AddDays(-8);

        var user = await _db.Users
        .Include(e => e.Links)
        .ThenInclude(e => e.Hits)
        .Where(e => e.Id == model.UserModelId)
        .SingleOrDefaultAsync();

        var count = user.Links.SelectMany(e => e.Hits)
        .Where(e => e.CreatedAt.Date >= weekAgo && e.CreatedAt.Date <= DateTime.UtcNow.Date.AddDays(-1))
        .Count();

        model.Value = count;
        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.ClickLastWeekOnLinksUsers.Update(model);
        await _db.SaveChangesAsync();
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
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

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
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);
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
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);
        model.Value = sharedsToday;

        _db.SharedTodayUsers.Update(model);
        await _db.SaveChangesAsync();
    }
}