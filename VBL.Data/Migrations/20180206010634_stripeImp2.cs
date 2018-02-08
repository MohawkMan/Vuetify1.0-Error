using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class stripeImp2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StripeAccounts");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "StripeConnectClicks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scope",
                table: "StripeConnectClicks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StripeAuthTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessToken = table.Column<string>(nullable: true),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    Error = table.Column<string>(nullable: true),
                    ErrorDescription = table.Column<string>(nullable: true),
                    LiveMode = table.Column<bool>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Scope = table.Column<string>(nullable: true),
                    StripePublishableKey = table.Column<string>(nullable: true),
                    StripeUserId = table.Column<string>(nullable: true),
                    TokenType = table.Column<string>(nullable: true),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeAuthTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeAuthTokens_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StripeAuthTokens_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StripeAuthTokens_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StripeAuthTokens_OrganizationId",
                table: "StripeAuthTokens",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeAuthTokens_UserIdCreated",
                table: "StripeAuthTokens",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_StripeAuthTokens_UserIdModified",
                table: "StripeAuthTokens",
                column: "UserIdModified");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StripeAuthTokens");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "StripeConnectClicks");

            migrationBuilder.DropColumn(
                name: "Scope",
                table: "StripeConnectClicks");

            migrationBuilder.CreateTable(
                name: "StripeAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeAccounts_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StripeAccounts_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StripeAccounts_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StripeAccounts_OrganizationId",
                table: "StripeAccounts",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeAccounts_UserIdCreated",
                table: "StripeAccounts",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_StripeAccounts_UserIdModified",
                table: "StripeAccounts",
                column: "UserIdModified");
        }
    }
}
