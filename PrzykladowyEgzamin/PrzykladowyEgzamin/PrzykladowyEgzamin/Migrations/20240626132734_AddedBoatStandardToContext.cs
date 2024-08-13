using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrzykladowyEgzamin.Migrations
{
    /// <inheritdoc />
    public partial class AddedBoatStandardToContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_BoatStandard_IdBoatStandard",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Sailboats_BoatStandard_IdBoatStandard",
                table: "Sailboats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoatStandard",
                table: "BoatStandard");

            migrationBuilder.RenameTable(
                name: "BoatStandard",
                newName: "BoatStandards");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoatStandards",
                table: "BoatStandards",
                column: "IdBoatStandard");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_BoatStandards_IdBoatStandard",
                table: "Reservations",
                column: "IdBoatStandard",
                principalTable: "BoatStandards",
                principalColumn: "IdBoatStandard",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sailboats_BoatStandards_IdBoatStandard",
                table: "Sailboats",
                column: "IdBoatStandard",
                principalTable: "BoatStandards",
                principalColumn: "IdBoatStandard",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_BoatStandards_IdBoatStandard",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Sailboats_BoatStandards_IdBoatStandard",
                table: "Sailboats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoatStandards",
                table: "BoatStandards");

            migrationBuilder.RenameTable(
                name: "BoatStandards",
                newName: "BoatStandard");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoatStandard",
                table: "BoatStandard",
                column: "IdBoatStandard");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_BoatStandard_IdBoatStandard",
                table: "Reservations",
                column: "IdBoatStandard",
                principalTable: "BoatStandard",
                principalColumn: "IdBoatStandard",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sailboats_BoatStandard_IdBoatStandard",
                table: "Sailboats",
                column: "IdBoatStandard",
                principalTable: "BoatStandard",
                principalColumn: "IdBoatStandard",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
