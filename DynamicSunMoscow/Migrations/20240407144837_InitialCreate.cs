using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicSunMoscow.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temprature = table.Column<float>(type: "real", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    DewPoint = table.Column<float>(type: "real", nullable: false),
                    Pressure = table.Column<int>(type: "int", nullable: false),
                    WindSpeed = table.Column<int>(type: "int", nullable: false),
                    Cloudiness = table.Column<int>(type: "int", nullable: true),
                    CloudBase = table.Column<int>(type: "int", nullable: false),
                    HorizontalVisibility = table.Column<int>(type: "int", nullable: true),
                    HumidityString = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WindDirections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Direction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeatherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WindDirectionId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WindDirections_Weathers_WeatherId",
                        column: x => x.WeatherId,
                        principalTable: "Weathers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WindDirections_WeatherId",
                table: "WindDirections",
                column: "WeatherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WindDirections");

            migrationBuilder.DropTable(
                name: "Weathers");
        }
    }
}
