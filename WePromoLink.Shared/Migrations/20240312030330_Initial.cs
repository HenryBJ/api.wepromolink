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
                    Bound = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JoinWaitingLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinWaitingLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticPageDataTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Json = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPageDataTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticPageProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Inventory = table.Column<int>(type: "int", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AffiliateProgram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commission = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AffiliateLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPageProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticPageResources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeMB = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPageResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticPageWebsiteTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPageWebsiteTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    MonthlyPriceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnualyPriceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monthly = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    Commission = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Annually = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    DepositFeePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WithdrawFeePercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Upgradeable = table.Column<bool>(type: "bit", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Group = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticPageProductByResources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaticPageProductModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StaticPageResourceModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPageProductByResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticPageProductByResources_StaticPageProducts_StaticPageProductModelId",
                        column: x => x.StaticPageProductModelId,
                        principalTable: "StaticPageProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaticPageProductByResources_StaticPageResources_StaticPageResourceModelId",
                        column: x => x.StaticPageResourceModelId,
                        principalTable: "StaticPageResources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaticPages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaticPageDataTemplateModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaticPageWebsiteTemplateModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticPages_StaticPageDataTemplates_StaticPageDataTemplateModelId",
                        column: x => x.StaticPageDataTemplateModelId,
                        principalTable: "StaticPageDataTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaticPages_StaticPageWebsiteTemplates_StaticPageWebsiteTemplateModelId",
                        column: x => x.StaticPageWebsiteTemplateModelId,
                        principalTable: "StaticPageWebsiteTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoolValue = table.Column<bool>(type: "bit", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
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
                    StripeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "SurveyAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurveyQuestionModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_SurveyQuestions_SurveyQuestionModelId",
                        column: x => x.SurveyQuestionModelId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaticPageProductByPages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaticPageProductModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StaticPageModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AffiliateClicks = table.Column<int>(type: "int", nullable: false),
                    BuyClicks = table.Column<int>(type: "int", nullable: false),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPageProductByPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticPageProductByPages_StaticPageProducts_StaticPageProductModelId",
                        column: x => x.StaticPageProductModelId,
                        principalTable: "StaticPageProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaticPageProductByPages_StaticPages_StaticPageModelId",
                        column: x => x.StaticPageModelId,
                        principalTable: "StaticPages",
                        principalColumn: "Id");
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
                    Budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Available = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                name: "SurveyDatapoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyAnswerModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyQuestionModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyDatapoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyDatapoints_SurveyAnswers_SurveyAnswerModelId",
                        column: x => x.SurveyAnswerModelId,
                        principalTable: "SurveyAnswers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SurveyDatapoints_SurveyQuestions_SurveyQuestionModelId",
                        column: x => x.SurveyQuestionModelId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id");
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
                    AffiliateLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Affiliates = table.Column<int>(type: "int", nullable: false),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                name: "MyPages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Visited = table.Column<int>(type: "int", nullable: false),
                    Conversion = table.Column<int>(type: "int", nullable: false),
                    CallOfAction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QRUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageHeaderUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    ImageHeaderUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Social = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyPageId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CampaignLanguages = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    SubscriptionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commission = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Payed = table.Column<bool>(type: "bit", nullable: false),
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
                    StripeTranferId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentProcessorFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LinkModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CampaignModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    AmountNet = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                columns: new[] { "Id", "Active", "Annually", "AnnualyPriceId", "Commission", "DepositFeePercent", "Disabled", "Discount", "ExternalId", "Level", "Metadata", "Monthly", "MonthlyPriceId", "Order", "PaymentMethod", "Tag", "Title", "Upgradeable", "WithdrawFeePercent" },
                values: new object[,]
                {
                    { new Guid("251343f4-52e2-45f6-b30f-f24a8c49741e"), true, 891m, "price_1OsdMCC26XBdqsojOddpYcZQ", 0m, 0m, false, 25m, "EIAvzngmir3W", 2, null, 99m, "price_1OsdMCC26XBdqsojSdyEGOUY", 3, "stripe", "Popular", "Professional", false, 0m },
                    { new Guid("51daa39e-0e84-4d94-a770-b769463850a8"), true, 0m, "", 0m, 10m, false, 0m, "x2Cc9RQucEzF", 1, null, 0m, "", 2, "stripe", "", "Basic", false, 10m }
                });

            migrationBuilder.InsertData(
                table: "SurveyQuestions",
                columns: new[] { "Id", "Group", "Value" },
                values: new object[,]
                {
                    { new Guid("481e23cc-274b-4e77-a615-4e7fe9cbb065"), 2, "Which payment methods do you prefer for platform subscriptions and earnings withdrawals?" },
                    { new Guid("527355e6-b2dd-4948-978d-cd27ca2f8e6a"), 1, "What motivates you to use a platform like WePromoLink?" },
                    { new Guid("53b02b05-e776-430a-9c3a-d2b7664eb13e"), 4, "How useful do you find the WePromoLink platform for your advertising needs?" },
                    { new Guid("a746e9b7-1205-417f-b52d-11c06237abad"), 5, "What additional features or improvements would you like to see on the WePromoLink platform?" },
                    { new Guid("b9537c05-0004-4063-852e-90de39f90c6e"), 3, "Do you prefer a monthly subscription fee or paying a commission on earnings?" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionFeatures",
                columns: new[] { "Id", "BoolValue", "Name", "Order", "SubscriptionPlanModelId", "Value" },
                values: new object[,]
                {
                    { new Guid("00c9aeda-0782-4d05-bd88-6ef9d12cca8a"), false, "Links", 2, new Guid("51daa39e-0e84-4d94-a770-b769463850a8"), "Unlimited" },
                    { new Guid("0708ecc6-a8e7-4fd4-912b-dd48a8c20574"), false, "Deposit fee", 4, new Guid("51daa39e-0e84-4d94-a770-b769463850a8"), "10%" },
                    { new Guid("0ecf7d20-ef7c-40da-b144-f26d71b60a78"), false, "Links", 2, new Guid("251343f4-52e2-45f6-b30f-f24a8c49741e"), "Unlimited" },
                    { new Guid("22b8e6cb-55dc-4932-98d3-37c6ffdb1ebc"), false, "Stripe Account", 3, new Guid("251343f4-52e2-45f6-b30f-f24a8c49741e"), "Express" },
                    { new Guid("a758b320-74b4-4f95-87d1-b9c88aa88fc3"), false, "Campaigns", 1, new Guid("251343f4-52e2-45f6-b30f-f24a8c49741e"), "Unlimited" },
                    { new Guid("aa4946d6-4547-414a-9b39-f104a4181fb8"), true, "Withdraw fee", 5, new Guid("51daa39e-0e84-4d94-a770-b769463850a8"), "10%" },
                    { new Guid("af6ca5f3-deaa-422d-af72-fd2467166569"), true, "Stripe Account", 3, new Guid("51daa39e-0e84-4d94-a770-b769463850a8"), "Standard" },
                    { new Guid("dcadc223-3611-4a85-b4fa-f22dd299b385"), false, "Deposit fee", 4, new Guid("251343f4-52e2-45f6-b30f-f24a8c49741e"), "0%" },
                    { new Guid("e36dc510-e30b-4b3b-bd56-d9df6bf5a49f"), false, "Campaigns", 1, new Guid("51daa39e-0e84-4d94-a770-b769463850a8"), "Unlimited" },
                    { new Guid("f417261e-ae11-40ae-84ff-9e9c2dda01d1"), false, "Withdraw fee", 5, new Guid("251343f4-52e2-45f6-b30f-f24a8c49741e"), "0%" }
                });

            migrationBuilder.InsertData(
                table: "SurveyAnswers",
                columns: new[] { "Id", "SurveyQuestionModelId", "Value" },
                values: new object[,]
                {
                    { new Guid("04c6ad19-1e4f-4c94-9866-bc7232900a3f"), new Guid("a746e9b7-1205-417f-b52d-11c06237abad"), "Advanced analytics and reporting" },
                    { new Guid("0a0b9c44-d4b9-4575-a8a6-23070ba6d519"), new Guid("527355e6-b2dd-4948-978d-cd27ca2f8e6a"), "Earning money through affiliate marketing" },
                    { new Guid("0c2b7934-ad7a-4fc7-bfeb-a6391ce00f71"), new Guid("53b02b05-e776-430a-9c3a-d2b7664eb13e"), "Not very useful" },
                    { new Guid("39f8b69d-9724-43ef-bef9-6761710263e2"), new Guid("53b02b05-e776-430a-9c3a-d2b7664eb13e"), "Very useful" },
                    { new Guid("414c8646-14f1-4644-bab1-30adc65d9b55"), new Guid("53b02b05-e776-430a-9c3a-d2b7664eb13e"), "Not useful at all" },
                    { new Guid("48fca2f4-cd5c-41d4-bbe4-b0a1b5861287"), new Guid("a746e9b7-1205-417f-b52d-11c06237abad"), "Improved user interface and navigation" },
                    { new Guid("56818da3-c514-405c-98ba-16bc021662eb"), new Guid("481e23cc-274b-4e77-a615-4e7fe9cbb065"), "PayPal" },
                    { new Guid("6c69816d-b809-49c4-8cd0-e61f2cb45496"), new Guid("527355e6-b2dd-4948-978d-cd27ca2f8e6a"), "Exploring new advertising opportunities" },
                    { new Guid("6c8b8778-b109-4ec1-b4df-89b198cee430"), new Guid("53b02b05-e776-430a-9c3a-d2b7664eb13e"), "Extremely useful" },
                    { new Guid("7b79f529-f5fc-425e-816f-7586928b6936"), new Guid("b9537c05-0004-4063-852e-90de39f90c6e"), "I'm not sure" },
                    { new Guid("7d39037c-d5b7-4422-bf5e-cf89e09b7aaf"), new Guid("a746e9b7-1205-417f-b52d-11c06237abad"), "Integration with other advertising platforms" },
                    { new Guid("8398cc5b-6bfb-482a-9837-3685bda7a9de"), new Guid("481e23cc-274b-4e77-a615-4e7fe9cbb065"), "Stripe" },
                    { new Guid("bac2f4e7-d8b2-4ff0-a7f9-3569d3341497"), new Guid("a746e9b7-1205-417f-b52d-11c06237abad"), "I'm not sure" },
                    { new Guid("bb88a70b-d2be-41f1-a653-3d6a89890466"), new Guid("527355e6-b2dd-4948-978d-cd27ca2f8e6a"), "Promoting my products or services" },
                    { new Guid("c4abbe42-b81e-4814-9760-1b2aeeb86401"), new Guid("53b02b05-e776-430a-9c3a-d2b7664eb13e"), "Somewhat useful" },
                    { new Guid("c65b3ca9-eaad-45ce-8d6c-636d8fb5ad48"), new Guid("b9537c05-0004-4063-852e-90de39f90c6e"), "Monthly subscription fee" },
                    { new Guid("c78c8031-5b76-42d4-9604-becc575085c4"), new Guid("481e23cc-274b-4e77-a615-4e7fe9cbb065"), "Credit/Debit card" },
                    { new Guid("ca42c8fe-3c21-47a9-a070-0cba26382a36"), new Guid("a746e9b7-1205-417f-b52d-11c06237abad"), "More campaign customization options" },
                    { new Guid("ceb885db-0f1c-455a-b616-d17ebc18b70f"), new Guid("481e23cc-274b-4e77-a615-4e7fe9cbb065"), "Bank transfer" },
                    { new Guid("f2495822-6154-4099-a41a-d2ab9eeb9501"), new Guid("b9537c05-0004-4063-852e-90de39f90c6e"), "Commission on earnings" },
                    { new Guid("f6482cc9-1485-44fb-bee9-1304daf07d68"), new Guid("527355e6-b2dd-4948-978d-cd27ca2f8e6a"), "Connecting with other users and businesses" }
                });

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
                name: "IX_BitcoinBillings_UserModelId",
                table: "BitcoinBillings",
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
                name: "IX_Settings_UserModelId",
                table: "Settings",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaticPageProductByPages_StaticPageModelId",
                table: "StaticPageProductByPages",
                column: "StaticPageModelId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticPageProductByPages_StaticPageProductModelId",
                table: "StaticPageProductByPages",
                column: "StaticPageProductModelId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticPageProductByResources_StaticPageProductModelId",
                table: "StaticPageProductByResources",
                column: "StaticPageProductModelId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticPageProductByResources_StaticPageResourceModelId",
                table: "StaticPageProductByResources",
                column: "StaticPageResourceModelId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticPages_StaticPageDataTemplateModelId",
                table: "StaticPages",
                column: "StaticPageDataTemplateModelId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticPages_StaticPageWebsiteTemplateModelId",
                table: "StaticPages",
                column: "StaticPageWebsiteTemplateModelId");

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
                name: "IX_SurveyAnswers_SurveyQuestionModelId",
                table: "SurveyAnswers",
                column: "SurveyQuestionModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyDatapoints_SurveyAnswerModelId",
                table: "SurveyDatapoints",
                column: "SurveyAnswerModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyDatapoints_SurveyQuestionModelId",
                table: "SurveyDatapoints",
                column: "SurveyQuestionModelId");

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
                name: "BitcoinBillings");

            migrationBuilder.DropTable(
                name: "GenericEvent");

            migrationBuilder.DropTable(
                name: "Hits");

            migrationBuilder.DropTable(
                name: "JoinWaitingLists");

            migrationBuilder.DropTable(
                name: "MyPages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "Privacies");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "StaticPageProductByPages");

            migrationBuilder.DropTable(
                name: "StaticPageProductByResources");

            migrationBuilder.DropTable(
                name: "StripeBillings");

            migrationBuilder.DropTable(
                name: "SubscriptionFeatures");

            migrationBuilder.DropTable(
                name: "SurveyDatapoints");

            migrationBuilder.DropTable(
                name: "GeoDatas");

            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "StaticPages");

            migrationBuilder.DropTable(
                name: "StaticPageProducts");

            migrationBuilder.DropTable(
                name: "StaticPageResources");

            migrationBuilder.DropTable(
                name: "SurveyAnswers");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "StaticPageDataTemplates");

            migrationBuilder.DropTable(
                name: "StaticPageWebsiteTemplates");

            migrationBuilder.DropTable(
                name: "SurveyQuestions");

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
