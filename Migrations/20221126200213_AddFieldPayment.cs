using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WePromoLink.Migrations
{
    public partial class AddFieldPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "PaymentTransactions",
                newName: "Status");

            migrationBuilder.AddColumn<int>(
                name: "EmailModelId",
                table: "PaymentTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredAt",
                table: "PaymentTransactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentLink",
                table: "PaymentTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailModelId",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "ExpiredAt",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "PaymentLink",
                table: "PaymentTransactions");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "PaymentTransactions",
                newName: "Email");
        }
    }
}
