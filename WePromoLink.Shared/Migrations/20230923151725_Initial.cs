using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WePromoLink.Shared.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GenericEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Continent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(20,14)", precision: 20, scale: 14, nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(20,14)", precision: 20, scale: 14, nullable: true),
                    CountryFlagUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Original = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginalWidth = table.Column<int>(type: "int", nullable: false),
                    OriginalHeight = table.Column<int>(type: "int", nullable: false),
                    OriginalAspectRatio = table.Column<double>(type: "float", nullable: false),
                    Compressed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompressedWidth = table.Column<int>(type: "int", nullable: false),
                    CompressedHeight = table.Column<int>(type: "int", nullable: false),
                    CompressedAspectRatio = table.Column<double>(type: "float", nullable: false),
                    Medium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediumWidth = table.Column<int>(type: "int", nullable: false),
                    MediumHeight = table.Column<int>(type: "int", nullable: false),
                    MediumAspectRatio = table.Column<double>(type: "float", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThumbnailWidth = table.Column<int>(type: "int", nullable: false),
                    ThumbnailHeight = table.Column<int>(type: "int", nullable: false),
                    ThumbnailAspectRatio = table.Column<double>(type: "float", nullable: false),
                    Bound = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    MonthlyProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnualyProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyPaymantLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnnualyPaymantLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monthly = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    Annually = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoolValue = table.Column<bool>(type: "bit", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionPlanModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionFeatures_SubscriptionPlans_SubscriptionPlanModelId",
                        column: x => x.SubscriptionPlanModelId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionPlanModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NextPayment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastPayment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CanceledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_SubscriptionPlans_SubscriptionPlanModelId",
                        column: x => x.SubscriptionPlanModelId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirebaseId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    IsSubscribed = table.Column<bool>(type: "bit", nullable: false),
                    BlockageCause = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Subscriptions_SubscriptionModelId",
                        column: x => x.SubscriptionModelId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AffiliatedUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    TotalPayments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberPayments = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliatedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AffiliatedUsers_Users_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AffiliatedUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AffiliatePrograms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    AffiliateLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Affiliates = table.Column<int>(type: "int", nullable: false),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MRR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OTR = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliatePrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AffiliatePrograms_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Availables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Availables_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BitcoinBillings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BitcoinBillings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BitcoinBillings_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageDataModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Budget = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    EPM = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    LastClick = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastShared = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaigns_ImageDatas_ImageDataModelId",
                        column: x => x.ImageDataModelId,
                        principalTable: "ImageDatas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Campaigns_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClickLastWeekOnLinksUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClickLastWeekOnLinksUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClickLastWeekOnLinksUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClicksLastWeekOnCampaignUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClicksLastWeekOnCampaignUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClicksLastWeekOnCampaignUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClicksTodayOnCampaignUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClicksTodayOnCampaignUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClicksTodayOnCampaignUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClicksTodayOnLinksUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClicksTodayOnLinksUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClicksTodayOnLinksUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EarnLastWeekUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EarnLastWeekUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EarnLastWeekUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EarnTodayUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EarnTodayUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EarnTodayUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryClicksByCountriesOnCampaignUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<int>(type: "int", nullable: false),
                    X1 = table.Column<int>(type: "int", nullable: false),
                    X2 = table.Column<int>(type: "int", nullable: false),
                    X3 = table.Column<int>(type: "int", nullable: false),
                    X4 = table.Column<int>(type: "int", nullable: false),
                    X5 = table.Column<int>(type: "int", nullable: false),
                    X6 = table.Column<int>(type: "int", nullable: false),
                    X7 = table.Column<int>(type: "int", nullable: false),
                    X8 = table.Column<int>(type: "int", nullable: false),
                    X9 = table.Column<int>(type: "int", nullable: false),
                    L0 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryClicksByCountriesOnCampaignUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryClicksByCountriesOnCampaignUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryClicksByCountriesOnLinkUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<int>(type: "int", nullable: false),
                    X1 = table.Column<int>(type: "int", nullable: false),
                    X2 = table.Column<int>(type: "int", nullable: false),
                    X3 = table.Column<int>(type: "int", nullable: false),
                    X4 = table.Column<int>(type: "int", nullable: false),
                    X5 = table.Column<int>(type: "int", nullable: false),
                    X6 = table.Column<int>(type: "int", nullable: false),
                    X7 = table.Column<int>(type: "int", nullable: false),
                    X8 = table.Column<int>(type: "int", nullable: false),
                    X9 = table.Column<int>(type: "int", nullable: false),
                    L0 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryClicksByCountriesOnLinkUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryClicksByCountriesOnLinkUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryClicksOnCampaignUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<int>(type: "int", nullable: false),
                    X1 = table.Column<int>(type: "int", nullable: false),
                    X2 = table.Column<int>(type: "int", nullable: false),
                    X3 = table.Column<int>(type: "int", nullable: false),
                    X4 = table.Column<int>(type: "int", nullable: false),
                    X5 = table.Column<int>(type: "int", nullable: false),
                    X6 = table.Column<int>(type: "int", nullable: false),
                    X7 = table.Column<int>(type: "int", nullable: false),
                    X8 = table.Column<int>(type: "int", nullable: false),
                    X9 = table.Column<int>(type: "int", nullable: false),
                    L0 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L2 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L3 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L4 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L5 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L6 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L7 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L8 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L9 = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryClicksOnCampaignUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryClicksOnCampaignUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryClicksOnLinksUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<int>(type: "int", nullable: false),
                    X1 = table.Column<int>(type: "int", nullable: false),
                    X2 = table.Column<int>(type: "int", nullable: false),
                    X3 = table.Column<int>(type: "int", nullable: false),
                    X4 = table.Column<int>(type: "int", nullable: false),
                    X5 = table.Column<int>(type: "int", nullable: false),
                    X6 = table.Column<int>(type: "int", nullable: false),
                    X7 = table.Column<int>(type: "int", nullable: false),
                    X8 = table.Column<int>(type: "int", nullable: false),
                    X9 = table.Column<int>(type: "int", nullable: false),
                    L0 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L2 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L3 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L4 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L5 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L6 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L7 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L8 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L9 = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryClicksOnLinksUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryClicksOnLinksUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryClicksOnSharesUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<int>(type: "int", nullable: false),
                    X1 = table.Column<int>(type: "int", nullable: false),
                    X2 = table.Column<int>(type: "int", nullable: false),
                    X3 = table.Column<int>(type: "int", nullable: false),
                    X4 = table.Column<int>(type: "int", nullable: false),
                    X5 = table.Column<int>(type: "int", nullable: false),
                    X6 = table.Column<int>(type: "int", nullable: false),
                    X7 = table.Column<int>(type: "int", nullable: false),
                    X8 = table.Column<int>(type: "int", nullable: false),
                    X9 = table.Column<int>(type: "int", nullable: false),
                    L0 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L2 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L3 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L4 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L5 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L6 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L7 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L8 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L9 = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryClicksOnSharesUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryClicksOnSharesUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEarnByCountriesUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X1 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X2 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X3 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X4 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X5 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X6 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X7 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X8 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X9 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    L0 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEarnByCountriesUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEarnByCountriesUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEarnOnLinksUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X1 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X2 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X3 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X4 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X5 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X6 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X7 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X8 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X9 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    L0 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L2 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L3 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L4 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L5 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L6 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L7 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L8 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L9 = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEarnOnLinksUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEarnOnLinksUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistorySharedByUsersUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<int>(type: "int", nullable: false),
                    X1 = table.Column<int>(type: "int", nullable: false),
                    X2 = table.Column<int>(type: "int", nullable: false),
                    X3 = table.Column<int>(type: "int", nullable: false),
                    X4 = table.Column<int>(type: "int", nullable: false),
                    X5 = table.Column<int>(type: "int", nullable: false),
                    X6 = table.Column<int>(type: "int", nullable: false),
                    X7 = table.Column<int>(type: "int", nullable: false),
                    X8 = table.Column<int>(type: "int", nullable: false),
                    X9 = table.Column<int>(type: "int", nullable: false),
                    L0 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorySharedByUsersUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorySharedByUsersUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lockeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lockeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lockeds_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyPages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Visited = table.Column<int>(type: "int", nullable: false),
                    Conversion = table.Column<int>(type: "int", nullable: false),
                    CallOfAction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QRUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageHeaderUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyPages_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayoutStats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayoutStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayoutStats_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Privacies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShowEmailOnProfile = table.Column<bool>(type: "bit", nullable: false),
                    ShowSocialsOnProfile = table.Column<bool>(type: "bit", nullable: false),
                    ShowCampaignsOnProfile = table.Column<bool>(type: "bit", nullable: false),
                    ShowLinksOnProfile = table.Column<bool>(type: "bit", nullable: false),
                    ShowProfitOnProfile = table.Column<bool>(type: "bit", nullable: false),
                    ShowAffiliateLinkOnProfile = table.Column<bool>(type: "bit", nullable: false),
                    ShowQRUrlOnProfile = table.Column<bool>(type: "bit", nullable: false),
                    PublicMyPage = table.Column<bool>(type: "bit", nullable: false),
                    ShowAffiliateLinkOnMyPage = table.Column<bool>(type: "bit", nullable: false),
                    ShowCallOfActionOnMyPage = table.Column<bool>(type: "bit", nullable: false),
                    ShowLinksOnMyPage = table.Column<bool>(type: "bit", nullable: false),
                    ShowSocialsOnMyPage = table.Column<bool>(type: "bit", nullable: false),
                    UseMyPageTemplate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privacies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Privacies_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageHeaderUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Social = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MyPageId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profits_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampaignLanguages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampaignClickedOnNotification = table.Column<bool>(type: "bit", nullable: false),
                    CampaignClickedOnRealTime = table.Column<bool>(type: "bit", nullable: false),
                    CampaignClickedOnEmail = table.Column<bool>(type: "bit", nullable: false),
                    CampaignCreatedOnNotification = table.Column<bool>(type: "bit", nullable: false),
                    CampaignCreatedOnRealTime = table.Column<bool>(type: "bit", nullable: false),
                    CampaignCreatedOnEmail = table.Column<bool>(type: "bit", nullable: false),
                    CampaignDeletedOnNotification = table.Column<bool>(type: "bit", nullable: false),
                    CampaignDeletedOnRealTime = table.Column<bool>(type: "bit", nullable: false),
                    CampaignDeletedOnEmail = table.Column<bool>(type: "bit", nullable: false),
                    CampaignEditedOnNotification = table.Column<bool>(type: "bit", nullable: false),
                    CampaignEditedOnRealTime = table.Column<bool>(type: "bit", nullable: false),
                    CampaignEditedOnEmail = table.Column<bool>(type: "bit", nullable: false),
                    CampaignPublishedOnNotification = table.Column<bool>(type: "bit", nullable: false),
                    CampaignPublishedOnRealTime = table.Column<bool>(type: "bit", nullable: false),
                    CampaignPublishedOnEmail = table.Column<bool>(type: "bit", nullable: false),
                    CampaignSharedOnNotification = table.Column<bool>(type: "bit", nullable: false),
                    CampaignSharedOnRealTime = table.Column<bool>(type: "bit", nullable: false),
                    CampaignSharedOnEmail = table.Column<bool>(type: "bit", nullable: false),
                    CampaignSoldOutOnNotification = table.Column<bool>(type: "bit", nullable: false),
                    CampaignSoldOutOnRealTime = table.Column<bool>(type: "bit", nullable: false),
                    CampaignSoldOutOnEmail = table.Column<bool>(type: "bit", nullable: false),
                    CampaignUnPublishedOnNotification = table.Column<bool>(type: "bit", nullable: false),
                    CampaignUnPublishedOnRealTime = table.Column<bool>(type: "bit", nullable: false),
                    CampaignUnPublishedOnEmail = table.Column<bool>(type: "bit", nullable: false),
                    LinkClickedOnNotification = table.Column<bool>(type: "bit", nullable: false),
                    LinkClickedOnRealTime = table.Column<bool>(type: "bit", nullable: false),
                    LinkClickedOnEmail = table.Column<bool>(type: "bit", nullable: false),
                    LinkCreatedOnNotification = table.Column<bool>(type: "bit", nullable: false),
                    LinkCreatedOnRealTime = table.Column<bool>(type: "bit", nullable: false),
                    LinkCreatedOnEmail = table.Column<bool>(type: "bit", nullable: false),
                    HitGeoLocalizedSuccessOnNotification = table.Column<bool>(type: "bit", nullable: false),
                    HitGeoLocalizedSuccessOnRealTime = table.Column<bool>(type: "bit", nullable: false),
                    HitGeoLocalizedSuccessOnEmail = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SharedLastWeekUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedLastWeekUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedLastWeekUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SharedTodayUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedTodayUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedTodayUsers_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StripeBillings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeBillings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeBillings_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbuseReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbuseReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbuseReports_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AbuseReports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClicksLastWeekOnCampaigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampaignModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClicksLastWeekOnCampaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClicksLastWeekOnCampaigns_Campaigns_CampaignModelId",
                        column: x => x.CampaignModelId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClicksTodayOnCampaigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampaignModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClicksTodayOnCampaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClicksTodayOnCampaigns_Campaigns_CampaignModelId",
                        column: x => x.CampaignModelId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryClicksByCountriesOnCampaigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampaignModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<int>(type: "int", nullable: false),
                    X1 = table.Column<int>(type: "int", nullable: false),
                    X2 = table.Column<int>(type: "int", nullable: false),
                    X3 = table.Column<int>(type: "int", nullable: false),
                    X4 = table.Column<int>(type: "int", nullable: false),
                    X5 = table.Column<int>(type: "int", nullable: false),
                    X6 = table.Column<int>(type: "int", nullable: false),
                    X7 = table.Column<int>(type: "int", nullable: false),
                    X8 = table.Column<int>(type: "int", nullable: false),
                    X9 = table.Column<int>(type: "int", nullable: false),
                    L0 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryClicksByCountriesOnCampaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryClicksByCountriesOnCampaigns_Campaigns_CampaignModelId",
                        column: x => x.CampaignModelId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryClicksOnCampaigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampaignModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<int>(type: "int", nullable: false),
                    X1 = table.Column<int>(type: "int", nullable: false),
                    X2 = table.Column<int>(type: "int", nullable: false),
                    X3 = table.Column<int>(type: "int", nullable: false),
                    X4 = table.Column<int>(type: "int", nullable: false),
                    X5 = table.Column<int>(type: "int", nullable: false),
                    X6 = table.Column<int>(type: "int", nullable: false),
                    X7 = table.Column<int>(type: "int", nullable: false),
                    X8 = table.Column<int>(type: "int", nullable: false),
                    X9 = table.Column<int>(type: "int", nullable: false),
                    L0 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L2 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L3 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L4 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L5 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L6 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L7 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L8 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L9 = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryClicksOnCampaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryClicksOnCampaigns_Campaigns_CampaignModelId",
                        column: x => x.CampaignModelId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistorySharedByUsersOnCampaigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampaignModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<int>(type: "int", nullable: false),
                    X1 = table.Column<int>(type: "int", nullable: false),
                    X2 = table.Column<int>(type: "int", nullable: false),
                    X3 = table.Column<int>(type: "int", nullable: false),
                    X4 = table.Column<int>(type: "int", nullable: false),
                    X5 = table.Column<int>(type: "int", nullable: false),
                    X6 = table.Column<int>(type: "int", nullable: false),
                    X7 = table.Column<int>(type: "int", nullable: false),
                    X8 = table.Column<int>(type: "int", nullable: false),
                    X9 = table.Column<int>(type: "int", nullable: false),
                    L0 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorySharedByUsersOnCampaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorySharedByUsersOnCampaigns_Campaigns_CampaignModelId",
                        column: x => x.CampaignModelId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistorySharedOnCampaigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampaignModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<int>(type: "int", nullable: false),
                    X1 = table.Column<int>(type: "int", nullable: false),
                    X2 = table.Column<int>(type: "int", nullable: false),
                    X3 = table.Column<int>(type: "int", nullable: false),
                    X4 = table.Column<int>(type: "int", nullable: false),
                    X5 = table.Column<int>(type: "int", nullable: false),
                    X6 = table.Column<int>(type: "int", nullable: false),
                    X7 = table.Column<int>(type: "int", nullable: false),
                    X8 = table.Column<int>(type: "int", nullable: false),
                    X9 = table.Column<int>(type: "int", nullable: false),
                    L0 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L2 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L3 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L4 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L5 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L6 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L7 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L8 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L9 = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorySharedOnCampaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorySharedOnCampaigns_Campaigns_CampaignModelId",
                        column: x => x.CampaignModelId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampaignModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastClick = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Links_Campaigns_CampaignModelId",
                        column: x => x.CampaignModelId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Links_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SharedLastWeekOnCampaigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampaignModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedLastWeekOnCampaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedLastWeekOnCampaigns_Campaigns_CampaignModelId",
                        column: x => x.CampaignModelId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SharedTodayOnCampaigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampaignModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedTodayOnCampaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedTodayOnCampaigns_Campaigns_CampaignModelId",
                        column: x => x.CampaignModelId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClicksLastWeekOnLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClicksLastWeekOnLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClicksLastWeekOnLinks_Links_LinkModelId",
                        column: x => x.LinkModelId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClicksTodayOnLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClicksTodayOnLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClicksTodayOnLinks_Links_LinkModelId",
                        column: x => x.LinkModelId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EarnLastWeekOnLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EarnLastWeekOnLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EarnLastWeekOnLinks_Links_LinkModelId",
                        column: x => x.LinkModelId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EarnTodayOnLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EarnTodayOnLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EarnTodayOnLinks_Links_LinkModelId",
                        column: x => x.LinkModelId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryClicksByCountriesOnLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<int>(type: "int", nullable: false),
                    X1 = table.Column<int>(type: "int", nullable: false),
                    X2 = table.Column<int>(type: "int", nullable: false),
                    X3 = table.Column<int>(type: "int", nullable: false),
                    X4 = table.Column<int>(type: "int", nullable: false),
                    X5 = table.Column<int>(type: "int", nullable: false),
                    X6 = table.Column<int>(type: "int", nullable: false),
                    X7 = table.Column<int>(type: "int", nullable: false),
                    X8 = table.Column<int>(type: "int", nullable: false),
                    X9 = table.Column<int>(type: "int", nullable: false),
                    L0 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryClicksByCountriesOnLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryClicksByCountriesOnLinks_Links_LinkModelId",
                        column: x => x.LinkModelId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryClicksOnLinkModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<int>(type: "int", nullable: false),
                    X1 = table.Column<int>(type: "int", nullable: false),
                    X2 = table.Column<int>(type: "int", nullable: false),
                    X3 = table.Column<int>(type: "int", nullable: false),
                    X4 = table.Column<int>(type: "int", nullable: false),
                    X5 = table.Column<int>(type: "int", nullable: false),
                    X6 = table.Column<int>(type: "int", nullable: false),
                    X7 = table.Column<int>(type: "int", nullable: false),
                    X8 = table.Column<int>(type: "int", nullable: false),
                    X9 = table.Column<int>(type: "int", nullable: false),
                    L0 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L2 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L3 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L4 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L5 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L6 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L7 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L8 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L9 = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryClicksOnLinkModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryClicksOnLinkModels_Links_LinkModelId",
                        column: x => x.LinkModelId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEarnByCountriesOnLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X1 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X2 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X3 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X4 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X5 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X6 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X7 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X8 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X9 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    L0 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    L9 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEarnByCountriesOnLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEarnByCountriesOnLinks_Links_LinkModelId",
                        column: x => x.LinkModelId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEarnOnLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X0 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X1 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X2 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X3 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X4 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X5 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X6 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X7 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X8 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    X9 = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    L0 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L2 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L3 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L4 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L5 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L6 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L7 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L8 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    L9 = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEarnOnLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEarnOnLinks_Links_LinkModelId",
                        column: x => x.LinkModelId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeoDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsGeolocated = table.Column<bool>(type: "bit", nullable: false),
                    FirstHitAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastHitAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Counter = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeolocatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hits_GeoDatas_GeoDataId",
                        column: x => x.GeoDataId,
                        principalTable: "GeoDatas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hits_Links_LinkModelId",
                        column: x => x.LinkModelId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LinkModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CampaignModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Campaigns_CampaignModelId",
                        column: x => x.CampaignModelId,
                        principalTable: "Campaigns",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Links_LinkModelId",
                        column: x => x.LinkModelId,
                        principalTable: "Links",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "Annually", "AnnualyPaymantLink", "AnnualyProductId", "Discount", "ExternalId", "Level", "Metadata", "Monthly", "MonthlyPaymantLink", "MonthlyProductId", "Order", "PaymentMethod", "Tag", "Title" },
                values: new object[] { new Guid("8ae6798e-b144-432f-a78a-3cf552b46eb4"), 0m, "", "", 0m, "SOks5GRn4l0U", 1, null, 0m, "", "", 1, "bitcoin", "", "Community" });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "Annually", "AnnualyPaymantLink", "AnnualyProductId", "Discount", "ExternalId", "Level", "Metadata", "Monthly", "MonthlyPaymantLink", "MonthlyProductId", "Order", "PaymentMethod", "Tag", "Title" },
                values: new object[] { new Guid("e60952b8-ee79-43f9-8df2-e886d5cbdc37"), 244m, "https://buy.stripe.com/test_8wM8Ao6iAfFD3m0aEF", "prod_NpuAflpfqloJa9", 15m, "0B0wpjmqQIlD", 2, null, 24m, "https://buy.stripe.com/test_eVa9Es8qI0KJaOs7ss", "prod_NpnKrvEvvWJtqG", 2, "stripe", "Popular", "Professional" });

            migrationBuilder.InsertData(
                table: "SubscriptionFeatures",
                columns: new[] { "Id", "BoolValue", "Name", "SubscriptionPlanModelId", "Value" },
                values: new object[] { new Guid("721d6c59-3f1d-4489-8761-5db7e50ba83e"), false, "Contain ads", new Guid("e60952b8-ee79-43f9-8df2-e886d5cbdc37"), null });

            migrationBuilder.InsertData(
                table: "SubscriptionFeatures",
                columns: new[] { "Id", "BoolValue", "Name", "SubscriptionPlanModelId", "Value" },
                values: new object[] { new Guid("b893be68-4a00-41e3-83ce-dd8da502f46d"), true, "Contain ads", new Guid("8ae6798e-b144-432f-a78a-3cf552b46eb4"), null });

            migrationBuilder.CreateIndex(
                name: "IX_AbuseReports_CampaignId",
                table: "AbuseReports",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_AbuseReports_UserId",
                table: "AbuseReports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AffiliatedUsers_ParentId",
                table: "AffiliatedUsers",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AffiliatedUsers_UserModelId",
                table: "AffiliatedUsers",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AffiliatePrograms_UserModelId",
                table: "AffiliatePrograms",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Availables_UserModelId",
                table: "Availables",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BitcoinBillings_UserModelId",
                table: "BitcoinBillings",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserModelId",
                table: "Budgets",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_ExternalId",
                table: "Campaigns",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_ImageDataModelId",
                table: "Campaigns",
                column: "ImageDataModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_UserModelId",
                table: "Campaigns",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ClickLastWeekOnLinksUsers_UserModelId",
                table: "ClickLastWeekOnLinksUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClicksLastWeekOnCampaigns_CampaignModelId",
                table: "ClicksLastWeekOnCampaigns",
                column: "CampaignModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClicksLastWeekOnCampaignUsers_UserModelId",
                table: "ClicksLastWeekOnCampaignUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClicksLastWeekOnLinks_LinkModelId",
                table: "ClicksLastWeekOnLinks",
                column: "LinkModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClicksTodayOnCampaigns_CampaignModelId",
                table: "ClicksTodayOnCampaigns",
                column: "CampaignModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClicksTodayOnCampaignUsers_UserModelId",
                table: "ClicksTodayOnCampaignUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClicksTodayOnLinks_LinkModelId",
                table: "ClicksTodayOnLinks",
                column: "LinkModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClicksTodayOnLinksUsers_UserModelId",
                table: "ClicksTodayOnLinksUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EarnLastWeekOnLinks_LinkModelId",
                table: "EarnLastWeekOnLinks",
                column: "LinkModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EarnLastWeekUsers_UserModelId",
                table: "EarnLastWeekUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EarnTodayOnLinks_LinkModelId",
                table: "EarnTodayOnLinks",
                column: "LinkModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EarnTodayUsers_UserModelId",
                table: "EarnTodayUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryClicksByCountriesOnCampaigns_CampaignModelId",
                table: "HistoryClicksByCountriesOnCampaigns",
                column: "CampaignModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryClicksByCountriesOnCampaignUsers_UserModelId",
                table: "HistoryClicksByCountriesOnCampaignUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryClicksByCountriesOnLinks_LinkModelId",
                table: "HistoryClicksByCountriesOnLinks",
                column: "LinkModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryClicksByCountriesOnLinkUsers_UserModelId",
                table: "HistoryClicksByCountriesOnLinkUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryClicksOnCampaigns_CampaignModelId",
                table: "HistoryClicksOnCampaigns",
                column: "CampaignModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryClicksOnCampaignUsers_UserModelId",
                table: "HistoryClicksOnCampaignUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryClicksOnLinkModels_LinkModelId",
                table: "HistoryClicksOnLinkModels",
                column: "LinkModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryClicksOnLinksUsers_UserModelId",
                table: "HistoryClicksOnLinksUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryClicksOnSharesUsers_UserModelId",
                table: "HistoryClicksOnSharesUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEarnByCountriesOnLinks_LinkModelId",
                table: "HistoryEarnByCountriesOnLinks",
                column: "LinkModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEarnByCountriesUsers_UserModelId",
                table: "HistoryEarnByCountriesUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEarnOnLinks_LinkModelId",
                table: "HistoryEarnOnLinks",
                column: "LinkModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEarnOnLinksUsers_UserModelId",
                table: "HistoryEarnOnLinksUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistorySharedByUsersOnCampaigns_CampaignModelId",
                table: "HistorySharedByUsersOnCampaigns",
                column: "CampaignModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistorySharedByUsersUsers_UserModelId",
                table: "HistorySharedByUsersUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistorySharedOnCampaigns_CampaignModelId",
                table: "HistorySharedOnCampaigns",
                column: "CampaignModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hits_GeoDataId",
                table: "Hits",
                column: "GeoDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Hits_LinkModelId",
                table: "Hits",
                column: "LinkModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_CampaignModelId",
                table: "Links",
                column: "CampaignModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_ExternalId",
                table: "Links",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_UserModelId",
                table: "Links",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Lockeds_UserModelId",
                table: "Lockeds",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyPages_UserModelId",
                table: "MyPages",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ExternalId",
                table: "Notifications",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserModelId",
                table: "Notifications",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_CampaignModelId",
                table: "PaymentTransactions",
                column: "CampaignModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_ExternalId",
                table: "PaymentTransactions",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_LinkModelId",
                table: "PaymentTransactions",
                column: "LinkModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_UserModelId",
                table: "PaymentTransactions",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PayoutStats_UserModelId",
                table: "PayoutStats",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Privacies_UserModelId",
                table: "Privacies",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserModelId",
                table: "Profiles",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profits_UserModelId",
                table: "Profits",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Settings_UserModelId",
                table: "Settings",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SharedLastWeekOnCampaigns_CampaignModelId",
                table: "SharedLastWeekOnCampaigns",
                column: "CampaignModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SharedLastWeekUsers_UserModelId",
                table: "SharedLastWeekUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SharedTodayOnCampaigns_CampaignModelId",
                table: "SharedTodayOnCampaigns",
                column: "CampaignModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SharedTodayUsers_UserModelId",
                table: "SharedTodayUsers",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StripeBillings_UserModelId",
                table: "StripeBillings",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionFeatures_SubscriptionPlanModelId",
                table: "SubscriptionFeatures",
                column: "SubscriptionPlanModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_ExternalId",
                table: "SubscriptionPlans",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ExternalId",
                table: "Subscriptions",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriptionPlanModelId",
                table: "Subscriptions",
                column: "SubscriptionPlanModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ExternalId",
                table: "Users",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SubscriptionModelId",
                table: "Users",
                column: "SubscriptionModelId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbuseReports");

            migrationBuilder.DropTable(
                name: "AffiliatedUsers");

            migrationBuilder.DropTable(
                name: "AffiliatePrograms");

            migrationBuilder.DropTable(
                name: "Availables");

            migrationBuilder.DropTable(
                name: "BitcoinBillings");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "ClickLastWeekOnLinksUsers");

            migrationBuilder.DropTable(
                name: "ClicksLastWeekOnCampaigns");

            migrationBuilder.DropTable(
                name: "ClicksLastWeekOnCampaignUsers");

            migrationBuilder.DropTable(
                name: "ClicksLastWeekOnLinks");

            migrationBuilder.DropTable(
                name: "ClicksTodayOnCampaigns");

            migrationBuilder.DropTable(
                name: "ClicksTodayOnCampaignUsers");

            migrationBuilder.DropTable(
                name: "ClicksTodayOnLinks");

            migrationBuilder.DropTable(
                name: "ClicksTodayOnLinksUsers");

            migrationBuilder.DropTable(
                name: "EarnLastWeekOnLinks");

            migrationBuilder.DropTable(
                name: "EarnLastWeekUsers");

            migrationBuilder.DropTable(
                name: "EarnTodayOnLinks");

            migrationBuilder.DropTable(
                name: "EarnTodayUsers");

            migrationBuilder.DropTable(
                name: "GenericEvent");

            migrationBuilder.DropTable(
                name: "HistoryClicksByCountriesOnCampaigns");

            migrationBuilder.DropTable(
                name: "HistoryClicksByCountriesOnCampaignUsers");

            migrationBuilder.DropTable(
                name: "HistoryClicksByCountriesOnLinks");

            migrationBuilder.DropTable(
                name: "HistoryClicksByCountriesOnLinkUsers");

            migrationBuilder.DropTable(
                name: "HistoryClicksOnCampaigns");

            migrationBuilder.DropTable(
                name: "HistoryClicksOnCampaignUsers");

            migrationBuilder.DropTable(
                name: "HistoryClicksOnLinkModels");

            migrationBuilder.DropTable(
                name: "HistoryClicksOnLinksUsers");

            migrationBuilder.DropTable(
                name: "HistoryClicksOnSharesUsers");

            migrationBuilder.DropTable(
                name: "HistoryEarnByCountriesOnLinks");

            migrationBuilder.DropTable(
                name: "HistoryEarnByCountriesUsers");

            migrationBuilder.DropTable(
                name: "HistoryEarnOnLinks");

            migrationBuilder.DropTable(
                name: "HistoryEarnOnLinksUsers");

            migrationBuilder.DropTable(
                name: "HistorySharedByUsersOnCampaigns");

            migrationBuilder.DropTable(
                name: "HistorySharedByUsersUsers");

            migrationBuilder.DropTable(
                name: "HistorySharedOnCampaigns");

            migrationBuilder.DropTable(
                name: "Hits");

            migrationBuilder.DropTable(
                name: "Lockeds");

            migrationBuilder.DropTable(
                name: "MyPages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "PayoutStats");

            migrationBuilder.DropTable(
                name: "Privacies");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Profits");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "SharedLastWeekOnCampaigns");

            migrationBuilder.DropTable(
                name: "SharedLastWeekUsers");

            migrationBuilder.DropTable(
                name: "SharedTodayOnCampaigns");

            migrationBuilder.DropTable(
                name: "SharedTodayUsers");

            migrationBuilder.DropTable(
                name: "StripeBillings");

            migrationBuilder.DropTable(
                name: "SubscriptionFeatures");

            migrationBuilder.DropTable(
                name: "GeoDatas");

            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "ImageDatas");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");
        }
    }
}
