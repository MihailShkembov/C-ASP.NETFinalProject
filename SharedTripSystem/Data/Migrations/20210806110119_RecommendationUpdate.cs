using Microsoft.EntityFrameworkCore.Migrations;

namespace SharedTripSystem.Migrations
{
    public partial class RecommendationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descriptop",
                table: "Recommendations",
                newName: "Descripton");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripton",
                table: "Recommendations",
                newName: "Descriptop");
        }
    }
}
