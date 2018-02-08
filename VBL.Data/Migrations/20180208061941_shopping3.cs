using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class shopping3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TournamentRegistrationId",
                table: "ShoppingBagItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBagItems_TournamentRegistrationId",
                table: "ShoppingBagItems",
                column: "TournamentRegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBagItems_TournamentRegistrations_TournamentRegistrationId",
                table: "ShoppingBagItems",
                column: "TournamentRegistrationId",
                principalTable: "TournamentRegistrations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBagItems_TournamentRegistrations_TournamentRegistrationId",
                table: "ShoppingBagItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingBagItems_TournamentRegistrationId",
                table: "ShoppingBagItems");

            migrationBuilder.DropColumn(
                name: "TournamentRegistrationId",
                table: "ShoppingBagItems");
        }
    }
}
