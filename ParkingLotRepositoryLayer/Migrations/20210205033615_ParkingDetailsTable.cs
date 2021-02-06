using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingLotRepositoryLayer.Migrations
{
    public partial class ParkingDetailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingDetails",
                columns: table => new
                {
                    ParkingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VehicleNumber = table.Column<string>(nullable: false),
                    EntryTime = table.Column<DateTime>(nullable: false),
                    ParkingType = table.Column<int>(nullable: false),
                    VehicleType = table.Column<int>(nullable: false),
                    DriverType = table.Column<int>(nullable: false),
                    ExitTime = table.Column<DateTime>(nullable: false),
                    ParkingSlotNumber = table.Column<string>(nullable: false),
                    Charges = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingDetails", x => x.ParkingID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingDetails");
        }
    }
}
