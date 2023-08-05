using Microsoft.EntityFrameworkCore;
using WePromoLink.Enums;
using WePromoLink.Models;

namespace WePromoLink.Repositories;

public partial class DataRepository
{
    public async Task UpdateLink(Guid linkId)
    {
        var link = await _db.Links
        .Include(e => e.ClicksLastWeekOnLink)
        .Include(e => e.ClicksTodayOnLink)
        .Include(e => e.EarnLastWeekOnLink)
        .Include(e => e.EarnTodayOnLink)
        .Include(e => e.HistoryClicksByCountriesOnLink)
        .Include(e => e.HistoryClicksOnLink)
        .Include(e => e.HistoryEarnByCountriesOnLink)
        .Include(e => e.HistoryEarnOnLink)
        .Where(e => e.Id == linkId)
        .SingleOrDefaultAsync();

        await Update(link!.ClicksLastWeekOnLink);
        await Update(link!.ClicksTodayOnLink);
        await Update(link!.EarnLastWeekOnLink);
        await Update(link!.EarnTodayOnLink);
        await Update(link!.HistoryClicksByCountriesOnLink);
        await Update(link!.HistoryClicksOnLink);
        await Update(link!.HistoryEarnByCountriesOnLink);
        await Update(link!.HistoryEarnOnLink);
    }

