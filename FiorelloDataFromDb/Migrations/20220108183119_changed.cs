using Microsoft.EntityFrameworkCore.Migrations;

namespace FiorelloDataFromDb.Migrations
{
    public partial class changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Flowers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "Flowers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
