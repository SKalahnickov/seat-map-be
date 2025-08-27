using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seatmap.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddUnrotateNumbersToVehicleVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UnrotateNumbers",
                table: "vehicle_version",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnrotateNumbers",
                table: "vehicle_version");
        }
    }
}
