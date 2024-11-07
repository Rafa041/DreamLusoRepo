using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamLuso.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewPropertyVisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyVisits_Clients_ClientId",
                schema: "DreamLuso",
                table: "PropertyVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyVisits_RealStateAgent_RealStateAgentId",
                schema: "DreamLuso",
                table: "PropertyVisits");

            migrationBuilder.DropColumn(
                name: "Date",
                schema: "DreamLuso",
                table: "PropertyVisits");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                schema: "DreamLuso",
                table: "PropertyVisits",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyVisits_ClientId",
                schema: "DreamLuso",
                table: "PropertyVisits",
                newName: "IX_PropertyVisits_UserId");

            migrationBuilder.RenameColumn(
                name: "Date",
                schema: "DreamLuso",
                table: "Notifications",
                newName: "UpdateAt");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmationToken",
                schema: "DreamLuso",
                table: "PropertyVisits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedAt",
                schema: "DreamLuso",
                table: "PropertyVisits",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "DreamLuso",
                table: "PropertyVisits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimeSlot",
                schema: "DreamLuso",
                table: "PropertyVisits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "VisitDate",
                schema: "DreamLuso",
                table: "PropertyVisits",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                schema: "DreamLuso",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "DreamLuso",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "DreamLuso",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "DreamLuso",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyVisits_Users_RealStateAgentId",
                schema: "DreamLuso",
                table: "PropertyVisits",
                column: "RealStateAgentId",
                principalSchema: "DreamLuso",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyVisits_Users_UserId",
                schema: "DreamLuso",
                table: "PropertyVisits",
                column: "UserId",
                principalSchema: "DreamLuso",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyVisits_Users_RealStateAgentId",
                schema: "DreamLuso",
                table: "PropertyVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyVisits_Users_UserId",
                schema: "DreamLuso",
                table: "PropertyVisits");

            migrationBuilder.DropColumn(
                name: "ConfirmationToken",
                schema: "DreamLuso",
                table: "PropertyVisits");

            migrationBuilder.DropColumn(
                name: "ConfirmedAt",
                schema: "DreamLuso",
                table: "PropertyVisits");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "DreamLuso",
                table: "PropertyVisits");

            migrationBuilder.DropColumn(
                name: "TimeSlot",
                schema: "DreamLuso",
                table: "PropertyVisits");

            migrationBuilder.DropColumn(
                name: "VisitDate",
                schema: "DreamLuso",
                table: "PropertyVisits");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                schema: "DreamLuso",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "DreamLuso",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "DreamLuso",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "DreamLuso",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "DreamLuso",
                table: "PropertyVisits",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyVisits_UserId",
                schema: "DreamLuso",
                table: "PropertyVisits",
                newName: "IX_PropertyVisits_ClientId");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                schema: "DreamLuso",
                table: "Notifications",
                newName: "Date");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                schema: "DreamLuso",
                table: "PropertyVisits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyVisits_Clients_ClientId",
                schema: "DreamLuso",
                table: "PropertyVisits",
                column: "ClientId",
                principalSchema: "DreamLuso",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyVisits_RealStateAgent_RealStateAgentId",
                schema: "DreamLuso",
                table: "PropertyVisits",
                column: "RealStateAgentId",
                principalSchema: "DreamLuso",
                principalTable: "RealStateAgent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
