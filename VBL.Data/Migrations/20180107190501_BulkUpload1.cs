using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class BulkUpload1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TournamentId",
                table: "TournamentRegistrations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AauNumber",
                table: "TournamentRegistrationPlayers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvpNumber",
                table: "TournamentRegistrationPlayers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "TournamentRegistrations");

            migrationBuilder.DropColumn(
                name: "AauNumber",
                table: "TournamentRegistrationPlayers");

            migrationBuilder.DropColumn(
                name: "AvpNumber",
                table: "TournamentRegistrationPlayers");
        }
    }
}
