using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamLuso.Data.Migrations;

/// <inheritdoc />
public partial class Inicial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "DreamLuso");

        migrationBuilder.CreateTable(
            name: "Address",
            schema: "DreamLuso",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Address", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Category",
            schema: "DreamLuso",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Category", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            schema: "DreamLuso",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Access = table.Column<int>(type: "int", nullable: false),
                ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Accounts",
            schema: "DreamLuso",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Accounts", x => x.Id);
                table.ForeignKey(
                    name: "FK_Accounts_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "DreamLuso",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Notifications",
            schema: "DreamLuso",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                SenderUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RecipentUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false),
                ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                Priority = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Notifications", x => x.Id);
                table.ForeignKey(
                    name: "FK_Notifications_Users_RecipentUserId",
                    column: x => x.RecipentUserId,
                    principalSchema: "DreamLuso",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.NoAction);
                table.ForeignKey(
                    name: "FK_Notifications_Users_SenderUserId",
                    column: x => x.SenderUserId,
                    principalSchema: "DreamLuso",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.NoAction);
            });

        migrationBuilder.CreateTable(
            name: "RealStateAgent",
            schema: "DreamLuso",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                OfficeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                TotalSales = table.Column<int>(type: "int", nullable: false),
                TotalListings = table.Column<int>(type: "int", nullable: false),
                Certifications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                LanguagesSpoken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RealStateAgent", x => x.Id);
                table.ForeignKey(
                    name: "FK_RealStateAgent_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "DreamLuso",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Property",
            schema: "DreamLuso",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false),
                Size = table.Column<double>(type: "float", nullable: false),
                Bedrooms = table.Column<int>(type: "int", nullable: false),
                Bathrooms = table.Column<int>(type: "int", nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Amenities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                DateListed = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                YearBuilt = table.Column<DateTime>(type: "datetime2", nullable: false),
                OwnerInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                HeatingSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CoolingSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                RealStateAgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Property", x => x.Id);
                table.ForeignKey(
                    name: "FK_Property_Address_AddressId",
                    column: x => x.AddressId,
                    principalSchema: "DreamLuso",
                    principalTable: "Address",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Property_RealStateAgent_RealStateAgentId",
                    column: x => x.RealStateAgentId,
                    principalSchema: "DreamLuso",
                    principalTable: "RealStateAgent",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Property_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "DreamLuso",
                    principalTable: "Users",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Comments",
            schema: "DreamLuso",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Rating = table.Column<int>(type: "int", nullable: false),
                DateTimePosted = table.Column<DateTime>(type: "datetime2", nullable: false),
                Flagged = table.Column<bool>(type: "bit", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Comments", x => x.Id);
                table.ForeignKey(
                    name: "FK_Comments_Property_PropertyId",
                    column: x => x.PropertyId,
                    principalSchema: "DreamLuso",
                    principalTable: "Property",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Comments_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "DreamLuso",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Favorites",
            schema: "DreamLuso",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Favorites", x => x.Id);
                table.ForeignKey(
                    name: "FK_Favorites_Category_CategoryId",
                    column: x => x.CategoryId,
                    principalSchema: "DreamLuso",
                    principalTable: "Category",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Favorites_Property_PropertyId",
                    column: x => x.PropertyId,
                    principalSchema: "DreamLuso",
                    principalTable: "Property",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Favorites_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "DreamLuso",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "FinancialTransactions",
            schema: "DreamLuso",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RealStateAgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Value = table.Column<double>(type: "float", nullable: false),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ReferenceId = table.Column<double>(type: "float", nullable: false),
                PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                TransactionStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                TransactionHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FinancialTransactions", x => x.Id);
                table.ForeignKey(
                    name: "FK_FinancialTransactions_Property_PropertyId",
                    column: x => x.PropertyId,
                    principalSchema: "DreamLuso",
                    principalTable: "Property",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_FinancialTransactions_RealStateAgent_RealStateAgentId",
                    column: x => x.RealStateAgentId,
                    principalSchema: "DreamLuso",
                    principalTable: "RealStateAgent",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_FinancialTransactions_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "DreamLuso",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.NoAction);
            });

        migrationBuilder.CreateTable(
            name: "PropertyImages",
            schema: "DreamLuso",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FileName = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PropertyImages", x => x.Id);
                table.ForeignKey(
                    name: "FK_PropertyImages_Property_PropertyId",
                    column: x => x.PropertyId,
                    principalSchema: "DreamLuso",
                    principalTable: "Property",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Accounts_UserId",
            schema: "DreamLuso",
            table: "Accounts",
            column: "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Comments_PropertyId",
            schema: "DreamLuso",
            table: "Comments",
            column: "PropertyId");

        migrationBuilder.CreateIndex(
            name: "IX_Comments_UserId",
            schema: "DreamLuso",
            table: "Comments",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Favorites_CategoryId",
            schema: "DreamLuso",
            table: "Favorites",
            column: "CategoryId");

        migrationBuilder.CreateIndex(
            name: "IX_Favorites_PropertyId",
            schema: "DreamLuso",
            table: "Favorites",
            column: "PropertyId");

        migrationBuilder.CreateIndex(
            name: "IX_Favorites_UserId",
            schema: "DreamLuso",
            table: "Favorites",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_FinancialTransactions_PropertyId",
            schema: "DreamLuso",
            table: "FinancialTransactions",
            column: "PropertyId");

        migrationBuilder.CreateIndex(
            name: "IX_FinancialTransactions_RealStateAgentId",
            schema: "DreamLuso",
            table: "FinancialTransactions",
            column: "RealStateAgentId");

        migrationBuilder.CreateIndex(
            name: "IX_FinancialTransactions_UserId",
            schema: "DreamLuso",
            table: "FinancialTransactions",
            column: "UserId");

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
            name: "IX_Property_AddressId",
            schema: "DreamLuso",
            table: "Property",
            column: "AddressId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Property_RealStateAgentId",
            schema: "DreamLuso",
            table: "Property",
            column: "RealStateAgentId");

        migrationBuilder.CreateIndex(
            name: "IX_Property_UserId",
            schema: "DreamLuso",
            table: "Property",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_PropertyImages_PropertyId",
            schema: "DreamLuso",
            table: "PropertyImages",
            column: "PropertyId");

        migrationBuilder.CreateIndex(
            name: "IX_RealStateAgent_UserId",
            schema: "DreamLuso",
            table: "RealStateAgent",
            column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Accounts",
            schema: "DreamLuso");

        migrationBuilder.DropTable(
            name: "Comments",
            schema: "DreamLuso");

        migrationBuilder.DropTable(
            name: "Favorites",
            schema: "DreamLuso");

        migrationBuilder.DropTable(
            name: "FinancialTransactions",
            schema: "DreamLuso");

        migrationBuilder.DropTable(
            name: "Notifications",
            schema: "DreamLuso");

        migrationBuilder.DropTable(
            name: "PropertyImages",
            schema: "DreamLuso");

        migrationBuilder.DropTable(
            name: "Category",
            schema: "DreamLuso");

        migrationBuilder.DropTable(
            name: "Property",
            schema: "DreamLuso");

        migrationBuilder.DropTable(
            name: "Address",
            schema: "DreamLuso");

        migrationBuilder.DropTable(
            name: "RealStateAgent",
            schema: "DreamLuso");

        migrationBuilder.DropTable(
            name: "Users",
            schema: "DreamLuso");
    }
}
