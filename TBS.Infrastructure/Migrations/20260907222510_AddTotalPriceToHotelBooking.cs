using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TBS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalPriceToHotelBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceForNight",
                table: "HotelBookingRooms",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceForNight",
                table: "HotelBookingRooms");
        }
    }
}
