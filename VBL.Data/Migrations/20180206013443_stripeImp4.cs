using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class stripeImp4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StripeAuthTokens_Organizations_OrganizationId",
                table: "StripeAuthTokens");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "StripeAuthTokens",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_StripeAuthTokens_Organizations_OrganizationId",
                table: "StripeAuthTokens",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StripeAuthTokens_Organizations_OrganizationId",
                table: "StripeAuthTokens");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "StripeAuthTokens",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StripeAuthTokens_Organizations_OrganizationId",
                table: "StripeAuthTokens",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
