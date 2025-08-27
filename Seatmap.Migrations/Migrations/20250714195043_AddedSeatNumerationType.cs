using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seatmap.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeatNumerationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeatNumerationType",
                table: "vehicle_version",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeatNumerationType",
                table: "vehicle_version");
        }
    }
}
