using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seatmap.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AlterSeatTableAddedExternalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                table: "seat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.Sql(@"
                UPDATE seat
                SET ""ExternalId"" = ""Id""
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "seat");
        }
    }
}
