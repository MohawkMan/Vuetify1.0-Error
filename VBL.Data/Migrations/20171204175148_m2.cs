using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_SanctioningBodies_SanctioningBodyId",
                table: "Tournaments");

            migrationBuilder.AlterColumn<int>(
                name: "SanctioningBodyId",
                table: "Tournaments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_SanctioningBodies_SanctioningBodyId",
                table: "Tournaments",
                column: "SanctioningBodyId",
                principalTable: "SanctioningBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_SanctioningBodies_SanctioningBodyId",
                table: "Tournaments");

            migrationBuilder.AlterColumn<int>(
                name: "SanctioningBodyId",
                table: "Tournaments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_SanctioningBodies_SanctioningBodyId",
                table: "Tournaments",
                column: "SanctioningBodyId",
                principalTable: "SanctioningBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
