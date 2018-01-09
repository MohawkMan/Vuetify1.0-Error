using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class Points8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DtEarned",
                table: "TournamentTeamMembers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DtFinalized",
                table: "TournamentTeamMembers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DtEarned",
                table: "TournamentTeamMembers");

            migrationBuilder.DropColumn(
                name: "DtFinalized",
                table: "TournamentTeamMembers");
        }
    }
}
