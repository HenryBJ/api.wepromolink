using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WePromoLink.Shared.Migrations
{
    public partial class StaticPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
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

            

            migrationBuilder.CreateIndex(
                name: "IX_StaticPageProductByPages_StaticPageModelId",
                table: "StaticPageProductByPages",
                column: "StaticPageModelId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticPageProductByPages_StaticPageProductModelId",
                table: "StaticPageProductByPages",
                column: "StaticPageProductModelId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticPages_StaticPageDataTemplateModelId",
                table: "StaticPages",
                column: "StaticPageDataTemplateModelId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticPages_StaticPageWebsiteTemplateModelId",
                table: "StaticPages",
                column: "StaticPageWebsiteTemplateModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaticPageProductByPages");

            migrationBuilder.DropTable(
                name: "StaticPageResources");

            migrationBuilder.DropTable(
                name: "StaticPageProducts");

            migrationBuilder.DropTable(
                name: "StaticPages");

            migrationBuilder.DropTable(
                name: "StaticPageDataTemplates");

            migrationBuilder.DropTable(
                name: "StaticPageWebsiteTemplates");

            
        }
    }
}
