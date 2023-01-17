using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameEngine.Migrations
{
    /// <inheritdoc />
    public partial class newmodels1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Symbol = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => new { x.Symbol, x.Type });
                });

            migrationBuilder.CreateTable(
                name: "Deck",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deck", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    ChipsAquired = table.Column<int>(type: "int", nullable: false),
                    UserSecret = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardDeck",
                columns: table => new
                {
                    DecksId = table.Column<int>(type: "int", nullable: false),
                    CardsSymbol = table.Column<int>(type: "int", nullable: false),
                    CardsType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardDeck", x => new { x.DecksId, x.CardsSymbol, x.CardsType });
                    table.ForeignKey(
                        name: "FK_CardDeck_Card_CardsSymbol_CardsType",
                        columns: x => new { x.CardsSymbol, x.CardsType },
                        principalTable: "Card",
                        principalColumns: new[] { "Symbol", "Type" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardDeck_Deck_DecksId",
                        column: x => x.DecksId,
                        principalTable: "Deck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accessory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accessory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardPlayer",
                columns: table => new
                {
                    PlayersId = table.Column<int>(type: "int", nullable: false),
                    CardsSymbol = table.Column<int>(type: "int", nullable: false),
                    CardsType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPlayer", x => new { x.PlayersId, x.CardsSymbol, x.CardsType });
                    table.ForeignKey(
                        name: "FK_CardPlayer_Card_CardsSymbol_CardsType",
                        columns: x => new { x.CardsSymbol, x.CardsType },
                        principalTable: "Card",
                        principalColumns: new[] { "Symbol", "Type" },
                        onDelete: ReferentialAction.Cascade);
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
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentBet = table.Column<int>(type: "int", nullable: false),
                    Chips = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsFolded = table.Column<bool>(type: "bit", nullable: false),
                    PokerTableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    DeckId = table.Column<int>(type: "int", nullable: false),
                    Chips = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Table_Deck_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Deck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Table_Player_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accessory_UserId",
                table: "Accessory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CardDeck_CardsSymbol_CardsType",
                table: "CardDeck",
                columns: new[] { "CardsSymbol", "CardsType" });

            migrationBuilder.CreateIndex(
                name: "IX_CardPlayer_CardsSymbol_CardsType",
                table: "CardPlayer",
                columns: new[] { "CardsSymbol", "CardsType" });

            migrationBuilder.CreateIndex(
                name: "IX_CardPokerTable_CardsSymbol_CardsType",
                table: "CardPokerTable",
                columns: new[] { "CardsSymbol", "CardsType" });

            migrationBuilder.CreateIndex(
                name: "IX_Player_PokerTableId",
                table: "Player",
                column: "PokerTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_UserId",
                table: "Player",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_DeckId",
                table: "Table",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_OwnerId",
                table: "Table",
                column: "OwnerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CardPlayer_Player_PlayersId",
                table: "CardPlayer",
                column: "PlayersId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CardPokerTable_Table_TablesId",
                table: "CardPokerTable",
                column: "TablesId",
                principalTable: "Table",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Table_PokerTableId",
                table: "Player",
                column: "PokerTableId",
                principalTable: "Table",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_User_UserId",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_Deck_DeckId",
                table: "Table");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_Player_OwnerId",
                table: "Table");

            migrationBuilder.DropTable(
                name: "Accessory");

            migrationBuilder.DropTable(
                name: "CardDeck");

            migrationBuilder.DropTable(
                name: "CardPlayer");

            migrationBuilder.DropTable(
                name: "CardPokerTable");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Deck");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Table");
        }
    }
}
