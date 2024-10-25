using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamLuso.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsForRent",
                schema: "DreamLuso",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "IsForSale",
                schema: "DreamLuso",
                table: "Property");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
