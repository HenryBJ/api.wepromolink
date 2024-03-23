using Microsoft.EntityFrameworkCore;
using Polly;
using Scriban.Syntax;
using WePromoLink.Models;
using WePromoLink.Models.CRM;

namespace WePromoLink.Data;

public class CRMDataContext : DbContext
{
    public virtual DbSet<CampaignRunner> CampaignRunners { get; set; }
    public virtual DbSet<CampaignRunnerState> CampaignRunnerStates { get; set; }
    public virtual DbSet<LeadModel> Leads { get; set; }
    


    public CRMDataContext(DbContextOptions<CRMDataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

    }

       public void MigrateDB()
    {
        Policy
        .Handle<Exception>()
        .WaitAndRetry(10, r => TimeSpan.FromSeconds(20), onRetry: (ex, ts) =>
        {
            Console.WriteLine($"Migration error: {ex.Message}");
        })
        .Execute(() => Database.Migrate());
    }

}