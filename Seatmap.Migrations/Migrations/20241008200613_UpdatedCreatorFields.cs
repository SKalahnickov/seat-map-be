using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seatmap.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCreatorFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CreatorKey",
                table: "vehicle_version");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "vehicle_version",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleVersionId",
                table: "seat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleVersionId",
                table: "layer",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleVersionId",
                table: "graphic_element",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_seat_VehicleVersionId",
                table: "seat",
                column: "VehicleVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_layer_VehicleVersionId",
                table: "layer",
                column: "VehicleVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_graphic_element_VehicleVersionId",
                table: "graphic_element",
                column: "VehicleVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_graphic_element_vehicle_VehicleId",
                table: "graphic_element",
                column: "VehicleId",
                principalTable: "vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_graphic_element_vehicle_version_VehicleVersionId",
                table: "graphic_element",
                column: "VehicleVersionId",
                principalTable: "vehicle_version",
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
                name: "FK_layer_vehicle_version_VehicleVersionId",
                table: "layer",
                column: "VehicleVersionId",
                principalTable: "vehicle_version",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_seat_vehicle_VehicleId",
                table: "seat",
                column: "VehicleId",
                principalTable: "vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_seat_vehicle_version_VehicleVersionId",
                table: "seat",
                column: "VehicleVersionId",
                principalTable: "vehicle_version",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_graphic_element_vehicle_VehicleId",
                table: "graphic_element");

            migrationBuilder.DropForeignKey(
                name: "FK_graphic_element_vehicle_version_VehicleVersionId",
                table: "graphic_element");

            migrationBuilder.DropForeignKey(
                name: "FK_layer_vehicle_VehicleId",
                table: "layer");

            migrationBuilder.DropForeignKey(
                name: "FK_layer_vehicle_version_VehicleVersionId",
                table: "layer");

            migrationBuilder.DropForeignKey(
                name: "FK_seat_vehicle_VehicleId",
                table: "seat");

            migrationBuilder.DropForeignKey(
                name: "FK_seat_vehicle_version_VehicleVersionId",
                table: "seat");

            migrationBuilder.DropIndex(
                name: "IX_seat_VehicleVersionId",
                table: "seat");

            migrationBuilder.DropIndex(
                name: "IX_layer_VehicleVersionId",
                table: "layer");

            migrationBuilder.DropIndex(
                name: "IX_graphic_element_VehicleVersionId",
                table: "graphic_element");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "vehicle_version");

            migrationBuilder.DropColumn(
                name: "VehicleVersionId",
                table: "seat");

            migrationBuilder.DropColumn(
                name: "VehicleVersionId",
                table: "layer");

            migrationBuilder.DropColumn(
                name: "VehicleVersionId",
                table: "graphic_element");

            migrationBuilder.AddColumn<string>(
                name: "CreatorKey",
                table: "vehicle_version",
                type: "text",
                nullable: true);

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
    }
}
