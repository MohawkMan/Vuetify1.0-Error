using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class SanctioningBodyPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SanctioningBodyId",
                table: "TeamCountMultipliers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SanctioningBodyId",
                table: "PointValues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SanctioningBodyId",
                table: "TeamCountMultipliers");

            migrationBuilder.DropColumn(
                name: "SanctioningBodyId",
                table: "PointValues");
        }
    }
}
