using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class emailTemplates3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TournamentDirectorUserId",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_TournamentDirectorUserId",
                table: "Tournaments",
                column: "TournamentDirectorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Users_TournamentDirectorUserId",
                table: "Tournaments",
                column: "TournamentDirectorUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Users_TournamentDirectorUserId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_TournamentDirectorUserId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "TournamentDirectorUserId",
                table: "Tournaments");
        }
    }
}
