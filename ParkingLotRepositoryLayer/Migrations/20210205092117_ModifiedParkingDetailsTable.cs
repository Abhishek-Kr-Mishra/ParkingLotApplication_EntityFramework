using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingLotRepositoryLayer.Migrations
{
    public partial class ModifiedParkingDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmpty",
                table: "ParkingDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingDetails_DriverType",
                table: "ParkingDetails",
                column: "DriverType");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingDetails_VehicleType",
                table: "ParkingDetails",
                column: "VehicleType");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingDetails_DriverTypeDetails_DriverType",
                table: "ParkingDetails",
                column: "DriverType",
                principalTable: "DriverTypeDetails",
                principalColumn: "DriverTypeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingDetails_VehicleTypeDetails_VehicleType",
                table: "ParkingDetails",
                column: "VehicleType",
                principalTable: "VehicleTypeDetails",
                principalColumn: "VehicleTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingDetails_DriverTypeDetails_DriverType",
                table: "ParkingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingDetails_VehicleTypeDetails_VehicleType",
                table: "ParkingDetails");

            migrationBuilder.DropIndex(
                name: "IX_ParkingDetails_DriverType",
                table: "ParkingDetails");

            migrationBuilder.DropIndex(
                name: "IX_ParkingDetails_VehicleType",
                table: "ParkingDetails");

            migrationBuilder.DropColumn(
                name: "IsEmpty",
                table: "ParkingDetails");
        }
    }
}
