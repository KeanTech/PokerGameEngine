using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameEngine.Migrations
{
    /// <inheritdoc />
    public partial class newmodels8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerPokerTable");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "PlayerPokerTable",
                columns: table => new
                {
                    PlayersId = table.Column<int>(type: "int", nullable: false),
                    TablesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPokerTable", x => new { x.PlayersId, x.TablesId });
                    table.ForeignKey(
                        name: "FK_PlayerPokerTable_Player_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerPokerTable_Table_TablesId",
                        column: x => x.TablesId,
                        principalTable: "Table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPokerTable_TablesId",
                table: "PlayerPokerTable",
                column: "TablesId");
        }
    }
}
