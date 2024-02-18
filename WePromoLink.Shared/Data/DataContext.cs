using Microsoft.EntityFrameworkCore;
using Polly;
using Scriban.Syntax;
using WePromoLink.Models;

namespace WePromoLink.Data;

public class DataContext : DbContext
{
    public virtual DbSet<StaticPageDataTemplateModel> StaticPageDataTemplates { get; set; }
    public virtual DbSet<StaticPageProductByResourceModel> StaticPageProductByResources { get; set; }
    public virtual DbSet<StaticPageModel> StaticPages { get; set; }
    public virtual DbSet<StaticPageProductByPageModel> StaticPageProductByPages { get; set; }
    public virtual DbSet<StaticPageProductModel> StaticPageProducts { get; set; }
    public virtual DbSet<StaticPageResourceModel> StaticPageResources { get; set; }
    public virtual DbSet<StaticPageWebsiteTemplateModel> StaticPageWebsiteTemplates { get; set; }

    public virtual DbSet<JoinWaitingListModel> JoinWaitingLists { get; set; }
    public virtual DbSet<SurveyQuestionModel> SurveyQuestions { get; set; }
    public virtual DbSet<SurveyAnswerModel> SurveyAnswers { get; set; }
    public virtual DbSet<SurveyDatapointModel> SurveyDatapoints { get; set; }
    public virtual DbSet<AffiliatedUserModel> AffiliatedUsers { get; set; }
    public virtual DbSet<ProfileModel> Profiles { get; set; }
    public virtual DbSet<MyPageModel> MyPages { get; set; }
    public virtual DbSet<SettingModel> Settings { get; set; }
    public virtual DbSet<PrivacyModel> Privacies { get; set; }
    public virtual DbSet<AffiliateModel> AffiliatePrograms { get; set; }
    public virtual DbSet<SubscriptionFeatureModel> SubscriptionFeatures { get; set; }
    public virtual DbSet<ImageDataModel> ImageDatas { get; set; }
    public virtual DbSet<GeoDataModel> GeoDatas { get; set; }
    public virtual DbSet<GenericEventModel> GenericEvent { get; set; }
    public virtual DbSet<StripeBillingMethod> StripeBillings { get; set; }
    public virtual DbSet<BitcoinBillingMethod> BitcoinBillings { get; set; }
    public virtual DbSet<SubscriptionModel> Subscriptions { get; set; }
    public virtual DbSet<SubscriptionPlanModel> SubscriptionPlans { get; set; }
    public virtual DbSet<NotificationModel> Notifications { get; set; }
    public virtual DbSet<LinkModel> Links { get; set; }
    public virtual DbSet<UserModel> Users { get; set; }
    public virtual DbSet<HitModel> Hits { get; set; }
    public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }
    public virtual DbSet<CampaignModel> Campaigns { get; set; }
    public virtual DbSet<AbuseReportModel> AbuseReports { get; set; }
    public virtual DbSet<AvailableModel> Availables { get; set; }
    public virtual DbSet<BudgetModel> Budgets { get; set; }
    public virtual DbSet<LockedModel> Lockeds { get; set; }
    public virtual DbSet<PayoutStatModel> PayoutStats { get; set; }
    public virtual DbSet<ProfitModel> Profits { get; set; }

    // Statistics for User
    public virtual DbSet<ClickLastWeekOnLinksUserModel> ClickLastWeekOnLinksUsers { get; set; }
    public virtual DbSet<ClicksTodayOnCampaignUserModel> ClicksTodayOnCampaignUsers { get; set; }
    public virtual DbSet<ClicksLastWeekOnCampaignUserModel> ClicksLastWeekOnCampaignUsers { get; set; }
    public virtual DbSet<ClicksTodayOnLinksUserModel> ClicksTodayOnLinksUsers { get; set; }
    public virtual DbSet<EarnLastWeekUserModel> EarnLastWeekUsers { get; set; }
    public virtual DbSet<EarnTodayUserModel> EarnTodayUsers { get; set; }
    public virtual DbSet<HistoryClicksByCountriesOnCampaignUserModel> HistoryClicksByCountriesOnCampaignUsers { get; set; }
    public virtual DbSet<HistoryClicksByCountriesOnLinkUserModel> HistoryClicksByCountriesOnLinkUsers { get; set; }
    public virtual DbSet<HistoryClicksOnCampaignUserModel> HistoryClicksOnCampaignUsers { get; set; }
    // public virtual DbSet<HistoryClicksOnLinkUserModel> HistoryClicksOnLinkUsers { get; set; }
    public virtual DbSet<HistoryClicksOnSharesUserModel> HistoryClicksOnSharesUsers { get; set; }
    public virtual DbSet<HistoryEarnByCountriesUserModel> HistoryEarnByCountriesUsers { get; set; }
    public virtual DbSet<HistoryEarnOnLinksUserModel> HistoryEarnOnLinksUsers { get; set; }
    public virtual DbSet<HistorySharedByUsersUserModel> HistorySharedByUsersUsers { get; set; }
    public virtual DbSet<HistoryClicksOnLinksUserModel> HistoryClicksOnLinksUsers { get; set; }
    public virtual DbSet<SharedLastWeekUserModel> SharedLastWeekUsers { get; set; }
    public virtual DbSet<SharedTodayUserModel> SharedTodayUsers { get; set; }

    // Statistics for Campaign
    public virtual DbSet<ClicksLastWeekOnCampaignModel> ClicksLastWeekOnCampaigns { get; set; }
    public virtual DbSet<ClicksTodayOnCampaignModel> ClicksTodayOnCampaigns { get; set; }
    public virtual DbSet<HistoryClicksByCountriesOnCampaignModel> HistoryClicksByCountriesOnCampaigns { get; set; }
    public virtual DbSet<HistoryClicksOnCampaignModel> HistoryClicksOnCampaigns { get; set; }
    public virtual DbSet<HistorySharedByUsersOnCampaignModel> HistorySharedByUsersOnCampaigns { get; set; }
    public virtual DbSet<HistorySharedOnCampaignModel> HistorySharedOnCampaigns { get; set; }
    public virtual DbSet<SharedLastWeekOnCampaignModel> SharedLastWeekOnCampaigns { get; set; }
    public virtual DbSet<SharedTodayOnCampaignModel> SharedTodayOnCampaigns { get; set; }

    // Statistics for Links
    public virtual DbSet<ClicksLastWeekOnLinkModel> ClicksLastWeekOnLinks { get; set; }
    public virtual DbSet<ClicksTodayOnLinkModel> ClicksTodayOnLinks { get; set; }
    public virtual DbSet<EarnLastWeekOnLinkModel> EarnLastWeekOnLinks { get; set; }
    public virtual DbSet<EarnTodayOnLinkModel> EarnTodayOnLinks { get; set; }
    public virtual DbSet<HistoryClicksByCountriesOnLinkModel> HistoryClicksByCountriesOnLinks { get; set; }
    public virtual DbSet<HistoryEarnByCountriesOnLinkModel> HistoryEarnByCountriesOnLinks { get; set; }
    public virtual DbSet<HistoryEarnOnLinkModel> HistoryEarnOnLinks { get; set; }
    public virtual DbSet<HistoryClicksOnLinkModel> HistoryClicksOnLinkModels { get; set; }




    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        Guid subPlanProfesional = Guid.NewGuid();
        Guid subPlanBasic = Guid.NewGuid();
        base.OnModelCreating(builder);

        builder.Entity<SubscriptionFeatureModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasData(new[]
            {
                new SubscriptionFeatureModel { Id=Guid.NewGuid(), Name="Campaigns",BoolValue = false,SubscriptionPlanModelId = subPlanBasic, Value="Unlimited"},
                new SubscriptionFeatureModel { Id=Guid.NewGuid(), Name="Links",BoolValue = false,SubscriptionPlanModelId = subPlanBasic, Value="Unlimited"},
                new SubscriptionFeatureModel { Id=Guid.NewGuid(), Name="Commission per click",BoolValue = false,SubscriptionPlanModelId = subPlanBasic, Value="U$0.01"},
                new SubscriptionFeatureModel { Id=Guid.NewGuid(), Name="Contain ads",BoolValue = true,SubscriptionPlanModelId = subPlanBasic, Value=""},
                new SubscriptionFeatureModel { Id=Guid.NewGuid(), Name="Campaigns",BoolValue = false,SubscriptionPlanModelId = subPlanProfesional, Value="Unlimited"},
                new SubscriptionFeatureModel { Id=Guid.NewGuid(), Name="Links",BoolValue = false,SubscriptionPlanModelId = subPlanProfesional, Value="Unlimited"},
                new SubscriptionFeatureModel { Id=Guid.NewGuid(), Name="Commission per click",BoolValue = false,SubscriptionPlanModelId = subPlanProfesional, Value="U$0.00"},
                new SubscriptionFeatureModel { Id=Guid.NewGuid(), Name="Contain ads",BoolValue = false,SubscriptionPlanModelId = subPlanProfesional, Value=""}
            });
        });

        var key1 = Guid.NewGuid();
        var key2 = Guid.NewGuid();
        var key3 = Guid.NewGuid();
        var key4 = Guid.NewGuid();
        var key5 = Guid.NewGuid();

        builder.Entity<SurveyQuestionModel>(entity =>
        {

            entity.HasKey(e => e.Id);
            entity.HasData(new[]
            {
                new SurveyQuestionModel {Id=key1, Group=1, Value="What motivates you to use a platform like WePromoLink?"},
                new SurveyQuestionModel {Id=key2, Group=2, Value="Which payment methods do you prefer for platform subscriptions and earnings withdrawals?"},
                new SurveyQuestionModel {Id=key3, Group=3, Value="Do you prefer a monthly subscription fee or paying a commission on earnings?"},
                new SurveyQuestionModel {Id=key4 ,Group=4, Value="How useful do you find the WePromoLink platform for your advertising needs?"},
                new SurveyQuestionModel {Id=key5, Group=5, Value="What additional features or improvements would you like to see on the WePromoLink platform?"},
            });
        });

        builder.Entity<SurveyAnswerModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasData(new[]
            {
                new SurveyAnswerModel { SurveyQuestionModelId=key1, Id=Guid.NewGuid(),Value = "Earning money through affiliate marketing" },
                new SurveyAnswerModel { SurveyQuestionModelId=key1, Id=Guid.NewGuid(),Value = "Promoting my products or services" },
                new SurveyAnswerModel { SurveyQuestionModelId=key1, Id=Guid.NewGuid(),Value = "Exploring new advertising opportunities" },
                new SurveyAnswerModel { SurveyQuestionModelId=key1, Id=Guid.NewGuid(),Value = "Connecting with other users and businesses" },
                new SurveyAnswerModel { SurveyQuestionModelId=key2, Id=Guid.NewGuid(),Value = "Credit/Debit card" },
                new SurveyAnswerModel { SurveyQuestionModelId=key2, Id=Guid.NewGuid(),Value = "PayPal" },
                new SurveyAnswerModel { SurveyQuestionModelId=key2, Id=Guid.NewGuid(),Value = "Stripe" },
                new SurveyAnswerModel { SurveyQuestionModelId=key2, Id=Guid.NewGuid(), Value = "Bank transfer" },
                new SurveyAnswerModel { SurveyQuestionModelId=key3, Id=Guid.NewGuid(),Value = "Monthly subscription fee" },
                new SurveyAnswerModel { SurveyQuestionModelId=key3, Id=Guid.NewGuid(),Value = "Commission on earnings" },
                new SurveyAnswerModel { SurveyQuestionModelId=key3, Id=Guid.NewGuid(),Value = "I'm not sure" },
                new SurveyAnswerModel { SurveyQuestionModelId=key4, Id=Guid.NewGuid(),Value = "Extremely useful" },
                new SurveyAnswerModel { SurveyQuestionModelId=key4, Id=Guid.NewGuid(),Value = "Very useful" },
                new SurveyAnswerModel { SurveyQuestionModelId=key4, Id=Guid.NewGuid(),Value = "Somewhat useful" },
                new SurveyAnswerModel { SurveyQuestionModelId=key4, Id=Guid.NewGuid(),Value = "Not very useful" },
                new SurveyAnswerModel { SurveyQuestionModelId=key4, Id=Guid.NewGuid(),Value = "Not useful at all" },
                new SurveyAnswerModel { SurveyQuestionModelId=key5, Id=Guid.NewGuid(),Value = "More campaign customization options" },
                new SurveyAnswerModel { SurveyQuestionModelId=key5, Id=Guid.NewGuid(),Value = "Advanced analytics and reporting" },
                new SurveyAnswerModel { SurveyQuestionModelId=key5, Id=Guid.NewGuid(),Value = "Integration with other advertising platforms" },
                new SurveyAnswerModel { SurveyQuestionModelId=key5, Id=Guid.NewGuid(),Value = "Improved user interface and navigation" },
                new SurveyAnswerModel { SurveyQuestionModelId=key5, Id=Guid.NewGuid(),Value = "I'm not sure" },

            });
        });

        builder.Entity<SurveyDatapointModel>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.SurveyQuestionModel)
            .WithMany(e => e.Datapoints)
            .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(e => e.SurveyAnswerModel)
            .WithMany(e => e.Datapoints)
            .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<JoinWaitingListModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<StaticPageDataTemplateModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<StaticPageModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<StaticPageProductByPageModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<StaticPageProductByResourceModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<StaticPageProductModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<StaticPageResourceModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<StaticPageWebsiteTemplateModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<AffiliatedUserModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.User)
            .WithMany(e => e.AffiliatedUsers)
            .HasForeignKey(e => e.UserModelId)
            .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<ProfileModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<MyPageModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<SettingModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<PrivacyModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<AffiliateModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<ImageDataModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<GeoDataModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Latitude).HasPrecision(20, 14);
            entity.Property(e => e.Longitude).HasPrecision(20, 14);
        });

        builder.Entity<UserModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ExternalId);
        });

        builder.Entity<SubscriptionModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ExternalId);
            entity.HasOne(s => s.User)
        .WithOne(u => u.Subscription)
        .HasForeignKey<UserModel>(u => u.SubscriptionModelId);
        });

        builder.Entity<StripeBillingMethod>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<BitcoinBillingMethod>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<AvailableModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).HasPrecision(10, 4);
        });

        builder.Entity<BudgetModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).HasPrecision(10, 4);
        });

        builder.Entity<LinkModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ExternalId);
            entity.HasOne(e => e.User)
        .WithMany(u => u.Links)
        .HasForeignKey(l => l.UserModelId)
        .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<SubscriptionPlanModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ExternalId);
            entity.Property(e => e.Monthly).HasPrecision(10, 4);
            entity.Property(e => e.Annually).HasPrecision(10, 4);
            entity.Property(e => e.Discount).HasPrecision(10, 4);

            entity.HasData(new[]{
                new SubscriptionPlanModel {
                    Id = subPlanBasic,
                    MonthlyPriceId = "price_1Ol0LGC26XBdqsojWuT9BKvb",
                    AnnualyPriceId = "",
                    Commission = 0.01m,
                    Annually = 0,
                    Monthly = 0,
                    Discount = 0,
                    Level = 1,
                    PaymentMethod = "stripe",
                    Tag = "",
                    Title = "Basic",
                    ExternalId = Nanoid.Nanoid.Generate(size:12),
                    Order = 2
                },
                new SubscriptionPlanModel {
                    Id = subPlanProfesional,
                    MonthlyPriceId = "price_1OkwiHC26XBdqsojpgor0QaV",
                    AnnualyPriceId = "",
                    Commission = 0,
                    Annually = 0,
                    Monthly = 4.99m,
                    Discount = 0,
                    Level = 2,
                    PaymentMethod = "stripe",
                    Tag = "Popular",
                    Title = "Professional",
                    ExternalId = Nanoid.Nanoid.Generate(size:12),
                    Order = 3
                },
            });
        });

        builder.Entity<NotificationModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ExternalId);
        });

        builder.Entity<UserModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<HitModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<PaymentTransaction>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ExternalId);
            entity.Property(e => e.Amount).HasPrecision(10, 4);

            entity.HasOne(p => p.Link)
            .WithMany(l => l.Transactions)
            .HasForeignKey(p => p.LinkModelId)
            .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(p => p.Campaign)
            .WithMany(l => l.Transactions)
            .HasForeignKey(p => p.CampaignModelId)
            .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(p => p.User)
            .WithMany(l => l.Transactions)
            .HasForeignKey(p => p.UserModelId)
            .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<CampaignModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ExternalId);
            entity.Property(e => e.Budget).HasPrecision(10, 4);
            entity.Property(e => e.EPM).HasPrecision(10, 4);
        });

        builder.Entity<AbuseReportModel>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        builder.Entity<GenericEventModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<ClicksLastWeekOnCampaignModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<ClicksLastWeekOnCampaignUserModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<ClicksLastWeekOnLinkModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<ClicksTodayOnCampaignModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<ClicksTodayOnCampaignUserModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<ClicksTodayOnLinkModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<ClicksTodayOnLinksUserModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<EarnLastWeekOnLinkModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).HasPrecision(10, 4);
        });
        builder.Entity<EarnLastWeekUserModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).HasPrecision(10, 4);
        });
        builder.Entity<EarnTodayOnLinkModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).HasPrecision(10, 4);
        });
        builder.Entity<EarnTodayUserModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).HasPrecision(10, 4);
        });
        builder.Entity<HistoryClicksByCountriesOnCampaignUserModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<HistoryClicksByCountriesOnCampaignModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<HistoryClicksByCountriesOnLinkModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<HistoryClicksByCountriesOnLinkUserModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<HistoryClicksOnCampaignModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<HistoryClicksOnCampaignUserModel>(entity => entity.HasKey(e => e.Id));
        // builder.Entity<HistoryClicksOnLinkUserModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<HistoryClicksOnSharesUserModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<HistoryEarnByCountriesOnLinkModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.X0).HasPrecision(10, 4);
            entity.Property(e => e.X1).HasPrecision(10, 4);
            entity.Property(e => e.X2).HasPrecision(10, 4);
            entity.Property(e => e.X3).HasPrecision(10, 4);
            entity.Property(e => e.X4).HasPrecision(10, 4);
            entity.Property(e => e.X5).HasPrecision(10, 4);
            entity.Property(e => e.X6).HasPrecision(10, 4);
            entity.Property(e => e.X7).HasPrecision(10, 4);
            entity.Property(e => e.X8).HasPrecision(10, 4);
            entity.Property(e => e.X9).HasPrecision(10, 4);
        });
        builder.Entity<HistoryEarnByCountriesUserModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.X0).HasPrecision(10, 4);
            entity.Property(e => e.X1).HasPrecision(10, 4);
            entity.Property(e => e.X2).HasPrecision(10, 4);
            entity.Property(e => e.X3).HasPrecision(10, 4);
            entity.Property(e => e.X4).HasPrecision(10, 4);
            entity.Property(e => e.X5).HasPrecision(10, 4);
            entity.Property(e => e.X6).HasPrecision(10, 4);
            entity.Property(e => e.X7).HasPrecision(10, 4);
            entity.Property(e => e.X8).HasPrecision(10, 4);
            entity.Property(e => e.X9).HasPrecision(10, 4);
        });
        builder.Entity<HistoryEarnOnLinkModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.X0).HasPrecision(10, 4);
            entity.Property(e => e.X1).HasPrecision(10, 4);
            entity.Property(e => e.X2).HasPrecision(10, 4);
            entity.Property(e => e.X3).HasPrecision(10, 4);
            entity.Property(e => e.X4).HasPrecision(10, 4);
            entity.Property(e => e.X5).HasPrecision(10, 4);
            entity.Property(e => e.X6).HasPrecision(10, 4);
            entity.Property(e => e.X7).HasPrecision(10, 4);
            entity.Property(e => e.X8).HasPrecision(10, 4);
            entity.Property(e => e.X9).HasPrecision(10, 4);
        });

        builder.Entity<HistoryEarnOnLinksUserModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.X0).HasPrecision(10, 4);
            entity.Property(e => e.X1).HasPrecision(10, 4);
            entity.Property(e => e.X2).HasPrecision(10, 4);
            entity.Property(e => e.X3).HasPrecision(10, 4);
            entity.Property(e => e.X4).HasPrecision(10, 4);
            entity.Property(e => e.X5).HasPrecision(10, 4);
            entity.Property(e => e.X6).HasPrecision(10, 4);
            entity.Property(e => e.X7).HasPrecision(10, 4);
            entity.Property(e => e.X8).HasPrecision(10, 4);
            entity.Property(e => e.X9).HasPrecision(10, 4);
        });

        builder.Entity<HistorySharedByUsersOnCampaignModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<HistorySharedByUsersUserModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<HistorySharedOnCampaignModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<HistoryClicksOnLinksUserModel>(entity => entity.HasKey(e => e.Id));
        // builder.Entity<HistoryClicksOnLinkUserModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<LockedModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).HasPrecision(10, 4);
        });
        builder.Entity<PayoutStatModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).HasPrecision(10, 4);
        });
        builder.Entity<ProfitModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value).HasPrecision(10, 4);
        });
        builder.Entity<SharedLastWeekOnCampaignModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<SharedLastWeekUserModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<SharedTodayOnCampaignModel>(entity => entity.HasKey(e => e.Id));
        builder.Entity<SharedTodayUserModel>(entity => entity.HasKey(e => e.Id));
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