    private async Task Update(ClicksLastWeekOnLinkModel model)
    {
        DateTime weekAgo = DateTime.UtcNow.AddDays(-8);
        var link = await _db.Links.Where(e => e.Id == model.LinkModelId).SingleOrDefaultAsync();
        if (link == null) return;

        var count = _db.Hits
        .Where(e => e.LinkModelId == link.Id)
        .Where(e => e.CreatedAt.Date >= weekAgo && e.CreatedAt.Date <= DateTime.UtcNow.Date.AddDays(-1))
        .Count();

        model.Value = count;
        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.ClicksLastWeekOnLinks.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(ClicksTodayOnLinkModel model)
    {
        var link = await _db.Links.Where(e => e.Id == model.LinkModelId).SingleOrDefaultAsync();
        if (link == null) return;

        var count = _db.Hits
        .Where(e => e.LinkModelId == link.Id)
        .Where(e => e.CreatedAt.Date == DateTime.UtcNow.Date)
        .Count();

        model.Value = count;
        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.ClicksTodayOnLinks.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(EarnLastWeekOnLinkModel model)
    {
        DateTime weekAgo = DateTime.UtcNow.AddDays(-8);
        var link = await _db.Links.Where(e => e.Id == model.LinkModelId).SingleOrDefaultAsync();
        if (link == null) return;

        var total = await _db.PaymentTransactions
        .Where(e => e.LinkModelId == link.Id)
        .Where(e => e.Status == TransactionStatusEnum.Completed)
        .Where(e => e.TransactionType == TransactionTypeEnum.Profit)
        .Where(e => e.CreatedAt.Date >= weekAgo && e.CreatedAt.Date <= DateTime.UtcNow.Date.AddDays(-1))
        .Select(e => e.Amount)
        .SumAsync();

        model.Value = total;
        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.EarnLastWeekOnLinks.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(EarnTodayOnLinkModel model)
    {
        var link = await _db.Links.Where(e => e.Id == model.LinkModelId).SingleOrDefaultAsync();
        if (link == null) return;

        var total = await _db.PaymentTransactions
        .Where(e => e.LinkModelId == link.Id)
        .Where(e => e.Status == TransactionStatusEnum.Completed)
        .Where(e => e.TransactionType == TransactionTypeEnum.Profit)
        .Where(e => e.CreatedAt.Date == DateTime.UtcNow.Date)
        .Select(e => e.Amount)
        .SumAsync();

        model.Value = total;
        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.EarnTodayOnLinks.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(HistoryClicksByCountriesOnLinkModel model)
    {
        var hits = await _db.Hits
        .Where(e => e.LinkModelId == model.LinkModelId)
        .ToListAsync();

        var list = hits
        .GroupBy(e => e.Country)
        .Select(g => new
        {
            Country = g.Key,
            ClickCount = g.Count()
        })
        .OrderByDescending(e => e.ClickCount)
        .Take(10)
        .ToList();

        if (list.Count >= 1) { model.L0 = list[0].Country; model.X0 = list[0].ClickCount; } else { model.L0 = ""; }
        if (list.Count >= 2) { model.L1 = list[1].Country; model.X1 = list[1].ClickCount; } else { model.L1 = ""; }
        if (list.Count >= 3) { model.L2 = list[2].Country; model.X2 = list[2].ClickCount; } else { model.L2 = ""; }
        if (list.Count >= 4) { model.L3 = list[3].Country; model.X3 = list[3].ClickCount; } else { model.L3 = ""; }
        if (list.Count >= 5) { model.L4 = list[4].Country; model.X4 = list[4].ClickCount; } else { model.L4 = ""; }
        if (list.Count >= 6) { model.L5 = list[5].Country; model.X5 = list[5].ClickCount; } else { model.L5 = ""; }
        if (list.Count >= 7) { model.L6 = list[6].Country; model.X6 = list[6].ClickCount; } else { model.L6 = ""; }
        if (list.Count >= 8) { model.L7 = list[7].Country; model.X7 = list[7].ClickCount; } else { model.L7 = ""; }
        if (list.Count >= 9) { model.L8 = list[8].Country; model.X8 = list[8].ClickCount; } else { model.L8 = ""; }
        if (list.Count >= 10) { model.L9 = list[9].Country; model.X9 = list[9].ClickCount; } else { model.L9 = ""; }

        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.HistoryClicksByCountriesOnLinks.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(HistoryEarnByCountriesOnLinkModel model)
    {
        var trans = await _db.PaymentTransactions
        .Include(e => e.Link)
        .ThenInclude(e => e.Hits)
        .Where(e => e.TransactionType == TransactionTypeEnum.Profit)
        .Where(e => e.LinkModelId == model.LinkModelId)
        .ToListAsync();

        var list = trans.GroupBy(e => e.Link?.Hits?.FirstOrDefault()?.Country).Select(group => new
        {
            Country = group.Key,
            Profit = group.Sum(e => e.Amount)
        })
        .OrderByDescending(e => e.Profit)
        .Take(10)
        .ToList();
        
        if (list.Count >= 1) { model.L0 = list[0].Country; model.X0 = list[0].Profit; } else { model.L0 = ""; }
        if (list.Count >= 2) { model.L1 = list[1].Country; model.X1 = list[1].Profit; } else { model.L1 = ""; }
        if (list.Count >= 3) { model.L2 = list[2].Country; model.X2 = list[2].Profit; } else { model.L2 = ""; }
        if (list.Count >= 4) { model.L3 = list[3].Country; model.X3 = list[3].Profit; } else { model.L3 = ""; }
        if (list.Count >= 5) { model.L4 = list[4].Country; model.X4 = list[4].Profit; } else { model.L4 = ""; }
        if (list.Count >= 6) { model.L5 = list[5].Country; model.X5 = list[5].Profit; } else { model.L5 = ""; }
        if (list.Count >= 7) { model.L6 = list[6].Country; model.X6 = list[6].Profit; } else { model.L6 = ""; }
        if (list.Count >= 8) { model.L7 = list[7].Country; model.X7 = list[7].Profit; } else { model.L7 = ""; }
        if (list.Count >= 9) { model.L8 = list[8].Country; model.X8 = list[8].Profit; } else { model.L8 = ""; }
        if (list.Count >= 10) { model.L9 = list[9].Country; model.X9 = list[9].Profit; } else { model.L9 = ""; }

        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);

        _db.HistoryEarnByCountriesOnLinks.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(HistoryEarnOnLinkModel model)
    {
        DateTime currentDay = DateTime.UtcNow;
        DateTime startDate = currentDay.AddDays(-9);

        var dates = Enumerable.Range(0, 10).Select(offset => startDate.AddDays(offset).Date);

        var list = dates.GroupJoin(_db.PaymentTransactions
        .Where(e => e.LinkModelId == model.LinkModelId)
        .Where(e => e.Status == TransactionStatusEnum.Completed)
        .Where(e => e.TransactionType == TransactionTypeEnum.Profit),
        date => date.Date,
        payTrans => payTrans.CreatedAt.Date,
        (date, trans) => new
        {
            CreatedAt = date,
            ProfitTotal = trans.Sum(e => e.Amount),
        })
        .OrderBy(e => e.CreatedAt)
        .ToList();

        model.L0 = list[0].CreatedAt; model.X0 = list[0].ProfitTotal;
        model.L1 = list[1].CreatedAt; model.X1 = list[1].ProfitTotal;
        model.L2 = list[2].CreatedAt; model.X2 = list[2].ProfitTotal;
        model.L3 = list[3].CreatedAt; model.X3 = list[3].ProfitTotal;
        model.L4 = list[4].CreatedAt; model.X4 = list[4].ProfitTotal;
        model.L5 = list[5].CreatedAt; model.X5 = list[5].ProfitTotal;
        model.L6 = list[6].CreatedAt; model.X6 = list[6].ProfitTotal;
        model.L7 = list[7].CreatedAt; model.X7 = list[7].ProfitTotal;
        model.L8 = list[8].CreatedAt; model.X8 = list[8].ProfitTotal;
        model.L9 = list[9].CreatedAt; model.X9 = list[9].ProfitTotal;

        model.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
        model.LastModified = DateTime.UtcNow;
        model.ExpiredAt = DateTime.UtcNow.AddDays(1);
        
        _db.HistoryEarnOnLinks.Update(model);
        await _db.SaveChangesAsync();
    }

    private async Task Update(HistoryClicksOnLinkModel model)
    {
        DateTime currentDay = DateTime.UtcNow;
        DateTime startDate = currentDay.AddDays(-9);

        var dates = Enumerable.Range(0, 10).Select(offset => startDate.AddDays(offset).Date);

        var list = dates.GroupJoin(_db.Hits
        .Where(e => e.LinkModelId == model.LinkModelId),
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

        _db.HistoryClicksOnLinkModels.Update(model);
        await _db.SaveChangesAsync();
    }
}