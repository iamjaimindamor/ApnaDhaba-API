using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApnaDhaba_API.Migrations
{
    /// <inheritdoc />
    public partial class modifiedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "userModels",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "userModels");
        }
    }
}