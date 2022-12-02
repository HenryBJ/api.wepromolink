using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WePromoLink.Migrations
{
    public partial class PayTransPayout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PayoutId",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayoutId",
                table: "PaymentTransactions");
        }
    }
}
