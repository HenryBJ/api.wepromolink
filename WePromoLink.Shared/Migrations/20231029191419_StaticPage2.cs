using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WePromoLink.Shared.Migrations
{
    public partial class StaticPage2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
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

            
            migrationBuilder.CreateIndex(
                name: "IX_StaticPageProductByResources_StaticPageProductModelId",
                table: "StaticPageProductByResources",
                column: "StaticPageProductModelId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticPageProductByResources_StaticPageResourceModelId",
                table: "StaticPageProductByResources",
                column: "StaticPageResourceModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaticPageProductByResources");

            
        }
    }
}
