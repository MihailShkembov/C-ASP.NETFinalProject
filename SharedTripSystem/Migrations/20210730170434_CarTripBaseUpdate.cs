using Microsoft.EntityFrameworkCore.Migrations;

namespace SharedTripSystem.Migrations
{
    public partial class CarTripBaseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Cars_CarId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "Trips",
                newName: "PlateNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_CarId",
                table: "Trips",
                newName: "IX_Trips_PlateNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Cars_PlateNumber",
                table: "Trips",
                column: "PlateNumber",
                principalTable: "Cars",
                principalColumn: "PlateNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Cars_PlateNumber",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "PlateNumber",
                table: "Trips",
                newName: "CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_PlateNumber",
                table: "Trips",
                newName: "IX_Trips_CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Cars_CarId",
                table: "Trips",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "PlateNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
