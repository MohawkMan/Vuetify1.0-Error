using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class refund1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefundCutoff",
                table: "TournamentDivisions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BCC",
                table: "EmailMessages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CC",
                table: "EmailMessages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefundCutoff",
                table: "TournamentDivisions");

            migrationBuilder.DropColumn(
                name: "BCC",
                table: "EmailMessages");

            migrationBuilder.DropColumn(
                name: "CC",
                table: "EmailMessages");
        }
    }
}
