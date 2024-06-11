using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApnaDhaba_API.Migrations
{
    /// <inheritdoc />
    public partial class createDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "userModels",
                newName: "CreateDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "userModels",
                newName: "DateTime");
        }
    }
}