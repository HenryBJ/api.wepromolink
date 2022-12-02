using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WePromoLink.Migrations
{
    public partial class OtherChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainBudget",
                table: "SponsoredLinks");

            migrationBuilder.AlterColumn<int>(
                name: "EmailModelId",
                table: "PaymentTransactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RemainBudget",
                table: "SponsoredLinks",
                type: "decimal(10,8)",
                precision: 10,
                scale: 8,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "EmailModelId",
                table: "PaymentTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
