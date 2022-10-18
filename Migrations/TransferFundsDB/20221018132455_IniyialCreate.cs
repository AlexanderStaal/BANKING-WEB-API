using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingAPI.Migrations.TransferFundsDB
{
    public partial class IniyialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferFunds",
                columns: table => new
                {
                    fromAccountNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    toAccountNumber = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferFunds", x => x.fromAccountNumber);
                });

            migrationBuilder.CreateTable(
                name: "TransferFundsStatus",
                columns: table => new
                {
                    Returnvalue = table.Column<string>(name: "Return value", type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferFundsStatus", x => x.Returnvalue);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferFunds");

            migrationBuilder.DropTable(
                name: "TransferFundsStatus");
        }
    }
}
