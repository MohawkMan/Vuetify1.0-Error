using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class emails1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrationEmails_EmailMessages_EmailMessageId",
                table: "TournamentRegistrationEmails");

            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRegistrationEmails_Emails_FromEmailAddress",
                table: "TournamentRegistrationEmails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEmails_Emails_EmailId",
                table: "UserEmails");

            migrationBuilder.DropTable(
                name: "EmailMessages");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEmails",
                table: "UserEmails");

            migrationBuilder.DropIndex(
                name: "IX_TournamentRegistrationEmails_EmailMessageId",
                table: "TournamentRegistrationEmails");

            migrationBuilder.DropIndex(
                name: "IX_TournamentRegistrationEmails_FromEmailAddress",
                table: "TournamentRegistrationEmails");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "UserEmails");

            migrationBuilder.DropColumn(
                name: "FromEmailAddress",
                table: "TournamentRegistrationEmails");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserEmails",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UserEmails",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "UserEmails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEmails",
                table: "UserEmails",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEmails",
                table: "UserEmails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserEmails");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "UserEmails");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "UserEmails");

            migrationBuilder.AddColumn<string>(
                name: "EmailId",
                table: "UserEmails",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FromEmailAddress",
                table: "TournamentRegistrationEmails",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEmails",
                table: "UserEmails",
                columns: new[] { "EmailId", "UserId" });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Address = table.Column<string>(nullable: false),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Address);
                    table.ForeignKey(
                        name: "FK_Emails_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emails_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BCC = table.Column<string>(nullable: true),
                    CC = table.Column<string>(nullable: true),
                    DTSent = table.Column<DateTime>(nullable: true),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    From = table.Column<string>(nullable: true),
                    FromEmailAddress = table.Column<string>(nullable: true),
                    HtmlMessage = table.Column<string>(nullable: true),
                    PlainTextMessage = table.Column<string>(nullable: true),
                    ReplyTo = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailMessages_Emails_FromEmailAddress",
                        column: x => x.FromEmailAddress,
                        principalTable: "Emails",
                        principalColumn: "Address",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailMessages_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailMessages_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrationEmails_EmailMessageId",
                table: "TournamentRegistrationEmails",
                column: "EmailMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentRegistrationEmails_FromEmailAddress",
                table: "TournamentRegistrationEmails",
                column: "FromEmailAddress");

            migrationBuilder.CreateIndex(
                name: "IX_EmailMessages_FromEmailAddress",
                table: "EmailMessages",
                column: "FromEmailAddress");

            migrationBuilder.CreateIndex(
                name: "IX_EmailMessages_UserIdCreated",
                table: "EmailMessages",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_EmailMessages_UserIdModified",
                table: "EmailMessages",
                column: "UserIdModified");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_UserIdCreated",
                table: "Emails",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_UserIdModified",
                table: "Emails",
                column: "UserIdModified");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrationEmails_EmailMessages_EmailMessageId",
                table: "TournamentRegistrationEmails",
                column: "EmailMessageId",
                principalTable: "EmailMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRegistrationEmails_Emails_FromEmailAddress",
                table: "TournamentRegistrationEmails",
                column: "FromEmailAddress",
                principalTable: "Emails",
                principalColumn: "Address",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEmails_Emails_EmailId",
                table: "UserEmails",
                column: "EmailId",
                principalTable: "Emails",
                principalColumn: "Address",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
