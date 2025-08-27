using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seatmap.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddedBufferedSeats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "buffered_seat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SeatId = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleVersionId = table.Column<Guid>(type: "uuid", nullable: false),
                    GraphicElementId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buffered_seat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_buffered_seat_vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_buffered_seat_vehicle_version_VehicleVersionId",
                        column: x => x.VehicleVersionId,
                        principalTable: "vehicle_version",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_buffered_seat_VehicleId",
                table: "buffered_seat",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_buffered_seat_VehicleVersionId",
                table: "buffered_seat",
                column: "VehicleVersionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "buffered_seat");
        }
    }
}
