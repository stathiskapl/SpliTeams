using Microsoft.EntityFrameworkCore.Migrations;

namespace SplitTeam.Migrations
{
    public partial class addusertoplayerranks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PlayerRanks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRanks_UserId",
                table: "PlayerRanks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerRanks_Users_UserId",
                table: "PlayerRanks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerRanks_Users_UserId",
                table: "PlayerRanks");

            migrationBuilder.DropIndex(
                name: "IX_PlayerRanks_UserId",
                table: "PlayerRanks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PlayerRanks");
        }
    }
}
