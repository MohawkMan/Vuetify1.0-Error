using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class revert1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentTeamMember");

            migrationBuilder.DropTable(
                name: "TournamentTeam");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TournamentTeam",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    Finish = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Points = table.Column<double>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Seed = table.Column<int>(nullable: true),
                    TournamentDivisionId = table.Column<int>(nullable: false),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentTeam_TournamentDivisions_TournamentDivisionId",
                        column: x => x.TournamentDivisionId,
                        principalTable: "TournamentDivisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentTeam_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentTeam_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentTeamMember",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    PointLockDt = table.Column<DateTime>(nullable: true),
                    Points = table.Column<double>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TournamentTeamId = table.Column<int>(nullable: false),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentTeamMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentTeamMember_TournamentTeam_TournamentTeamId",
                        column: x => x.TournamentTeamId,
                        principalTable: "TournamentTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentTeamMember_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentTeamMember_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeam_TournamentDivisionId",
                table: "TournamentTeam",
                column: "TournamentDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeam_UserIdCreated",
                table: "TournamentTeam",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeam_UserIdModified",
                table: "TournamentTeam",
                column: "UserIdModified");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeamMember_TournamentTeamId",
                table: "TournamentTeamMember",
                column: "TournamentTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeamMember_UserIdCreated",
                table: "TournamentTeamMember",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeamMember_UserIdModified",
                table: "TournamentTeamMember",
                column: "UserIdModified");
        }
    }
}
