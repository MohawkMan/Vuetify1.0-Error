using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class emailTemplates1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Emails");

            migrationBuilder.RenameColumn(
                name: "RefundCutoff",
                table: "TournamentDivisions",
                newName: "DtRefundCutoff");

            migrationBuilder.AddColumn<string>(
                name: "EmailNote",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SparkPostEmailTemplateId",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailNote",
                table: "TournamentDivisions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SparkPostEmailTemplateId",
                table: "TournamentDivisions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultEmailNote",
                table: "Organizations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SparkPostEmailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    For = table.Column<string>(nullable: true),
                    IsCurrent = table.Column<bool>(nullable: false),
                    IsDraft = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TemplateId = table.Column<string>(nullable: true),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparkPostEmailTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SparkPostEmailTemplates_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SparkPostEmailTemplates_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_SparkPostEmailTemplateId",
                table: "Tournaments",
                column: "SparkPostEmailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentDivisions_SparkPostEmailTemplateId",
                table: "TournamentDivisions",
                column: "SparkPostEmailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SparkPostEmailTemplates_UserIdCreated",
                table: "SparkPostEmailTemplates",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_SparkPostEmailTemplates_UserIdModified",
                table: "SparkPostEmailTemplates",
                column: "UserIdModified");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentDivisions_SparkPostEmailTemplates_SparkPostEmailTemplateId",
                table: "TournamentDivisions",
                column: "SparkPostEmailTemplateId",
                principalTable: "SparkPostEmailTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_SparkPostEmailTemplates_SparkPostEmailTemplateId",
                table: "Tournaments",
                column: "SparkPostEmailTemplateId",
                principalTable: "SparkPostEmailTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentDivisions_SparkPostEmailTemplates_SparkPostEmailTemplateId",
                table: "TournamentDivisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_SparkPostEmailTemplates_SparkPostEmailTemplateId",
                table: "Tournaments");

            migrationBuilder.DropTable(
                name: "SparkPostEmailTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_SparkPostEmailTemplateId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_TournamentDivisions_SparkPostEmailTemplateId",
                table: "TournamentDivisions");

            migrationBuilder.DropColumn(
                name: "EmailNote",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "SparkPostEmailTemplateId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "EmailNote",
                table: "TournamentDivisions");

            migrationBuilder.DropColumn(
                name: "SparkPostEmailTemplateId",
                table: "TournamentDivisions");

            migrationBuilder.DropColumn(
                name: "DefaultEmailNote",
                table: "Organizations");

            migrationBuilder.RenameColumn(
                name: "DtRefundCutoff",
                table: "TournamentDivisions",
                newName: "RefundCutoff");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Phones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Emails",
                nullable: false,
                defaultValue: false);
        }
    }
}
