using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamLuso.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_Address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_Address_AddressId1",
                schema: "DreamLuso",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_AddressId1",
                schema: "DreamLuso",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "AddressId1",
                schema: "DreamLuso",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "PorpertyId",
                schema: "DreamLuso",
                table: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressId1",
                schema: "DreamLuso",
                table: "Property",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PorpertyId",
                schema: "DreamLuso",
                table: "Address",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Property_AddressId1",
                schema: "DreamLuso",
                table: "Property",
                column: "AddressId1",
                unique: true,
                filter: "[AddressId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Address_AddressId1",
                schema: "DreamLuso",
                table: "Property",
                column: "AddressId1",
                principalSchema: "DreamLuso",
                principalTable: "Address",
                principalColumn: "Id");
        }
    }
}
