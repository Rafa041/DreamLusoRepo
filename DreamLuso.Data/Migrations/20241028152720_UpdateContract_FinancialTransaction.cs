using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamLuso.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContract_FinancialTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransactions_Property_PropertyId",
                schema: "DreamLuso",
                table: "FinancialTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransactions_RealStateAgent_RealStateAgentId",
                schema: "DreamLuso",
                table: "FinancialTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Clients_RecipentUserId",
                schema: "DreamLuso",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_RealStateAgent_SenderUserId",
                schema: "DreamLuso",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_RecipentUserId",
                schema: "DreamLuso",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_SenderUserId",
                schema: "DreamLuso",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_FinancialTransactions_RealStateAgentId",
                schema: "DreamLuso",
                table: "FinancialTransactions");

            migrationBuilder.DropColumn(
                name: "RecipentUserId",
                schema: "DreamLuso",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "SenderUserId",
                schema: "DreamLuso",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "RealStateAgentId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                newName: "InvoiceId");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                newName: "ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialTransactions_PropertyId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                newName: "IX_FinancialTransactions_ContractId");

            migrationBuilder.AlterColumn<int>(
                name: "TransactionStatus",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                schema: "DreamLuso",
                table: "Contracts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "AdditionalFees",
                schema: "DreamLuso",
                table: "Contracts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "ContractType",
                schema: "DreamLuso",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DocumentPath",
                schema: "DreamLuso",
                table: "Contracts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InsuranceDetails",
                schema: "DreamLuso",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "DreamLuso",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SecurityDeposit",
                schema: "DreamLuso",
                table: "Contracts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SignatureDate",
                schema: "DreamLuso",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "DreamLuso",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Invoice",
                schema: "DreamLuso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "DreamLuso",
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_FinancialTransactions_TransactionId",
                        column: x => x.TransactionId,
                        principalSchema: "DreamLuso",
                        principalTable: "FinancialTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PropertyVisits",
                schema: "DreamLuso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RealStateAgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyVisits_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "DreamLuso",
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyVisits_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "DreamLuso",
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyVisits_RealStateAgent_RealStateAgentId",
                        column: x => x.RealStateAgentId,
                        principalSchema: "DreamLuso",
                        principalTable: "RealStateAgent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RecipientId",
                schema: "DreamLuso",
                table: "Notifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SenderId",
                schema: "DreamLuso",
                table: "Notifications",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ContractId",
                schema: "DreamLuso",
                table: "Invoice",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_TransactionId",
                schema: "DreamLuso",
                table: "Invoice",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyVisits_ClientId",
                schema: "DreamLuso",
                table: "PropertyVisits",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyVisits_PropertyId",
                schema: "DreamLuso",
                table: "PropertyVisits",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyVisits_RealStateAgentId",
                schema: "DreamLuso",
                table: "PropertyVisits",
                column: "RealStateAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransactions_Contracts_ContractId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                column: "ContractId",
                principalSchema: "DreamLuso",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_RecipientId",
                schema: "DreamLuso",
                table: "Notifications",
                column: "RecipientId",
                principalSchema: "DreamLuso",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_SenderId",
                schema: "DreamLuso",
                table: "Notifications",
                column: "SenderId",
                principalSchema: "DreamLuso",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransactions_Contracts_ContractId",
                schema: "DreamLuso",
                table: "FinancialTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_RecipientId",
                schema: "DreamLuso",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_SenderId",
                schema: "DreamLuso",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "Invoice",
                schema: "DreamLuso");

            migrationBuilder.DropTable(
                name: "PropertyVisits",
                schema: "DreamLuso");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_RecipientId",
                schema: "DreamLuso",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_SenderId",
                schema: "DreamLuso",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ContractType",
                schema: "DreamLuso",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "DocumentPath",
                schema: "DreamLuso",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "InsuranceDetails",
                schema: "DreamLuso",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "DreamLuso",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SecurityDeposit",
                schema: "DreamLuso",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SignatureDate",
                schema: "DreamLuso",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "DreamLuso",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                newName: "RealStateAgentId");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                newName: "PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialTransactions_ContractId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                newName: "IX_FinancialTransactions_PropertyId");

            migrationBuilder.AddColumn<Guid>(
                name: "RecipentUserId",
                schema: "DreamLuso",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SenderUserId",
                schema: "DreamLuso",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "TransactionStatus",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                schema: "DreamLuso",
                table: "Contracts",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "AdditionalFees",
                schema: "DreamLuso",
                table: "Contracts",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RecipentUserId",
                schema: "DreamLuso",
                table: "Notifications",
                column: "RecipentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SenderUserId",
                schema: "DreamLuso",
                table: "Notifications",
                column: "SenderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactions_RealStateAgentId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                column: "RealStateAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransactions_Property_PropertyId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                column: "PropertyId",
                principalSchema: "DreamLuso",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransactions_RealStateAgent_RealStateAgentId",
                schema: "DreamLuso",
                table: "FinancialTransactions",
                column: "RealStateAgentId",
                principalSchema: "DreamLuso",
                principalTable: "RealStateAgent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Clients_RecipentUserId",
                schema: "DreamLuso",
                table: "Notifications",
                column: "RecipentUserId",
                principalSchema: "DreamLuso",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_RealStateAgent_SenderUserId",
                schema: "DreamLuso",
                table: "Notifications",
                column: "SenderUserId",
                principalSchema: "DreamLuso",
                principalTable: "RealStateAgent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
