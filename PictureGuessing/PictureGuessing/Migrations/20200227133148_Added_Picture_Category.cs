using Microsoft.EntityFrameworkCore.Migrations;

namespace PictureGuessing.Migrations
{
    public partial class Added_Picture_Category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Pictures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Pictures");
        }
    }
}
