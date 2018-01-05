using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class tourneyredo1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DefaultEmailNote",
                table: "Organizations",
                newName: "WebsiteUrl");

            migrationBuilder.AddColumn<string>(
                name: "ExternalRegistrationUrl",
                table: "TournamentRegistrationWindows",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Snapchat",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleUrl",
                table: "Locations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrganizationTournamentDefaults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    EmailNote = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    OneDay = table.Column<bool>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TournamentDirectorUserId = table.Column<int>(nullable: false),
                    UserCreatedId = table.Column<int>(nullable: true),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true),
                    UserModifiedId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationTournamentDefaults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationTournamentDefaults_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationTournamentDefaults_Users_UserCreatedId",
                        column: x => x.UserCreatedId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationTournamentDefaults_Users_UserModifiedId",
                        column: x => x.UserModifiedId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationTournamentDefaults_OrganizationId",
                table: "OrganizationTournamentDefaults",
                column: "OrganizationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationTournamentDefaults_UserCreatedId",
                table: "OrganizationTournamentDefaults",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationTournamentDefaults_UserModifiedId",
                table: "OrganizationTournamentDefaults",
                column: "UserModifiedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationTournamentDefaults");

            migrationBuilder.DropColumn(
                name: "ExternalRegistrationUrl",
                table: "TournamentRegistrationWindows");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Snapchat",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "GoogleUrl",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "WebsiteUrl",
                table: "Organizations",
                newName: "DefaultEmailNote");
        }
    }
}
