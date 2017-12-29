using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class emailTemplates2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TournamentTeams_TournamentRegistrationId",
                table: "TournamentTeams");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeams_TournamentRegistrationId",
                table: "TournamentTeams",
                column: "TournamentRegistrationId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TournamentTeams_TournamentRegistrationId",
                table: "TournamentTeams");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeams_TournamentRegistrationId",
                table: "TournamentTeams",
                column: "TournamentRegistrationId");
        }
    }
}
