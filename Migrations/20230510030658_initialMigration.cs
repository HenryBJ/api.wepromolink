﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WePromoLink.Migrations
{
    public partial class initialMigration : Migration
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
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MonthlyProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnualyProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyPaymantLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnnualyPaymantLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monthly = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    Annually = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    ContainAds = table.Column<bool>(type: "bit", nullable: false),
                    DepositFee = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    PayoutFee = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    PayoutMinimun = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
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
                name: "Badgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Notification = table.Column<int>(type: "int", nullable: false),
                    Campaign = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<int>(type: "int", nullable: false),
                    Clicks = table.Column<int>(type: "int", nullable: false),
                    Deposit = table.Column<int>(type: "int", nullable: false),
                    Withdraw = table.Column<int>(type: "int", nullable: false),
                    flag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Badgets_Users_UserModelId",
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
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "PayoutInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayoutType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BCTAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DebitCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Paypal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stripe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WireName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WireBankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WireSwiftorBic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WireIban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WireBankAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WireBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    wireRouting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayoutInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayoutInfos_Users_UserModelId",
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
                    table.PrimaryKey("PK_HistoryClicksOnLinkModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryClicksOnLinkModels_Links_LinkModelId",
                        column: x => x.LinkModelId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryClicksOnLinkUsers",
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
                    table.PrimaryKey("PK_HistoryClicksOnLinkUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryClicksOnLinkUsers_Links_LinkModelId",
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
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Geolocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsGeolocated = table.Column<bool>(type: "bit", nullable: false),
                    MapImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                columns: new[] { "Id", "Annually", "AnnualyPaymantLink", "AnnualyProductId", "ContainAds", "DepositFee", "Discount", "ExternalId", "Metadata", "Monthly", "MonthlyPaymantLink", "MonthlyProductId", "Order", "PaymentMethod", "PayoutFee", "PayoutMinimun", "Tag", "Title" },
                values: new object[] { new Guid("49c1e687-61b4-4e01-b264-f744b821534b"), 0m, "", "", true, 9m, 0m, "j_L41bzqdiwu", null, 0m, "", "", 1, "bitcoin", 9m, 100m, "", "Community" });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "Annually", "AnnualyPaymantLink", "AnnualyProductId", "ContainAds", "DepositFee", "Discount", "ExternalId", "Metadata", "Monthly", "MonthlyPaymantLink", "MonthlyProductId", "Order", "PaymentMethod", "PayoutFee", "PayoutMinimun", "Tag", "Title" },
                values: new object[] { new Guid("78caea83-fd1e-4945-b353-656c1dac833b"), 244m, "https://buy.stripe.com/test_8wM8Ao6iAfFD3m0aEF", "prod_NpuAflpfqloJa9", false, 0m, 15m, "WCb1yfH1bJhw", null, 24m, "https://buy.stripe.com/test_eVa9Es8qI0KJaOs7ss", "prod_NpnKrvEvvWJtqG", 2, "mastercard, visa, stripe", 0m, 50m, "Popular", "Professional" });

            migrationBuilder.CreateIndex(
                name: "IX_Availables_UserModelId",
                table: "Availables",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Badgets_UserModelId",
                table: "Badgets",
                column: "UserModelId");

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
                name: "IX_HistoryClicksOnLinkUsers_LinkModelId",
                table: "HistoryClicksOnLinkUsers",
                column: "LinkModelId");

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
                name: "IX_PayoutInfos_UserModelId",
                table: "PayoutInfos",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PayoutStats_UserModelId",
                table: "PayoutStats",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profits_UserModelId",
                table: "Profits",
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
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedTodayOnCampaigns_CampaignModelId",
                table: "SharedTodayOnCampaigns",
                column: "CampaignModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SharedTodayUsers_UserModelId",
                table: "SharedTodayUsers",
                column: "UserModelId");

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
                name: "Availables");

            migrationBuilder.DropTable(
                name: "Badgets");

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
                name: "HistoryClicksOnLinkUsers");

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
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "PayoutInfos");

            migrationBuilder.DropTable(
                name: "PayoutStats");

            migrationBuilder.DropTable(
                name: "Profits");

            migrationBuilder.DropTable(
                name: "SharedLastWeekOnCampaigns");

            migrationBuilder.DropTable(
                name: "SharedLastWeekUsers");

            migrationBuilder.DropTable(
                name: "SharedTodayOnCampaigns");

            migrationBuilder.DropTable(
                name: "SharedTodayUsers");

            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");
        }
    }
}
