using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamLuso.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainImage",
                schema: "DreamLuso",
                table: "PropertyImages");

            migrationBuilder.DropColumn(
                name: "CoolingSystem",
                schema: "DreamLuso",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "HeatingSystem",
                schema: "DreamLuso",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "OwnerInformation",
                schema: "DreamLuso",
                table: "Property");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMainImage",
                schema: "DreamLuso",
                table: "PropertyImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CoolingSystem",
                schema: "DreamLuso",
                table: "Property",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HeatingSystem",
                schema: "DreamLuso",
                table: "Property",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerInformation",
                schema: "DreamLuso",
                table: "Property",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
