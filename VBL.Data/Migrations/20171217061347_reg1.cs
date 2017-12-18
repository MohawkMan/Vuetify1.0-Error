using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class reg1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TournamentRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddOnQty = table.Column<int>(nullable: false),
                    Confirmed = table.Column<bool>(nullable: false),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    PaymentType = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TournamentDivisionId = table.Column<int>(nullable: false),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentRegistrations_TournamentDivisions_TournamentDivisionId",
                        column: x => x.TournamentDivisionId,
                        principalTable: "TournamentDivisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentRegistrations_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentRegistrations_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentRegistrationPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CbvaNumber = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Dob = table.Column<DateTime>(nullable: false),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<string>(nullable: true),
                    TournamentRegistrationId = table.Column<int>(nullable: true),
                    UsavNumber = table.Column<string>(nullable: true),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true),
                    VblId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentRegistrationPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentRegistrationPlayers_TournamentRegistrations_TournamentRegistrationId",
                        column: x => x.TournamentRegistrationId,
                        principalTable: "TournamentRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentRegistrationPlayers_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentRegistrationPlayers_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentTeams",
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
                    TournamentRegistrationId = table.Column<int>(nullable: false),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentTeams_TournamentDivisions_TournamentDivisionId",
                        column: x => x.TournamentDivisionId,
                        principalTable: "TournamentDivisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentTeams_TournamentRegistrations_TournamentRegistrationId",
                        column: x => x.TournamentRegistrationId,
                        principalTable: "TournamentRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_TournamentTeams_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentTeams_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentTeamMembers",
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
                    table.PrimaryKey("PK_TournamentTeamMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentTeamMembers_TournamentTeams_TournamentTeamId",
                        column: x => x.TournamentTeamId,
                        principalTable: "TournamentTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentTeamMembers_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentTeamMembers_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrationPlayers_TournamentRegistrationId",
                table: "TournamentRegistrationPlayers",
                column: "TournamentRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrationPlayers_UserIdCreated",
                table: "TournamentRegistrationPlayers",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrationPlayers_UserIdModified",
                table: "TournamentRegistrationPlayers",
                column: "UserIdModified");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrations_TournamentDivisionId",
                table: "TournamentRegistrations",
                column: "TournamentDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrations_UserIdCreated",
                table: "TournamentRegistrations",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrations_UserIdModified",
                table: "TournamentRegistrations",
                column: "UserIdModified");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeamMembers_TournamentTeamId",
                table: "TournamentTeamMembers",
                column: "TournamentTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeamMembers_UserIdCreated",
                table: "TournamentTeamMembers",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeamMembers_UserIdModified",
                table: "TournamentTeamMembers",
                column: "UserIdModified");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeams_TournamentDivisionId",
                table: "TournamentTeams",
                column: "TournamentDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeams_TournamentRegistrationId",
                table: "TournamentTeams",
                column: "TournamentRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeams_UserIdCreated",
                table: "TournamentTeams",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentTeams_UserIdModified",
                table: "TournamentTeams",
                column: "UserIdModified");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentRegistrationPlayers");

            migrationBuilder.DropTable(
                name: "TournamentTeamMembers");

            migrationBuilder.DropTable(
                name: "TournamentTeams");

            migrationBuilder.DropTable(
                name: "TournamentRegistrations");
        }
    }
}
