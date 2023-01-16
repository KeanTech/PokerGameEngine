using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameEngine.Migrations
{
    /// <inheritdoc />
    public partial class newmodels6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chips = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardPokerTable",
                columns: table => new
                {
                    TablesId = table.Column<int>(type: "int", nullable: false),
                    CardsSymbol = table.Column<int>(type: "int", nullable: false),
                    CardsType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPokerTable", x => new { x.TablesId, x.CardsSymbol, x.CardsType });
                    table.ForeignKey(
                        name: "FK_CardPokerTable_Card_CardsSymbol_CardsType",
                        columns: x => new { x.CardsSymbol, x.CardsType },
                        principalTable: "Card",
                        principalColumns: new[] { "Symbol", "Type" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardPokerTable_Table_TablesId",
                        column: x => x.TablesId,
                        principalTable: "Table",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_CardPokerTable_CardsSymbol_CardsType",
                table: "CardPokerTable",
                columns: new[] { "CardsSymbol", "CardsType" });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPokerTable_TablesId",
                table: "PlayerPokerTable",
                column: "TablesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardPokerTable");

            migrationBuilder.DropTable(
                name: "PlayerPokerTable");

            migrationBuilder.DropTable(
                name: "Table");
        }
    }
}
