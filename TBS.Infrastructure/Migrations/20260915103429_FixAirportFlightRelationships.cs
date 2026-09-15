using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TBS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAirportFlightRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Airlines",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Airlines");
        }
    }
}
