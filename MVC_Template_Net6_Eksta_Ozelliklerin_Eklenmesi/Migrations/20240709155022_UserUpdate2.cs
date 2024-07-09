using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Migrations
{
    public partial class UserUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "ProfilImageFileName",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValue: "no_image.jpeg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilImageFileName",
                table: "Users");

        }
    }
}
