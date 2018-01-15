using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class regUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrationInfo_TournamentDivisionId",
                table: "TournamentRegistrationInfo",
                column: "TournamentDivisionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrationInfo_TournamentDivisions_TournamentDivisionId",
                table: "TournamentRegistrationInfo",
                column: "TournamentDivisionId",
                principalTable: "TournamentDivisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrationInfo_TournamentDivisions_TournamentDivisionId",
                table: "TournamentRegistrationInfo");

            migrationBuilder.DropIndex(
                name: "IX_TournamentRegistrationInfo_TournamentDivisionId",
                table: "TournamentRegistrationInfo");
        }
    }
}
