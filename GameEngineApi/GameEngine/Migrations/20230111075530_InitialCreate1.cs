using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameEngine.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessory_User_UserId",
                table: "Accessory");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Table_TableId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "TableId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Accessory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accessory_User_UserId",
                table: "Accessory",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Table_TableId",
                table: "User",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessory_User_UserId",
                table: "Accessory");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Table_TableId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "TableId",
                table: "User",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Accessory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Accessory_User_UserId",
                table: "Accessory",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Table_TableId",
                table: "User",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "Id");
        }
    }
}
