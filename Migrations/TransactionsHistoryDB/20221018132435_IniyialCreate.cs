using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingAPI.Migrations.TransactionsHistoryDB
{
    public partial class IniyialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionsHistory",
                columns: table => new
                {
                    transactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fromAccountNumber = table.Column<int>(type: "int", nullable: false),
                    toAccountNumber = table.Column<int>(type: "int", nullable: false),
                    transactionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amountDebit = table.Column<double>(type: "float", nullable: false),
                    fromAccountBalance = table.Column<double>(type: "float", nullable: false),
                    toAccountBalance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionsHistory", x => x.transactionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionsHistory");
        }
    }
}
