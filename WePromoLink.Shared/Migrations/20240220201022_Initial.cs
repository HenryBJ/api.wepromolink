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
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                columns: new[] { "Id", "Annually", "AnnualyPriceId", "Commission", "Discount", "ExternalId", "Level", "Metadata", "Monthly", "MonthlyPriceId", "Order", "PaymentMethod", "Tag", "Title" },
                values: new object[,]
                {
                    { new Guid("29a030e6-e678-4c8e-bed6-9b8c31268571"), 0m, "", 0.01m, 0m, "swr4YVUhcnjJ", 1, null, 0m, "price_1Ol0LGC26XBdqsojWuT9BKvb", 2, "stripe", "", "Basic" },
                    { new Guid("88f73d2f-69ce-45d5-8171-ece2c49dfb7b"), 0m, "", 0m, 0m, "SoIk3H2vnIpb", 2, null, 4.99m, "price_1OkwiHC26XBdqsojpgor0QaV", 3, "stripe", "Popular", "Professional" }
                });

            migrationBuilder.InsertData(
                table: "SurveyQuestions",
                columns: new[] { "Id", "Group", "Value" },
                values: new object[,]
                {
                    { new Guid("005de81a-ea60-462e-bc13-b24b3ce83a0f"), 2, "Which payment methods do you prefer for platform subscriptions and earnings withdrawals?" },
                    { new Guid("39b09178-a1ce-4105-a640-7875b64c3f37"), 5, "What additional features or improvements would you like to see on the WePromoLink platform?" },
                    { new Guid("78206eeb-68b7-4207-b5dc-068082aa1cdb"), 1, "What motivates you to use a platform like WePromoLink?" },
                    { new Guid("b56250cd-f296-4ed9-85d0-d15c4b45a84d"), 4, "How useful do you find the WePromoLink platform for your advertising needs?" },
                    { new Guid("bcda607a-be83-48fa-8710-a464cd5d81c5"), 3, "Do you prefer a monthly subscription fee or paying a commission on earnings?" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionFeatures",
                columns: new[] { "Id", "BoolValue", "Name", "SubscriptionPlanModelId", "Value" },
                values: new object[,]
                {
                    { new Guid("25a029e4-2bc5-4d8a-953b-61504ea7b879"), false, "Commission per click", new Guid("88f73d2f-69ce-45d5-8171-ece2c49dfb7b"), "U$0.00" },
                    { new Guid("33d6956c-c000-411c-aa41-0f91d893cac9"), true, "Contain ads", new Guid("29a030e6-e678-4c8e-bed6-9b8c31268571"), "" },
                    { new Guid("4301b911-3f20-4ba7-9ed1-b8ad568eeaa0"), false, "Links", new Guid("88f73d2f-69ce-45d5-8171-ece2c49dfb7b"), "Unlimited" },
                    { new Guid("4724a846-12ab-4423-bbbe-65e8d825ea43"), false, "Campaigns", new Guid("29a030e6-e678-4c8e-bed6-9b8c31268571"), "Unlimited" },
                    { new Guid("97bcec47-6d0f-4a9e-8acb-741cc3cee8bf"), false, "Links", new Guid("29a030e6-e678-4c8e-bed6-9b8c31268571"), "Unlimited" },
                    { new Guid("998a1683-e4cb-4f49-8f75-5b8d26c7cf3e"), false, "Contain ads", new Guid("88f73d2f-69ce-45d5-8171-ece2c49dfb7b"), "" },
                    { new Guid("a2c3dc40-977e-4e2b-bde1-0d828bb72b38"), false, "Campaigns", new Guid("88f73d2f-69ce-45d5-8171-ece2c49dfb7b"), "Unlimited" },
                    { new Guid("a92469d4-81d7-4c1d-bcb3-265f064443f3"), false, "Commission per click", new Guid("29a030e6-e678-4c8e-bed6-9b8c31268571"), "U$0.01" }
                });

            migrationBuilder.InsertData(
                table: "SurveyAnswers",
                columns: new[] { "Id", "SurveyQuestionModelId", "Value" },
                values: new object[,]
                {
                    { new Guid("102db161-8515-4cc7-9aab-2e0aa9d91125"), new Guid("005de81a-ea60-462e-bc13-b24b3ce83a0f"), "Credit/Debit card" },
                    { new Guid("18493212-3ae0-4b49-889c-b1d441c05559"), new Guid("bcda607a-be83-48fa-8710-a464cd5d81c5"), "Commission on earnings" },
                    { new Guid("19c9c571-ac8f-4f7d-8053-54f9d1f1c4d1"), new Guid("78206eeb-68b7-4207-b5dc-068082aa1cdb"), "Promoting my products or services" },
                    { new Guid("3443683c-d757-4011-bb30-64e41f970786"), new Guid("b56250cd-f296-4ed9-85d0-d15c4b45a84d"), "Very useful" },
                    { new Guid("35a6dcb9-c6ac-40bd-96da-297eafd07f7f"), new Guid("39b09178-a1ce-4105-a640-7875b64c3f37"), "I'm not sure" },
                    { new Guid("3e7e4fe7-2848-4ebf-882d-bb3906524250"), new Guid("39b09178-a1ce-4105-a640-7875b64c3f37"), "Advanced analytics and reporting" },
                    { new Guid("432db7cd-aacd-403c-99ab-8c243ffec0fe"), new Guid("bcda607a-be83-48fa-8710-a464cd5d81c5"), "Monthly subscription fee" },
                    { new Guid("4f352dee-5b0b-4e3f-a4f1-c93aabdbe96d"), new Guid("78206eeb-68b7-4207-b5dc-068082aa1cdb"), "Exploring new advertising opportunities" },
                    { new Guid("736b026b-b79a-4721-b41c-8426d898cd0d"), new Guid("b56250cd-f296-4ed9-85d0-d15c4b45a84d"), "Extremely useful" },
                    { new Guid("76ccbdf1-4fb2-40aa-8c5a-e10a9b5aaeb1"), new Guid("b56250cd-f296-4ed9-85d0-d15c4b45a84d"), "Not useful at all" },
                    { new Guid("9421c406-3291-45fd-a92e-c49dd7fc053a"), new Guid("78206eeb-68b7-4207-b5dc-068082aa1cdb"), "Connecting with other users and businesses" },
                    { new Guid("cbe9f9e2-6505-4704-b7fa-f08ba222ced5"), new Guid("005de81a-ea60-462e-bc13-b24b3ce83a0f"), "Bank transfer" },
                    { new Guid("cbf13998-292c-43eb-a7d5-0465db1a2ea5"), new Guid("39b09178-a1ce-4105-a640-7875b64c3f37"), "Improved user interface and navigation" },
                    { new Guid("ce11e04a-23ff-4733-a2d3-b5cd1b102a70"), new Guid("b56250cd-f296-4ed9-85d0-d15c4b45a84d"), "Somewhat useful" },
                    { new Guid("d28a1e23-e0b5-4d5d-b13d-4c96ac8c5b58"), new Guid("bcda607a-be83-48fa-8710-a464cd5d81c5"), "I'm not sure" },
                    { new Guid("d4e0fd46-a356-4d75-bf03-a3e945188ef3"), new Guid("005de81a-ea60-462e-bc13-b24b3ce83a0f"), "PayPal" },
                    { new Guid("d73e2668-7f0f-4351-8ce1-d1c536a35bb7"), new Guid("39b09178-a1ce-4105-a640-7875b64c3f37"), "More campaign customization options" },
                    { new Guid("dd1720fa-d533-41e7-b7ce-0b8fa943b7a6"), new Guid("39b09178-a1ce-4105-a640-7875b64c3f37"), "Integration with other advertising platforms" },
                    { new Guid("df9638f3-3f01-4eeb-926f-191452db1a47"), new Guid("005de81a-ea60-462e-bc13-b24b3ce83a0f"), "Stripe" },
                    { new Guid("e004f790-e7a8-4e70-93b8-98c5b990d031"), new Guid("78206eeb-68b7-4207-b5dc-068082aa1cdb"), "Earning money through affiliate marketing" },
                    { new Guid("f247c8c7-b061-4ff5-93e2-8d0af201a7e4"), new Guid("b56250cd-f296-4ed9-85d0-d15c4b45a84d"), "Not very useful" }
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
