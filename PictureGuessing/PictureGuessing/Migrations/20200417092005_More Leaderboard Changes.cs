using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PictureGuessing.Migrations
{
    public partial class MoreLeaderboardChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaderboardEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DifficultyScale = table.Column<float>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    TimeInSeconds = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderboardEntries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaderboardEntries");
        }
    }
}
