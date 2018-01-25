using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class regDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TournamentTeams",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "TournamentTeams",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TournamentRegistrations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "TournamentRegistrations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TournamentTeams");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "TournamentTeams");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TournamentRegistrations");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "TournamentRegistrations");
        }
    }
}
