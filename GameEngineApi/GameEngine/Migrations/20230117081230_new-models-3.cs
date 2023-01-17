using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameEngine.Migrations
{
    /// <inheritdoc />
    public partial class newmodels3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Subscribe_TableId",
                table: "Subscribe",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribe_Table_TableId",
                table: "Subscribe",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscribe_Table_TableId",
                table: "Subscribe");

            migrationBuilder.DropIndex(
                name: "IX_Subscribe_TableId",
                table: "Subscribe");
        }
    }
}
