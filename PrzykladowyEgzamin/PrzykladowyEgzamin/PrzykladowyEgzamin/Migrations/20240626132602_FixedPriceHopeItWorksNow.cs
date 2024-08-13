using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrzykladowyEgzamin.Migrations
{
    /// <inheritdoc />
    public partial class FixedPriceHopeItWorksNow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoatStandard",
                columns: table => new
                {
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatStandard", x => x.IdBoatStandard);
                });

            migrationBuilder.CreateTable(
                name: "ClientCategories",
                columns: table => new
                {
                    IdClientCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiscountPerc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCategories", x => x.IdClientCategory);
                });

            migrationBuilder.CreateTable(
                name: "Sailboats",
                columns: table => new
                {
                    IdSailboat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sailboats", x => x.IdSailboat);
                    table.ForeignKey(
                        name: "FK_Sailboats_BoatStandard_IdBoatStandard",
                        column: x => x.IdBoatStandard,
                        principalTable: "BoatStandard",
                        principalColumn: "IdBoatStandard",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdClientCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.IdClient);
                    table.ForeignKey(
                        name: "FK_Clients_ClientCategories_IdClientCategory",
                        column: x => x.IdClientCategory,
                        principalTable: "ClientCategories",
                        principalColumn: "IdClientCategory",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    IdReservation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    NumOfBoats = table.Column<int>(type: "int", nullable: false),
                    Fulfilled = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CancelReason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false),
                    SailboatIdSailboat = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.IdReservation);
                    table.ForeignKey(
                        name: "FK_Reservations_BoatStandard_IdBoatStandard",
                        column: x => x.IdBoatStandard,
                        principalTable: "BoatStandard",
                        principalColumn: "IdBoatStandard",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Sailboats_SailboatIdSailboat",
                        column: x => x.SailboatIdSailboat,
                        principalTable: "Sailboats",
                        principalColumn: "IdSailboat");
                });

            migrationBuilder.CreateTable(
                name: "ClientReservation",
                columns: table => new
                {
                    ClientsIdClient = table.Column<int>(type: "int", nullable: false),
                    IdClient = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientReservation", x => new { x.ClientsIdClient, x.IdClient });
                    table.ForeignKey(
                        name: "FK_ClientReservation_Clients_ClientsIdClient",
                        column: x => x.ClientsIdClient,
                        principalTable: "Clients",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientReservation_Reservations_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Reservations",
                        principalColumn: "IdReservation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientReservation_IdClient",
                table: "ClientReservation",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_IdClientCategory",
                table: "Clients",
                column: "IdClientCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_IdBoatStandard",
                table: "Reservations",
                column: "IdBoatStandard");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SailboatIdSailboat",
                table: "Reservations",
                column: "SailboatIdSailboat");

            migrationBuilder.CreateIndex(
                name: "IX_Sailboats_IdBoatStandard",
                table: "Sailboats",
                column: "IdBoatStandard");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientReservation");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "ClientCategories");

            migrationBuilder.DropTable(
                name: "Sailboats");

            migrationBuilder.DropTable(
                name: "BoatStandard");
        }
    }
}
