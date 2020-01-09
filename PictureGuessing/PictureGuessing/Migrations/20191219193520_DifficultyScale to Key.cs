using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PictureGuessing.Migrations
{
    public partial class DifficultyScaletoKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Difficulties",
                table: "Difficulties");

            migrationBuilder.DropColumn(
                name: "difficultyID",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Difficulties");

            migrationBuilder.AddColumn<float>(
                name: "DifficultyScale",
                table: "Game",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Difficulties",
                table: "Difficulties",
                column: "DifficultyScale");

            migrationBuilder.CreateIndex(
                name: "IX_Game_DifficultyScale",
                table: "Game",
                column: "DifficultyScale");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Difficulties_DifficultyScale",
                table: "Game",
                column: "DifficultyScale",
                principalTable: "Difficulties",
                principalColumn: "DifficultyScale",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Difficulties_DifficultyScale",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_DifficultyScale",
                table: "Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Difficulties",
                table: "Difficulties");

            migrationBuilder.DropColumn(
                name: "DifficultyScale",
                table: "Game");

            migrationBuilder.AddColumn<Guid>(
                name: "difficultyID",
                table: "Game",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Difficulties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Difficulties",
                table: "Difficulties",
                column: "Id");
        }
    }
}
