using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class payment1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanPayAtEvent",
                table: "TournamentRegistrationWindows",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanProcessPayment",
                table: "TournamentRegistrationWindows",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanPayAtEvent",
                table: "TournamentRegistrationWindows");

            migrationBuilder.DropColumn(
                name: "CanProcessPayment",
                table: "TournamentRegistrationWindows");
        }
    }
}
