using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class Points4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerProfileId",
                table: "TournamentTeamMembers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeamMembers_PlayerProfileId",
                table: "TournamentTeamMembers",
                column: "PlayerProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentTeamMembers_PlayerProfiles_PlayerProfileId",
                table: "TournamentTeamMembers",
                column: "PlayerProfileId",
                principalTable: "PlayerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentTeamMembers_PlayerProfiles_PlayerProfileId",
                table: "TournamentTeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_TournamentTeamMembers_PlayerProfileId",
                table: "TournamentTeamMembers");

            migrationBuilder.DropColumn(
                name: "PlayerProfileId",
                table: "TournamentTeamMembers");
        }
    }
}
