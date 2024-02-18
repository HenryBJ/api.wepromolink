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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxAge = table.Column<TimeSpan>(type: "time", nullable: true),
                    Etag = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                columns: new[] { "Id", "Annually", "AnnualyPriceId", "Commission", "Discount", "ExternalId", "Level", "Metadata", "Monthly", "MonthlyPriceId", "Order", "PaymentMethod", "Tag", "Title" },
                values: new object[] { new Guid("4c440ccb-4797-481c-a428-5893c5493aad"), 0m, "", 0.09m, 0m, "il7Tbn3V4iFw", 1, null, 0m, "price_1OktQYC26XBdqsojcQwEpZTu", 2, "stripe", "Popular", "Basic" });

            migrationBuilder.InsertData(
                table: "SurveyQuestions",
                columns: new[] { "Id", "Group", "Value" },
                values: new object[,]
                {
                    { new Guid("191c668b-e3e4-4169-bb51-f3e6e740d86b"), 2, "Which payment methods do you prefer for platform subscriptions and earnings withdrawals?" },
                    { new Guid("5aca019b-e281-4027-ae46-52dbe8db55bf"), 5, "What additional features or improvements would you like to see on the WePromoLink platform?" },
                    { new Guid("943590a1-6d2f-4d2e-be37-7a3d06f42b83"), 3, "Do you prefer a monthly subscription fee or paying a commission on earnings?" },
                    { new Guid("c761951d-ef8a-4ad3-90b3-64f7e90441df"), 4, "How useful do you find the WePromoLink platform for your advertising needs?" },
                    { new Guid("eae5653d-be97-4674-81eb-5dde7d2fc7ef"), 1, "What motivates you to use a platform like WePromoLink?" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionFeatures",
                columns: new[] { "Id", "BoolValue", "Name", "SubscriptionPlanModelId", "Value" },
                values: new object[,]
                {
                    { new Guid("5c255353-0fcc-4c21-9945-490e3a40a385"), false, "Links", new Guid("4c440ccb-4797-481c-a428-5893c5493aad"), "Unlimited" },
                    { new Guid("a4f20b2f-de54-421d-ae16-63dc276b3223"), true, "Contain ads", new Guid("4c440ccb-4797-481c-a428-5893c5493aad"), "" },
                    { new Guid("e702dab7-560f-46ea-a51c-522b401a4e39"), false, "Commission per click", new Guid("4c440ccb-4797-481c-a428-5893c5493aad"), "U$0.09" },
                    { new Guid("e73e19b1-98a7-4d0b-bb00-d6e5273d4232"), false, "Campaigns", new Guid("4c440ccb-4797-481c-a428-5893c5493aad"), "Unlimited" }
                });

            migrationBuilder.InsertData(
                table: "SurveyAnswers",
                columns: new[] { "Id", "SurveyQuestionModelId", "Value" },
                values: new object[,]
                {
                    { new Guid("009a2738-b1f0-4a05-9900-5a1925cbf42b"), new Guid("c761951d-ef8a-4ad3-90b3-64f7e90441df"), "Extremely useful" },
                    { new Guid("11cf7250-05fe-466e-883f-29959823808a"), new Guid("191c668b-e3e4-4169-bb51-f3e6e740d86b"), "Bank transfer" },
                    { new Guid("1bef408c-1c69-416b-9e87-86aaa175ee60"), new Guid("c761951d-ef8a-4ad3-90b3-64f7e90441df"), "Not useful at all" },
                    { new Guid("2f413356-2958-4594-a084-c18fa01bc80a"), new Guid("eae5653d-be97-4674-81eb-5dde7d2fc7ef"), "Connecting with other users and businesses" },
                    { new Guid("2f46f69a-b0db-45c1-afe7-0cec7e772dc8"), new Guid("943590a1-6d2f-4d2e-be37-7a3d06f42b83"), "Monthly subscription fee" },
                    { new Guid("33aefef1-1601-42cb-a18a-3059fdcc782d"), new Guid("eae5653d-be97-4674-81eb-5dde7d2fc7ef"), "Promoting my products or services" },
                    { new Guid("3a162e33-11e0-401d-8bff-5f37dd5c10f5"), new Guid("c761951d-ef8a-4ad3-90b3-64f7e90441df"), "Not very useful" },
                    { new Guid("65593b8c-3d84-47af-81fa-2591b5ae9f2f"), new Guid("eae5653d-be97-4674-81eb-5dde7d2fc7ef"), "Earning money through affiliate marketing" },
                    { new Guid("83f7e6e0-daf5-4c19-93da-b8fb50e4935c"), new Guid("c761951d-ef8a-4ad3-90b3-64f7e90441df"), "Somewhat useful" },
                    { new Guid("8418c289-686a-46a9-bb27-84ca5249637e"), new Guid("5aca019b-e281-4027-ae46-52dbe8db55bf"), "I'm not sure" },
                    { new Guid("86dab0a0-2212-4cee-8913-39a8b7bdbba2"), new Guid("c761951d-ef8a-4ad3-90b3-64f7e90441df"), "Very useful" },
                    { new Guid("8aee4bc3-db2d-4ecd-b474-0d26a6d21d6d"), new Guid("5aca019b-e281-4027-ae46-52dbe8db55bf"), "More campaign customization options" },
                    { new Guid("92d229d7-2b00-4eeb-b287-920efc5babe0"), new Guid("5aca019b-e281-4027-ae46-52dbe8db55bf"), "Integration with other advertising platforms" },
                    { new Guid("a87ae105-22d1-40bf-8059-ea39b73009bc"), new Guid("943590a1-6d2f-4d2e-be37-7a3d06f42b83"), "Commission on earnings" },
                    { new Guid("ba66e195-63a1-47cc-8f68-276f9414146a"), new Guid("943590a1-6d2f-4d2e-be37-7a3d06f42b83"), "I'm not sure" },
                    { new Guid("ccc5973b-1ce3-43fa-bd37-ec35bc64c312"), new Guid("191c668b-e3e4-4169-bb51-f3e6e740d86b"), "Stripe" },
                    { new Guid("d969b536-753c-4ae4-9389-68c6879f55b5"), new Guid("191c668b-e3e4-4169-bb51-f3e6e740d86b"), "PayPal" },
                    { new Guid("de2b7e59-276d-47e3-a6ab-6d39592ded8c"), new Guid("5aca019b-e281-4027-ae46-52dbe8db55bf"), "Advanced analytics and reporting" },
                    { new Guid("e15eb85f-6ec1-4816-9c5e-e60dfd6524fe"), new Guid("eae5653d-be97-4674-81eb-5dde7d2fc7ef"), "Exploring new advertising opportunities" },
                    { new Guid("e47c1e8d-d4bb-4f44-9f72-d0a4c1fbaaf4"), new Guid("5aca019b-e281-4027-ae46-52dbe8db55bf"), "Improved user interface and navigation" },
                    { new Guid("e7f21638-23c4-4ad0-8a1d-da61670e5948"), new Guid("191c668b-e3e4-4169-bb51-f3e6e740d86b"), "Credit/Debit card" }
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
                name: "JoinWaitingLists");

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
