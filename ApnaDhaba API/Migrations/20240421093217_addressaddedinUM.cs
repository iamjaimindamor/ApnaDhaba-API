using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApnaDhaba_API.Migrations
{
    /// <inheritdoc />
    public partial class addressaddedinUM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "userModels",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "userModels");
        }
    }
}