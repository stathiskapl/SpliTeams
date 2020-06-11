using Microsoft.EntityFrameworkCore.Migrations;

namespace SplitTeam.Migrations
{
    public partial class averageRank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AverageRank",
                table: "Players",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageRank",
                table: "Players");
        }
    }
}
