using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;
using WePromoLink.Shared.DTO.Messages;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.Controller.Tasks;

public class UpdateStatsTask
{
    private readonly IServiceScopeFactory _fac;

    public UpdateStatsTask(IServiceScopeFactory fac)
    {
        _fac = fac;
    }

    public async Task Update()
    {
        using var scope = _fac.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<DataContext>();
        var _campaignUpdater = scope.ServiceProvider.GetRequiredService<MessageBroker<UpdateCampaignMessage>>();
        var _userUpdater = scope.ServiceProvider.GetRequiredService<MessageBroker<UpdateUserMessage>>();
        var _linkUpdater = scope.ServiceProvider.GetRequiredService<MessageBroker<UpdateLinkMessage>>();

        List<Guid> usersList = await _db.Users
        .Where(e =>
            e.ClickLastWeekOnLinksUser.ExpiredAt.Value < DateTime.UtcNow ||
            e.ClicksLastWeekOnCampaignUser.ExpiredAt.Value < DateTime.UtcNow ||
            e.ClicksTodayOnCampaignUser.ExpiredAt.Value < DateTime.UtcNow ||
            e.ClicksTodayOnLinksUser.ExpiredAt.Value < DateTime.UtcNow ||
            e.EarnLastWeekUser.ExpiredAt.Value < DateTime.UtcNow ||
            e.EarnTodayUser.ExpiredAt.Value < DateTime.UtcNow ||
            e.HistoryClicksByCountriesOnCampaignUser.ExpiredAt.Value < DateTime.UtcNow ||
            e.HistoryClicksByCountriesOnLinkUser.ExpiredAt.Value < DateTime.UtcNow ||
            e.HistoryClicksOnCampaignUser.ExpiredAt.Value < DateTime.UtcNow ||
            e.HistoryClicksOnLinksUser.ExpiredAt.Value < DateTime.UtcNow ||
            e.HistoryClicksOnSharesUser.ExpiredAt.Value < DateTime.UtcNow ||
            e.HistoryEarnByCountriesUser.ExpiredAt.Value < DateTime.UtcNow ||
            e.HistoryEarnOnLinksUser.ExpiredAt.Value < DateTime.UtcNow ||
            e.HistorySharedByUsersUser.ExpiredAt.Value < DateTime.UtcNow
        )
        .Select(e => e.Id)
        .Distinct()
        .ToListAsync();
        foreach (var userId in usersList)
        {
            _userUpdater.Send(new UpdateUserMessage() { Id = userId });
        }

        List<Guid> campaignsList = await _db.Campaigns
        .Where(e =>
        e.ClicksLastWeekOnCampaign.ExpiredAt.Value < DateTime.UtcNow ||
        e.ClicksTodayOnCampaign.ExpiredAt.Value < DateTime.UtcNow ||
        e.HistoryClicksByCountriesOnCampaign.ExpiredAt.Value < DateTime.UtcNow ||
        e.HistoryClicksOnCampaign.ExpiredAt.Value < DateTime.UtcNow ||
        e.HistorySharedByUsersOnCampaign.ExpiredAt.Value < DateTime.UtcNow ||
        e.HistorySharedOnCampaign.ExpiredAt.Value < DateTime.UtcNow ||
        e.SharedLastWeekOnCampaign.ExpiredAt.Value < DateTime.UtcNow ||
        e.SharedTodayOnCampaignModel.ExpiredAt.Value < DateTime.UtcNow
        )
        .Select(e => e.Id)
        .Distinct()
        .ToListAsync();

        foreach (var campaignId in campaignsList)
        {
            _campaignUpdater.Send(new UpdateCampaignMessage() { Id = campaignId });
        }

        List<Guid> linksList = await _db.Links
        .Where(e =>
        e.ClicksLastWeekOnLink.ExpiredAt.Value < DateTime.UtcNow ||
        e.ClicksTodayOnLink.ExpiredAt.Value < DateTime.UtcNow ||
        e.EarnLastWeekOnLink.ExpiredAt.Value < DateTime.UtcNow ||
        e.EarnTodayOnLink.ExpiredAt.Value < DateTime.UtcNow ||
        e.HistoryClicksByCountriesOnLink.ExpiredAt.Value < DateTime.UtcNow ||
        e.HistoryClicksOnLink.ExpiredAt.Value < DateTime.UtcNow ||
        e.HistoryEarnByCountriesOnLink.ExpiredAt.Value < DateTime.UtcNow ||
        e.HistoryEarnOnLink.ExpiredAt.Value < DateTime.UtcNow
        )
        .Select(e => e.Id)
        .Distinct()
        .ToListAsync();

        foreach (var linkId in linksList)
        {
            _linkUpdater.Send(new UpdateLinkMessage() { Id = linkId });
        }

    }


}