using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seatmap.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddStaticImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "static_image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    X = table.Column<decimal>(type: "numeric", nullable: false),
                    Y = table.Column<decimal>(type: "numeric", nullable: false),
                    Width = table.Column<decimal>(type: "numeric", nullable: false),
                    Height = table.Column<decimal>(type: "numeric", nullable: false),
                    Rotation = table.Column<decimal>(type: "numeric", nullable: false),
                    Selected = table.Column<bool>(type: "boolean", nullable: false),
                    LayerId = table.Column<Guid>(type: "uuid", nullable: true),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleVersionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_static_image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_static_image_layer_LayerId",
                        column: x => x.LayerId,
                        principalTable: "layer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_static_image_vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_static_image_vehicle_version_VehicleVersionId",
                        column: x => x.VehicleVersionId,
                        principalTable: "vehicle_version",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_static_image_LayerId",
                table: "static_image",
                column: "LayerId");

            migrationBuilder.CreateIndex(
                name: "IX_static_image_VehicleId",
                table: "static_image",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_static_image_VehicleVersionId",
                table: "static_image",
                column: "VehicleVersionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "static_image");
        }
    }
}
