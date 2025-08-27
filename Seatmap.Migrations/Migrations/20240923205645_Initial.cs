using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seatmap.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attribute_selection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeId = table.Column<string>(type: "text", nullable: true),
                    RelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeName = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    StringValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attribute_selection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vehicle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDraft = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "layer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_layer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_layer_vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "graphic_element",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    X = table.Column<decimal>(type: "numeric", nullable: false),
                    Y = table.Column<decimal>(type: "numeric", nullable: false),
                    DefaultWidth = table.Column<decimal>(type: "numeric", nullable: false),
                    DefaultHeight = table.Column<decimal>(type: "numeric", nullable: false),
                    RotationDeg = table.Column<decimal>(type: "numeric", nullable: false),
                    AdjustedX = table.Column<decimal>(type: "numeric", nullable: false),
                    AdjustedY = table.Column<decimal>(type: "numeric", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    LayerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_graphic_element", x => x.Id);
                    table.ForeignKey(
                        name: "FK_graphic_element_layer_LayerId",
                        column: x => x.LayerId,
                        principalTable: "layer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_graphic_element_vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "seat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    GraphicElementId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_seat_graphic_element_GraphicElementId",
                        column: x => x.GraphicElementId,
                        principalTable: "graphic_element",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_seat_layer_LayerId",
                        column: x => x.LayerId,
                        principalTable: "layer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_seat_vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attribute_selection_AttributeId",
                table: "attribute_selection",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_attribute_selection_RelationId",
                table: "attribute_selection",
                column: "RelationId");

            migrationBuilder.CreateIndex(
                name: "IX_graphic_element_LayerId",
                table: "graphic_element",
                column: "LayerId");

            migrationBuilder.CreateIndex(
                name: "IX_graphic_element_VehicleId",
                table: "graphic_element",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_layer_VehicleId",
                table: "layer",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_seat_GraphicElementId",
                table: "seat",
                column: "GraphicElementId");

            migrationBuilder.CreateIndex(
                name: "IX_seat_LayerId",
                table: "seat",
                column: "LayerId");

            migrationBuilder.CreateIndex(
                name: "IX_seat_VehicleId",
                table: "seat",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attribute_selection");

            migrationBuilder.DropTable(
                name: "seat");

            migrationBuilder.DropTable(
                name: "graphic_element");

            migrationBuilder.DropTable(
                name: "layer");

            migrationBuilder.DropTable(
                name: "vehicle");
        }
    }
}
