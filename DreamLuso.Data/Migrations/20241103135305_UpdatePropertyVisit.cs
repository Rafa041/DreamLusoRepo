using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamLuso.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropertyVisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyVisits_Users_RealStateAgentId",
                schema: "DreamLuso",
                table: "PropertyVisits");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyVisits_RealStateAgent_RealStateAgentId",
                schema: "DreamLuso",
                table: "PropertyVisits");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyVisits_Users_RealStateAgentId",
                schema: "DreamLuso",
                table: "PropertyVisits",
                column: "RealStateAgentId",
                principalSchema: "DreamLuso",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
