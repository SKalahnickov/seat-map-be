using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seatmap.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddedVehicleVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_graphic_element_vehicle_VehicleId",
                table: "graphic_element");

            migrationBuilder.DropForeignKey(
                name: "FK_layer_vehicle_VehicleId",
                table: "layer");

            migrationBuilder.DropForeignKey(
                name: "FK_seat_vehicle_VehicleId",
                table: "seat");

            migrationBuilder.DropColumn(
                name: "IsDraft",
                table: "vehicle");

            migrationBuilder.AddColumn<string>(
                name: "SelectionKey",
                table: "attribute_selection",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectionValue",
                table: "attribute_selection",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "vehicle_version",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDraft = table.Column<bool>(type: "boolean", nullable: false),
                    CreatorKey = table.Column<string>(type: "text", nullable: true),
                    GridSize = table.Column<decimal>(type: "numeric", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_version", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vehicle_version_vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_version_VehicleId",
                table: "vehicle_version",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_graphic_element_vehicle_version_VehicleId",
                table: "graphic_element",
                column: "VehicleId",
                principalTable: "vehicle_version",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_layer_vehicle_version_VehicleId",
                table: "layer",
                column: "VehicleId",
                principalTable: "vehicle_version",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_seat_vehicle_version_VehicleId",
                table: "seat",
                column: "VehicleId",
                principalTable: "vehicle_version",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_graphic_element_vehicle_version_VehicleId",
                table: "graphic_element");

            migrationBuilder.DropForeignKey(
                name: "FK_layer_vehicle_version_VehicleId",
                table: "layer");

            migrationBuilder.DropForeignKey(
                name: "FK_seat_vehicle_version_VehicleId",
                table: "seat");

            migrationBuilder.DropTable(
                name: "vehicle_version");

            migrationBuilder.DropColumn(
                name: "SelectionKey",
                table: "attribute_selection");

            migrationBuilder.DropColumn(
                name: "SelectionValue",
                table: "attribute_selection");

            migrationBuilder.AddColumn<bool>(
                name: "IsDraft",
                table: "vehicle",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_graphic_element_vehicle_VehicleId",
                table: "graphic_element",
                column: "VehicleId",
                principalTable: "vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_layer_vehicle_VehicleId",
                table: "layer",
                column: "VehicleId",
                principalTable: "vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_seat_vehicle_VehicleId",
                table: "seat",
                column: "VehicleId",
                principalTable: "vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
