using Microsoft.EntityFrameworkCore.Migrations;

namespace FiorelloDataFromDb.Migrations
{
    public partial class SettingTabl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(maxLength: 200, nullable: false),
                    SearchIcon = table.Column<string>(nullable: false),
                    BasketIcon = table.Column<string>(nullable: false),
                    InstagramUrl = table.Column<string>(maxLength: 150, nullable: true),
                    FacebookUrl = table.Column<string>(maxLength: 150, nullable: true),
                    Parallax = table.Column<string>(maxLength: 150, nullable: false),
                    ParallaxTitle = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
