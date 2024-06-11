using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApnaDhaba_API.Migrations
{
    /// <inheritdoc />
    public partial class datatypechanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assignedRoles_roleModels_roleId1",
                table: "assignedRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_assignedRoles_userModels_userId1",
                table: "assignedRoles");

            migrationBuilder.DropIndex(
                name: "IX_assignedRoles_roleId1",
                table: "assignedRoles");

            migrationBuilder.DropIndex(
                name: "IX_assignedRoles_userId1",
                table: "assignedRoles");

            migrationBuilder.RenameColumn(
                name: "userId1",
                table: "assignedRoles",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "roleId1",
                table: "assignedRoles",
                newName: "roleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "assignedRoles",
                newName: "userId1");

            migrationBuilder.RenameColumn(
                name: "roleId",
                table: "assignedRoles",
                newName: "roleId1");

            migrationBuilder.CreateIndex(
                name: "IX_assignedRoles_roleId1",
                table: "assignedRoles",
                column: "roleId1");

            migrationBuilder.CreateIndex(
                name: "IX_assignedRoles_userId1",
                table: "assignedRoles",
                column: "userId1");

            migrationBuilder.AddForeignKey(
                name: "FK_assignedRoles_roleModels_roleId1",
                table: "assignedRoles",
                column: "roleId1",
                principalTable: "roleModels",
                principalColumn: "roleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_assignedRoles_userModels_userId1",
                table: "assignedRoles",
                column: "userId1",
                principalTable: "userModels",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}