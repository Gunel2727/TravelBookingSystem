using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TBS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixBookingDateDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "HotelBookings",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "HotelBookings");
        }
    }
}
