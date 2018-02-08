using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class stripeImp3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StripeAuthTokenId",
                table: "StripeConnectClicks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StripeConnectClicks_StripeAuthTokenId",
                table: "StripeConnectClicks",
                column: "StripeAuthTokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_StripeConnectClicks_StripeAuthTokens_StripeAuthTokenId",
                table: "StripeConnectClicks",
                column: "StripeAuthTokenId",
                principalTable: "StripeAuthTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StripeConnectClicks_StripeAuthTokens_StripeAuthTokenId",
                table: "StripeConnectClicks");

            migrationBuilder.DropIndex(
                name: "IX_StripeConnectClicks_StripeAuthTokenId",
                table: "StripeConnectClicks");

            migrationBuilder.DropColumn(
                name: "StripeAuthTokenId",
                table: "StripeConnectClicks");
        }
    }
}
