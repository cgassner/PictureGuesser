using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PictureGuessing.Migrations
{
    public partial class ChangesforLeaderboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerLength",
                table: "Pictures");

            migrationBuilder.AddColumn<DateTime>(
                name: "Endtime",
                table: "Game",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Game",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endtime",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Game");

            migrationBuilder.AddColumn<int>(
                name: "AnswerLength",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
