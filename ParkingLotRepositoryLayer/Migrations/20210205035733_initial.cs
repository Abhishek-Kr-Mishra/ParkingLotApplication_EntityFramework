using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingLotRepositoryLayer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriverTypeDetails",
                columns: table => new
                {
                    DriverTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DriverType = table.Column<string>(nullable: false),
                    DriverCharges = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverTypeDetails", x => x.DriverTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ParkingTypeDetails",
                columns: table => new
                {
                    ParkingTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParkingType = table.Column<string>(nullable: false),
                    Charges = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingTypeDetails", x => x.ParkingTypeID);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmailID = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypeDetails",
                columns: table => new
                {
                    VehicleTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VehicleType = table.Column<string>(nullable: false),
                    VehicleCharges = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypeDetails", x => x.VehicleTypeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingDetails_ParkingType",
                table: "ParkingDetails",
                column: "ParkingType");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingDetails_ParkingTypeDetails_ParkingType",
                table: "ParkingDetails",
                column: "ParkingType",
                principalTable: "ParkingTypeDetails",
                principalColumn: "ParkingTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingDetails_ParkingTypeDetails_ParkingType",
                table: "ParkingDetails");

            migrationBuilder.DropTable(
                name: "DriverTypeDetails");

            migrationBuilder.DropTable(
                name: "ParkingTypeDetails");

            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.DropTable(
                name: "VehicleTypeDetails");

            migrationBuilder.DropIndex(
                name: "IX_ParkingDetails_ParkingType",
                table: "ParkingDetails");
        }
    }
}
