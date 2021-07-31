using Microsoft.EntityFrameworkCore.Migrations;

namespace SharedTripSystem.Migrations
{
    public partial class CarTripConflict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trips_PlateNumber",
                table: "Trips");

            migrationBuilder.AlterColumn<string>(
                name: "TripId",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_PlateNumber",
                table: "Trips",
                column: "PlateNumber",
                unique: true,
                filter: "[PlateNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_TripId",
                table: "Cars",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Trips_TripId",
                table: "Cars",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Trips_TripId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Trips_PlateNumber",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Cars_TripId",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "TripId",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_PlateNumber",
                table: "Trips",
                column: "PlateNumber");
        }
    }
}
