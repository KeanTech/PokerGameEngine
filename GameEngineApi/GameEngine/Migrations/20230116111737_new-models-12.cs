using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameEngine.Migrations
{
    /// <inheritdoc />
    public partial class newmodels12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Player_UserId",
                table: "Player",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_User_UserId",
                table: "Player",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_User_UserId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_UserId",
                table: "Player");
        }
    }
}
