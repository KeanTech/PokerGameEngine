using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameEngine.Migrations
{
    /// <inheritdoc />
    public partial class newmodels11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerPlayerId",
                table: "Table",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerPlayerId",
                table: "Table");
        }
    }
}
