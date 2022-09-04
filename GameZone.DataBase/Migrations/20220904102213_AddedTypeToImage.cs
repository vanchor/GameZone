using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameZone.DataBase.Migrations
{
    public partial class AddedTypeToImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Images");
        }
    }
}
