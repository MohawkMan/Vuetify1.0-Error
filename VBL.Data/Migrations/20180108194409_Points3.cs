using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class Points3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VblId",
                table: "TournamentRegistrationPlayers",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NeedsMatchReview",
                table: "TournamentRegistrationPlayers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PlayerProfileId",
                table: "TournamentRegistrationPlayers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VblId",
                table: "PlayerProfiles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrationPlayers_PlayerProfileId",
                table: "TournamentRegistrationPlayers",
                column: "PlayerProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrationPlayers_PlayerProfiles_PlayerProfileId",
                table: "TournamentRegistrationPlayers",
                column: "PlayerProfileId",
                principalTable: "PlayerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrationPlayers_PlayerProfiles_PlayerProfileId",
                table: "TournamentRegistrationPlayers");

            migrationBuilder.DropIndex(
                name: "IX_TournamentRegistrationPlayers_PlayerProfileId",
                table: "TournamentRegistrationPlayers");

            migrationBuilder.DropColumn(
                name: "NeedsMatchReview",
                table: "TournamentRegistrationPlayers");

            migrationBuilder.DropColumn(
                name: "PlayerProfileId",
                table: "TournamentRegistrationPlayers");

            migrationBuilder.DropColumn(
                name: "VblId",
                table: "PlayerProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "VblId",
                table: "TournamentRegistrationPlayers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
