using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class Points7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "VblBasePointsEarned",
                table: "TournamentTeamMembers",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "OrganizationPointsEarned",
                table: "TournamentTeamMembers",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VblTotalPointsEarned",
                table: "TournamentTeamMembers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PointValueMultipliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TournamentTeamMemberId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointValueMultipliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointValueMultipliers_TournamentTeamMembers_TournamentTeamMemberId",
                        column: x => x.TournamentTeamMemberId,
                        principalTable: "TournamentTeamMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointValueMultipliers_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PointValueMultipliers_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PointValueMultipliers_TournamentTeamMemberId",
                table: "PointValueMultipliers",
                column: "TournamentTeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_PointValueMultipliers_UserIdCreated",
                table: "PointValueMultipliers",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_PointValueMultipliers_UserIdModified",
                table: "PointValueMultipliers",
                column: "UserIdModified");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointValueMultipliers");

            migrationBuilder.DropColumn(
                name: "VblTotalPointsEarned",
                table: "TournamentTeamMembers");

            migrationBuilder.AlterColumn<int>(
                name: "VblBasePointsEarned",
                table: "TournamentTeamMembers",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationPointsEarned",
                table: "TournamentTeamMembers",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
