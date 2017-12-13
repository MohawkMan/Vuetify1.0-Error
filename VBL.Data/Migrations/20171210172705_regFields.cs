using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class regFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TournamentRegistrationInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    Fields = table.Column<string>(nullable: true),
                    RequiredFields = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TournamentDivisionId = table.Column<int>(nullable: false),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentRegistrationInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentRegistrationInfo_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentRegistrationInfo_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrationInfo_UserIdCreated",
                table: "TournamentRegistrationInfo",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrationInfo_UserIdModified",
                table: "TournamentRegistrationInfo",
                column: "UserIdModified");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentRegistrationInfo");
        }
    }
}
