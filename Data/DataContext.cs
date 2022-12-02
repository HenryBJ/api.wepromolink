using Microsoft.EntityFrameworkCore;
using Polly;
using WePromoLink.Models;

namespace WePromoLink.Data;

public class DataContext:DbContext
{

    public virtual DbSet<AffiliateLinkModel> AffiliateLinks { get; set; }
    public virtual DbSet<EmailModel> Emails { get; set; }
    public virtual DbSet<HitAffiliateModel> HitAffiliates { get; set; }
    public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }
    public virtual DbSet<SponsoredLinkModel> SponsoredLinks { get; set; }
    
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<AffiliateLinkModel>(entity=>
        {
            entity.HasKey(e=>e.Id);
            entity.Property(e=>e.Available).HasPrecision(10,8);
            entity.Property(e=>e.Threshold).HasPrecision(10,8);
            entity.Property(e=>e.TotalEarned).HasPrecision(10,8);
            entity.Property(e=>e.TotalWithdraw).HasPrecision(10,8);

        });

        builder.Entity<EmailModel>(entity=>
        {
            entity.HasKey(e=>e.Id);
        });

        builder.Entity<HitAffiliateModel>(entity=>
        {
            entity.HasKey(e=>e.Id);
        });

        builder.Entity<PaymentTransaction>(entity=>
        {
            entity.HasKey(e=>e.Id);
            entity.Property(e=>e.Amount).HasPrecision(10,8);
        });

        builder.Entity<SponsoredLinkModel>(entity=>
        {
            entity.HasKey(e=>e.Id);
            entity.Property(e=>e.Budget).HasPrecision(10,8);
            entity.Property(e=>e.EPM).HasPrecision(10,8);
        });
    }    

    public void MigrateDB()
    {
        Policy
        .Handle<Exception>()
        .WaitAndRetry(10,r=>TimeSpan.FromSeconds(20),onRetry:(ex,ts)=>
        {
            Console.WriteLine($"Migration error: {ex.Message}");
        })
        .Execute(()=>Database.Migrate());
    }

}