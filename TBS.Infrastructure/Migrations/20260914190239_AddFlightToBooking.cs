using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TBS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFlightToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FlightId",
                table: "FlightBookings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FlightBookings_FlightId",
                table: "FlightBookings",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightBookings_Flights_FlightId",
                table: "FlightBookings",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightBookings_Flights_FlightId",
                table: "FlightBookings");

            migrationBuilder.DropIndex(
                name: "IX_FlightBookings_FlightId",
                table: "FlightBookings");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "FlightBookings");
        }
    }
}
