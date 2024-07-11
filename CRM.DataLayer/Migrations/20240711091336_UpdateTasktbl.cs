using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTasktbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskStatus",
                table: "CrmTasks",
                newName: "CrmTaskStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CrmTaskStatus",
                table: "CrmTasks",
                newName: "TaskStatus");
        }
    }
}
