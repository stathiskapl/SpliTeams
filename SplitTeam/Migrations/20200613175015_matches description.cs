using Microsoft.EntityFrameworkCore.Migrations;

namespace SplitTeam.Migrations
{
    public partial class matchesdescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Matches",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Matches");
        }
    }
}
