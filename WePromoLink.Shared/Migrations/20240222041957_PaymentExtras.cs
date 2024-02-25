using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WePromoLink.Shared.Migrations
{
    public partial class PaymentExtras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<decimal>(
                name: "AmountNet",
                table: "PaymentTransactions",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PaymentProcessorFee",
                table: "PaymentTransactions",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeTranferId",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: true);
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "AmountNet",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "PaymentProcessorFee",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "StripeTranferId",
                table: "PaymentTransactions");

            
        }
    }
}
