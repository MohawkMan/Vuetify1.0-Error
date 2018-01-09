using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class Points9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "PlayerProfiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "PlayerProfiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PlayerProfiles");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "PlayerProfiles");
        }
    }
}
