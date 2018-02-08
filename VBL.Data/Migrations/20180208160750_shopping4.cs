using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class shopping4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationTournamentDefaults_Users_UserCreatedId",
                table: "OrganizationTournamentDefaults");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationTournamentDefaults_Users_UserModifiedId",
                table: "OrganizationTournamentDefaults");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationTournamentDefaults_UserCreatedId",
                table: "OrganizationTournamentDefaults");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationTournamentDefaults_UserModifiedId",
                table: "OrganizationTournamentDefaults");

            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "OrganizationTournamentDefaults");

            migrationBuilder.DropColumn(
                name: "UserModifiedId",
                table: "OrganizationTournamentDefaults");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationTournamentDefaults_TournamentDirectorUserId",
                table: "OrganizationTournamentDefaults",
                column: "TournamentDirectorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationTournamentDefaults_UserIdCreated",
                table: "OrganizationTournamentDefaults",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationTournamentDefaults_UserIdModified",
                table: "OrganizationTournamentDefaults",
                column: "UserIdModified");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationTournamentDefaults_Users_TournamentDirectorUserId",
                table: "OrganizationTournamentDefaults",
                column: "TournamentDirectorUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationTournamentDefaults_Users_UserIdCreated",
                table: "OrganizationTournamentDefaults",
                column: "UserIdCreated",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationTournamentDefaults_Users_UserIdModified",
                table: "OrganizationTournamentDefaults",
                column: "UserIdModified",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationTournamentDefaults_Users_TournamentDirectorUserId",
                table: "OrganizationTournamentDefaults");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationTournamentDefaults_Users_UserIdCreated",
                table: "OrganizationTournamentDefaults");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationTournamentDefaults_Users_UserIdModified",
                table: "OrganizationTournamentDefaults");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationTournamentDefaults_TournamentDirectorUserId",
                table: "OrganizationTournamentDefaults");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationTournamentDefaults_UserIdCreated",
                table: "OrganizationTournamentDefaults");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationTournamentDefaults_UserIdModified",
                table: "OrganizationTournamentDefaults");

            migrationBuilder.AddColumn<int>(
                name: "UserCreatedId",
                table: "OrganizationTournamentDefaults",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserModifiedId",
                table: "OrganizationTournamentDefaults",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationTournamentDefaults_UserCreatedId",
                table: "OrganizationTournamentDefaults",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationTournamentDefaults_UserModifiedId",
                table: "OrganizationTournamentDefaults",
                column: "UserModifiedId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationTournamentDefaults_Users_UserCreatedId",
                table: "OrganizationTournamentDefaults",
                column: "UserCreatedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationTournamentDefaults_Users_UserModifiedId",
                table: "OrganizationTournamentDefaults",
                column: "UserModifiedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
