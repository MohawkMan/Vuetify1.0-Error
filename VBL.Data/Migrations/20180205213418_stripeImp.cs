using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class stripeImp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayPalPaymentResponses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Acct = table.Column<string>(nullable: true),
                    Amt = table.Column<decimal>(nullable: false),
                    AuthCode = table.Column<string>(nullable: true),
                    CardType = table.Column<byte>(nullable: false),
                    CorrelationId = table.Column<string>(nullable: true),
                    Cvv2Match = table.Column<string>(nullable: true),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    Pnref = table.Column<string>(nullable: true),
                    Ppref = table.Column<string>(nullable: true),
                    RespMsg = table.Column<string>(nullable: true),
                    Result = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SecureToken = table.Column<string>(nullable: true),
                    SecureTokenId = table.Column<string>(nullable: true),
                    TransTime = table.Column<string>(nullable: true),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayPalPaymentResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayPalPaymentResponses_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayPalPaymentResponses_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayPalTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    RawData = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Total = table.Column<float>(nullable: false),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayPalTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayPalTransactions_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayPalTransactions_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "StripeConnectClicks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationUserId = table.Column<int>(nullable: false),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeConnectClicks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeConnectClicks_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StripeConnectClicks_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayPalTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    PayPalTransactionId = table.Column<int>(nullable: false),
                    RespMsg = table.Column<string>(nullable: true),
                    Result = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SecureToken = table.Column<string>(nullable: true),
                    SecureTokenId = table.Column<string>(nullable: true),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayPalTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayPalTokens_PayPalTransactions_PayPalTransactionId",
                        column: x => x.PayPalTransactionId,
                        principalTable: "PayPalTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayPalTokens_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayPalTokens_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayPalPaymentResponses_UserIdCreated",
                table: "PayPalPaymentResponses",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_PayPalPaymentResponses_UserIdModified",
                table: "PayPalPaymentResponses",
                column: "UserIdModified");

            migrationBuilder.CreateIndex(
                name: "IX_PayPalTokens_PayPalTransactionId",
                table: "PayPalTokens",
                column: "PayPalTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_PayPalTokens_UserIdCreated",
                table: "PayPalTokens",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_PayPalTokens_UserIdModified",
                table: "PayPalTokens",
                column: "UserIdModified");

            migrationBuilder.CreateIndex(
                name: "IX_PayPalTransactions_UserIdCreated",
                table: "PayPalTransactions",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_PayPalTransactions_UserIdModified",
                table: "PayPalTransactions",
                column: "UserIdModified");

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

            migrationBuilder.CreateIndex(
                name: "IX_StripeConnectClicks_UserIdCreated",
                table: "StripeConnectClicks",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_StripeConnectClicks_UserIdModified",
                table: "StripeConnectClicks",
                column: "UserIdModified");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayPalPaymentResponses");

            migrationBuilder.DropTable(
                name: "PayPalTokens");

            migrationBuilder.DropTable(
                name: "StripeAccounts");

            migrationBuilder.DropTable(
                name: "StripeConnectClicks");

            migrationBuilder.DropTable(
                name: "PayPalTransactions");
        }
    }
}
