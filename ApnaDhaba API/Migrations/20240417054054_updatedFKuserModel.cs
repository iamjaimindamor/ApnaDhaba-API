using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApnaDhaba_API.Migrations
{
    /// <inheritdoc />
    public partial class updatedFKuserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "roleName",
                table: "assignedRoles");

            migrationBuilder.AddColumn<int>(
                name: "userId1",
                table: "assignedRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_assignedRoles_userId1",
                table: "assignedRoles",
                column: "userId1");

            migrationBuilder.AddForeignKey(
                name: "FK_assignedRoles_userModels_userId1",
                table: "assignedRoles",
                column: "userId1",
                principalTable: "userModels",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assignedRoles_userModels_userId1",
                table: "assignedRoles");

            migrationBuilder.DropIndex(
                name: "IX_assignedRoles_userId1",
                table: "assignedRoles");

            migrationBuilder.DropColumn(
                name: "userId1",
                table: "assignedRoles");

            migrationBuilder.AddColumn<string>(
                name: "roleName",
                table: "assignedRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}