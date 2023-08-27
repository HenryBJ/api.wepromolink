using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;

namespace WePromoLink.Controller.Tasks;

public class LongTimeCheckTask
{
    private readonly IServiceScopeFactory _fac;
    private readonly ILogger<LongTimeCheckTask> _logger;

    public LongTimeCheckTask(IServiceScopeFactory fac, ILogger<LongTimeCheckTask> logger)
    {
        _fac = fac;
        _logger = logger;
    }

    public async Task Update()
    {
        // using var scope = _fac.CreateScope();
        // var _db = scope.ServiceProvider.GetRequiredService<DataContext>();

        // DateTime dateAgo = DateTime.UtcNow.AddDays(-31);
        
        // var list = await _db.Hits.Where(e=>e.LastHitAt <= dateAgo)
        // .Include(e=>e.Link)
        // .ThenInclude(e=>e.Campaign)
        // .ThenInclude(e=>e.User)
        // .Select(e=> new 
        // {
        //     OwnerUserId = e.Link.Campaign.User.Id,
        //     LinkCreatorUserId = e.Link.UserModelId,
        //     CampaignId = e.Link.Campaign.Id,
        //     LinkId = e.Link.Id,
        //     OwnerName =  e.Link.Campaign.User.Fullname,
        //     CampaignName = e.Link.Campaign.Title,
        //     LastClicked = e.LastHitAt
        // })
        // .Distinct()
        // .ToListAsync();

    }


}