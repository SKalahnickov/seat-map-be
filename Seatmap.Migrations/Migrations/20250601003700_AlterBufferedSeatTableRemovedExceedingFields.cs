using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seatmap.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AlterBufferedSeatTableRemovedExceedingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GraphicElementId",
                table: "buffered_seat");

            migrationBuilder.DropColumn(
                name: "LayerId",
                table: "buffered_seat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GraphicElementId",
                table: "buffered_seat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LayerId",
                table: "buffered_seat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
