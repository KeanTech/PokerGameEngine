using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameEngine.Migrations
{
    /// <inheritdoc />
    public partial class newmodels5 : Migration
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
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentBet = table.Column<int>(type: "int", nullable: false),
                    Chips = table.Column<int>(type: "int", nullable: false),
                    IsFolded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
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
                    table.ForeignKey(
                        name: "FK_CardPlayer_Player_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardPlayer_CardsSymbol_CardsType",
                table: "CardPlayer",
                columns: new[] { "CardsSymbol", "CardsType" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardPlayer");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
