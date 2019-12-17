using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PictureGuessing.Migrations
{
    public partial class NewPictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Difficulty_DifficultyId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Picture_PictureId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_PictureId",
                table: "Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Picture",
                table: "Picture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Difficulty",
                table: "Difficulty");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Picture");

            migrationBuilder.RenameTable(
                name: "Picture",
                newName: "Pictures");

            migrationBuilder.RenameTable(
                name: "Difficulty",
                newName: "Difficulties");

            migrationBuilder.RenameColumn(
                name: "PictureId",
                table: "Game",
                newName: "pictureID");

            migrationBuilder.AlterColumn<Guid>(
                name: "pictureID",
                table: "Game",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Pictures",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pictures",
                table: "Pictures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Difficulties",
                table: "Difficulties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Difficulties_DifficultyId",
                table: "Game",
                column: "DifficultyId",
                principalTable: "Difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Difficulties_DifficultyId",
                table: "Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pictures",
                table: "Pictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Difficulties",
                table: "Difficulties");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "Pictures");

            migrationBuilder.RenameTable(
                name: "Pictures",
                newName: "Picture");

            migrationBuilder.RenameTable(
                name: "Difficulties",
                newName: "Difficulty");

            migrationBuilder.RenameColumn(
                name: "pictureID",
                table: "Game",
                newName: "PictureId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PictureId",
                table: "Game",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Picture",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Picture",
                table: "Picture",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Difficulty",
                table: "Difficulty",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Game_PictureId",
                table: "Game",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Difficulty_DifficultyId",
                table: "Game",
                column: "DifficultyId",
                principalTable: "Difficulty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Picture_PictureId",
                table: "Game",
                column: "PictureId",
                principalTable: "Picture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
