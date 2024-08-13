using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrzykladowyEgzamin.Migrations
{
    /// <inheritdoc />
    public partial class AddedHttpGetEndpointTaskOneFinished : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateFrom",
                table: "Reservations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateTo",
                table: "Reservations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFrom",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "DateTo",
                table: "Reservations");
        }
    }
}
