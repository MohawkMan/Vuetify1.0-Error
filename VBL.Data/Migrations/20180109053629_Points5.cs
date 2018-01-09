using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class Points5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "TournamentTeams");

            migrationBuilder.RenameColumn(
                name: "Points",
                table: "TournamentTeamMembers",
                newName: "VblSeedingPoints");

            migrationBuilder.RenameColumn(
                name: "PointLockDt",
                table: "TournamentTeamMembers",
                newName: "DtPointLock");

            migrationBuilder.AddColumn<double>(
                name: "AauSeedingPoints",
                table: "TournamentTeamMembers",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AvpSeedingPoints",
                table: "TournamentTeamMembers",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CbvaSeedingPoints",
                table: "TournamentTeamMembers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Finish",
                table: "TournamentTeamMembers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationPointsEarned",
                table: "TournamentTeamMembers",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "UsavSeedingPoints",
                table: "TournamentTeamMembers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VblBasePointsEarned",
                table: "TournamentTeamMembers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TeamCountMultipliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    Multiplier = table.Column<double>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TeamCap = table.Column<int>(nullable: false),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamCountMultipliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamCountMultipliers_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamCountMultipliers_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamCountMultipliers_UserIdCreated",
                table: "TeamCountMultipliers",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCountMultipliers_UserIdModified",
                table: "TeamCountMultipliers",
                column: "UserIdModified");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamCountMultipliers");

            migrationBuilder.DropColumn(
                name: "AauSeedingPoints",
                table: "TournamentTeamMembers");

            migrationBuilder.DropColumn(
                name: "AvpSeedingPoints",
                table: "TournamentTeamMembers");

            migrationBuilder.DropColumn(
                name: "CbvaSeedingPoints",
                table: "TournamentTeamMembers");

            migrationBuilder.DropColumn(
                name: "Finish",
                table: "TournamentTeamMembers");

            migrationBuilder.DropColumn(
                name: "OrganizationPointsEarned",
                table: "TournamentTeamMembers");

            migrationBuilder.DropColumn(
                name: "UsavSeedingPoints",
                table: "TournamentTeamMembers");

            migrationBuilder.DropColumn(
                name: "VblBasePointsEarned",
                table: "TournamentTeamMembers");

            migrationBuilder.RenameColumn(
                name: "VblSeedingPoints",
                table: "TournamentTeamMembers",
                newName: "Points");

            migrationBuilder.RenameColumn(
                name: "DtPointLock",
                table: "TournamentTeamMembers",
                newName: "PointLockDt");

            migrationBuilder.AddColumn<double>(
                name: "Points",
                table: "TournamentTeams",
                nullable: true);
        }
    }
}
