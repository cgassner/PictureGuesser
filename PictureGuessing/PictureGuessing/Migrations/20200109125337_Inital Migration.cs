using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PictureGuessing.Migrations
{
    public partial class InitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    DifficultyScale = table.Column<float>(nullable: false),
                    rows = table.Column<int>(nullable: false),
                    cols = table.Column<int>(nullable: false),
                    revealDelay = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.DifficultyScale);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    URL = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true),
                    AnswerLength = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DifficultyScale = table.Column<float>(nullable: true),
                    pictureID = table.Column<Guid>(nullable: false),
                    isFinished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_Difficulties_DifficultyScale",
                        column: x => x.DifficultyScale,
                        principalTable: "Difficulties",
                        principalColumn: "DifficultyScale",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_DifficultyScale",
                table: "Game",
                column: "DifficultyScale");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Difficulties");
        }
    }
}
