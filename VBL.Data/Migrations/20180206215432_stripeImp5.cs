using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class stripeImp5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StripeAuthTokens_Organizations_OrganizationId",
                table: "StripeAuthTokens");

            migrationBuilder.DropIndex(
                name: "IX_StripeAuthTokens_OrganizationId",
                table: "StripeAuthTokens");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "StripeAuthTokens");

            migrationBuilder.CreateTable(
                name: "StripeAccountDetails",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BusinessLogoFileId = table.Column<string>(nullable: true),
                    BusinessName = table.Column<string>(nullable: true),
                    BusinessPrimaryColor = table.Column<string>(nullable: true),
                    BusinessUrl = table.Column<string>(nullable: true),
                    ChargesEnabled = table.Column<bool>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    DebitNegativeBalances = table.Column<bool>(nullable: false),
                    DefaultCurrency = table.Column<string>(nullable: true),
                    DetailsSubmitted = table.Column<bool>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Object = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: true),
                    PayoutStatementDescriptor = table.Column<string>(nullable: true),
                    PayoutsEnabled = table.Column<bool>(nullable: false),
                    ProductDescription = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StatementDescriptor = table.Column<string>(nullable: true),
                    StripeAuthTokenId = table.Column<int>(nullable: false),
                    SupportEmail = table.Column<string>(nullable: true),
                    SupportPhone = table.Column<string>(nullable: true),
                    SupportUrl = table.Column<string>(nullable: true),
                    Timezone = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeAccountDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeAccountDetails_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StripeAccountDetails_StripeAuthTokens_StripeAuthTokenId",
                        column: x => x.StripeAuthTokenId,
                        principalTable: "StripeAuthTokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StripeAccountDetails_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StripeAccountDetails_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StripeConnectClicks_OrganizationId",
                table: "StripeConnectClicks",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeAccountDetails_OrganizationId",
                table: "StripeAccountDetails",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeAccountDetails_StripeAuthTokenId",
                table: "StripeAccountDetails",
                column: "StripeAuthTokenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StripeAccountDetails_UserIdCreated",
                table: "StripeAccountDetails",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_StripeAccountDetails_UserIdModified",
                table: "StripeAccountDetails",
                column: "UserIdModified");

            migrationBuilder.AddForeignKey(
                name: "FK_StripeConnectClicks_Organizations_OrganizationId",
                table: "StripeConnectClicks",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StripeConnectClicks_Organizations_OrganizationId",
                table: "StripeConnectClicks");

            migrationBuilder.DropTable(
                name: "StripeAccountDetails");

            migrationBuilder.DropIndex(
                name: "IX_StripeConnectClicks_OrganizationId",
                table: "StripeConnectClicks");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "StripeAuthTokens",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StripeAuthTokens_OrganizationId",
                table: "StripeAuthTokens",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_StripeAuthTokens_Organizations_OrganizationId",
                table: "StripeAuthTokens",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
