using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PictureGuessing.Migrations
{
    public partial class LeaderboardChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "LeaderboardEntries",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Game",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameId",
                table: "LeaderboardEntries");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Game");
        }
    }
}
