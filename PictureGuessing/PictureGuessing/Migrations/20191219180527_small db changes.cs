using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PictureGuessing.Migrations
{
    public partial class smalldbchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Difficulties_DifficultyId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_DifficultyId",
                table: "Game");

            migrationBuilder.RenameColumn(
                name: "DifficultyId",
                table: "Game",
                newName: "difficultyID");

            migrationBuilder.AddColumn<int>(
                name: "AnswerLength",
                table: "Pictures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "difficultyID",
                table: "Game",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerLength",
                table: "Pictures");

            migrationBuilder.RenameColumn(
                name: "difficultyID",
                table: "Game",
                newName: "DifficultyId");

            migrationBuilder.AlterColumn<Guid>(
                name: "DifficultyId",
                table: "Game",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Game_DifficultyId",
                table: "Game",
                column: "DifficultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Difficulties_DifficultyId",
                table: "Game",
                column: "DifficultyId",
                principalTable: "Difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
