using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Users_OwnerId",
                table: "Leads");

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Marketers_OwnerId",
                table: "Leads",
                column: "OwnerId",
                principalTable: "Marketers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Marketers_OwnerId",
                table: "Leads");

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Users_OwnerId",
                table: "Leads",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
