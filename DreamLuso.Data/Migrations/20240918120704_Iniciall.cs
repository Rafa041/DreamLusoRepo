using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamLuso.Data.Migrations
{
    /// <inheritdoc />
    public partial class Iniciall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransactions_Users_UserId",
                schema: "DreamLuso",
                table: "FinancialTransactions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialTransactions_UserId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                newName: "IX_FinancialTransactions_ClientId");

            migrationBuilder.AddColumn<bool>(
                name: "IsMainImage",
                schema: "DreamLuso",
                table: "PropertyImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsForRent",
                schema: "DreamLuso",
                table: "Property",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsForSale",
                schema: "DreamLuso",
                table: "Property",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "DreamLuso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsBuyer = table.Column<bool>(type: "bit", nullable: false),
                    IsTenant = table.Column<bool>(type: "bit", nullable: false),
                    DateOfFirstTransaction = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "DreamLuso",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                schema: "DreamLuso",
                table: "Clients",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransactions_Clients_ClientId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                column: "ClientId",
                principalSchema: "DreamLuso",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransactions_Clients_ClientId",
                schema: "DreamLuso",
                table: "FinancialTransactions");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "DreamLuso");

            migrationBuilder.DropColumn(
                name: "IsMainImage",
                schema: "DreamLuso",
                table: "PropertyImages");

            migrationBuilder.DropColumn(
                name: "IsForRent",
                schema: "DreamLuso",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "IsForSale",
                schema: "DreamLuso",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialTransactions_ClientId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                newName: "IX_FinancialTransactions_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransactions_Users_UserId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                column: "UserId",
                principalSchema: "DreamLuso",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
