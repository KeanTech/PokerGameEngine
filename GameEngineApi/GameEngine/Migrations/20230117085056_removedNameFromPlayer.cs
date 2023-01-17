using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameEngine.Migrations
{
    /// <inheritdoc />
    public partial class removedNameFromPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Player");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Player",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
