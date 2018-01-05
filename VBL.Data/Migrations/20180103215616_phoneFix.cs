using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class phoneFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPhones_Phones_PhoneId",
                table: "UserPhones");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPhones",
                table: "UserPhones");

            migrationBuilder.DropIndex(
                name: "IX_UserPhones_PhoneId",
                table: "UserPhones");

            migrationBuilder.DropColumn(
                name: "PhoneId",
                table: "UserPhones");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserPhones",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<bool>(
                name: "IsSMS",
                table: "UserPhones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "UserPhones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "UserPhones",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPhones",
                table: "UserPhones",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPhones",
                table: "UserPhones");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserPhones");

            migrationBuilder.DropColumn(
                name: "IsSMS",
                table: "UserPhones");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "UserPhones");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "UserPhones");

            migrationBuilder.AddColumn<string>(
                name: "PhoneId",
                table: "UserPhones",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPhones",
                table: "UserPhones",
                columns: new[] { "PhoneId", "UserId" });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Number = table.Column<string>(nullable: false),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    IsSMS = table.Column<bool>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Number);
                    table.ForeignKey(
                        name: "FK_Phones_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Phones_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPhones_PhoneId",
                table: "UserPhones",
                column: "PhoneId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phones_UserIdCreated",
                table: "Phones",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_UserIdModified",
                table: "Phones",
                column: "UserIdModified");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhones_Phones_PhoneId",
                table: "UserPhones",
                column: "PhoneId",
                principalTable: "Phones",
                principalColumn: "Number",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
