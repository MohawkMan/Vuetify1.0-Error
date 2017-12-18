using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class email1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromEmailId",
                table: "EmailMessages");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "EmailMessages",
                newName: "To");

            migrationBuilder.AddColumn<DateTime>(
                name: "DTSent",
                table: "EmailMessages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "EmailMessages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HtmlMessage",
                table: "EmailMessages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlainTextMessage",
                table: "EmailMessages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReplyTo",
                table: "EmailMessages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DTSent",
                table: "EmailMessages");

            migrationBuilder.DropColumn(
                name: "From",
                table: "EmailMessages");

            migrationBuilder.DropColumn(
                name: "HtmlMessage",
                table: "EmailMessages");

            migrationBuilder.DropColumn(
                name: "PlainTextMessage",
                table: "EmailMessages");

            migrationBuilder.DropColumn(
                name: "ReplyTo",
                table: "EmailMessages");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "EmailMessages",
                newName: "Body");

            migrationBuilder.AddColumn<int>(
                name: "FromEmailId",
                table: "EmailMessages",
                nullable: true);
        }
    }
}
