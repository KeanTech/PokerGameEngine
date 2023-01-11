using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameEngine.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chip", x => x.Id);
                });

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
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false),
                    UserIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    ChipsAquired = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentBet = table.Column<int>(type: "int", nullable: true),
                    Chips = table.Column<int>(type: "int", nullable: true),
                    TableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Table_TableId",
                        column: x => x.TableId,
                        principalTable: "Table",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Accessory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accessory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: true),
                    TableId = table.Column<int>(type: "int", nullable: true),
                    TableId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_Table_TableId",
                        column: x => x.TableId,
                        principalTable: "Table",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Card_Table_TableId1",
                        column: x => x.TableId1,
                        principalTable: "Table",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Card_User_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accessory_UserId",
                table: "Accessory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_PlayerId",
                table: "Card",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_TableId",
                table: "Card",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_TableId1",
                table: "Card",
                column: "TableId1");

            migrationBuilder.CreateIndex(
                name: "IX_User_TableId",
                table: "User",
                column: "TableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accessory");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Chip");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Table");
        }
    }
}
