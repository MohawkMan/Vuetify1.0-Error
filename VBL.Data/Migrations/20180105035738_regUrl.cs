using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class regUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalRegistrationUrl",
                table: "TournamentRegistrationWindows");

            migrationBuilder.AddColumn<string>(
                name: "ExternalRegistrationUrl",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrganizationPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    IsCover = table.Column<bool>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Url = table.Column<string>(nullable: true),
                    UserCreatedId = table.Column<int>(nullable: true),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true),
                    UserModifiedId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationPhoto_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationPhoto_Users_UserCreatedId",
                        column: x => x.UserCreatedId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationPhoto_Users_UserModifiedId",
                        column: x => x.UserModifiedId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPhoto_OrganizationId",
                table: "OrganizationPhoto",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPhoto_UserCreatedId",
                table: "OrganizationPhoto",
                column: "UserCreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPhoto_UserModifiedId",
                table: "OrganizationPhoto",
                column: "UserModifiedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationPhoto");

            migrationBuilder.DropColumn(
                name: "ExternalRegistrationUrl",
                table: "Tournaments");

            migrationBuilder.AddColumn<string>(
                name: "ExternalRegistrationUrl",
                table: "TournamentRegistrationWindows",
                nullable: true);
        }
    }
}
