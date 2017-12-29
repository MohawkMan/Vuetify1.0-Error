using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class td1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TournamentDirectorUserId",
                table: "TournamentDivisions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TournamentDivisions_TournamentDirectorUserId",
                table: "TournamentDivisions",
                column: "TournamentDirectorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentDivisions_Users_TournamentDirectorUserId",
                table: "TournamentDivisions",
                column: "TournamentDirectorUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentDivisions_Users_TournamentDirectorUserId",
                table: "TournamentDivisions");

            migrationBuilder.DropIndex(
                name: "IX_TournamentDivisions_TournamentDirectorUserId",
                table: "TournamentDivisions");

            migrationBuilder.DropColumn(
                name: "TournamentDirectorUserId",
                table: "TournamentDivisions");
        }
    }
}
