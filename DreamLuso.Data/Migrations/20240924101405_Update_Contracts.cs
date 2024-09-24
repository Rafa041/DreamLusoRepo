using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamLuso.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_Contracts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBuyer",
                schema: "DreamLuso",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsTenant",
                schema: "DreamLuso",
                table: "Clients");

            migrationBuilder.AddColumn<bool>(
                name: "IsBuyer",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTenant",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBuyer",
                schema: "DreamLuso",
                table: "FinancialTransactions");

            migrationBuilder.DropColumn(
                name: "IsTenant",
                schema: "DreamLuso",
                table: "FinancialTransactions");

            migrationBuilder.AddColumn<bool>(
                name: "IsBuyer",
                schema: "DreamLuso",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTenant",
                schema: "DreamLuso",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
