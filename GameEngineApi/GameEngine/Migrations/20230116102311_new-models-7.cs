using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameEngine.Migrations
{
    /// <inheritdoc />
    public partial class newmodels7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeckId",
                table: "Table",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Table_DeckId",
                table: "Table",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_CardDeck_CardsSymbol_CardsType",
                table: "CardDeck",
                columns: new[] { "CardsSymbol", "CardsType" });

            migrationBuilder.AddForeignKey(
                name: "FK_Table_Deck_DeckId",
                table: "Table",
                column: "DeckId",
                principalTable: "Deck",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Table_Deck_DeckId",
                table: "Table");

            migrationBuilder.DropTable(
                name: "CardDeck");

            migrationBuilder.DropTable(
                name: "Deck");

            migrationBuilder.DropIndex(
                name: "IX_Table_DeckId",
                table: "Table");

            migrationBuilder.DropColumn(
                name: "DeckId",
                table: "Table");
        }
    }
}
