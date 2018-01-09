using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class LocationTZ1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Organizations",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "TimeZoneName",
                table: "Locations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeZoneName",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Organizations",
                newName: "UserName");
        }
    }
}
