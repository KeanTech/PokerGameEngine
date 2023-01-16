using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameEngine.Migrations
{
    /// <inheritdoc />
    public partial class newmodels13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Table_TableId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_TableId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "OwnerPlayerId",
                table: "Table",
                newName: "OwnerId");

            migrationBuilder.AddColumn<int>(
                name: "PokerTableId",
                table: "Player",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Table_OwnerId",
                table: "Table",
                column: "OwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_PokerTableId",
                table: "Player",
                column: "PokerTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Table_PokerTableId",
                table: "Player",
                column: "PokerTableId",
                principalTable: "Table",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Table_Player_OwnerId",
                table: "Table",
                column: "OwnerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Table_PokerTableId",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_Player_OwnerId",
                table: "Table");

            migrationBuilder.DropIndex(
                name: "IX_Table_OwnerId",
                table: "Table");

            migrationBuilder.DropIndex(
                name: "IX_Player_PokerTableId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "PokerTableId",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Table",
                newName: "OwnerPlayerId");

            migrationBuilder.AddColumn<int>(
                name: "TableId",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Player_TableId",
                table: "Player",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Table_TableId",
                table: "Player",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
