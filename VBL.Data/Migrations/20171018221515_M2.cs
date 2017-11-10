using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class M2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DtCreated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DtModified",
                table: "AspNetUsers",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdCreated",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdModified",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Address = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DtCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtModified = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Public = table.Column<bool>(type: "bit", nullable: false),
                    UserIdCreated = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserIdModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Verified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Address);
                    table.ForeignKey(
                        name: "FK_Emails_AspNetUsers_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emails_AspNetUsers_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DtCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtModified = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Public = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserIdCreated = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserIdModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organizations_AspNetUsers_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organizations_AspNetUsers_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    Number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DtCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtModified = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Public = table.Column<bool>(type: "bit", nullable: false),
                    SMS = table.Column<bool>(type: "bit", nullable: false),
                    UserIdCreated = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserIdModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Verified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.Number);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_AspNetUsers_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_AspNetUsers_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserEmails",
                columns: table => new
                {
                    EmailId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DtCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtModified = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UserIdCreated = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserIdModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEmails", x => new { x.EmailId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserEmails_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Address",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEmails_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEmails_AspNetUsers_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserEmails_AspNetUsers_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationUsers",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DtCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtModified = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    UserIdCreated = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserIdModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationUsers", x => new { x.OrganizationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_OrganizationUsers_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationUsers_AspNetUsers_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationUsers_AspNetUsers_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPhoneNumbers",
                columns: table => new
                {
                    PhoneNumberId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DtCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtModified = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UserIdCreated = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserIdModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPhoneNumbers", x => new { x.PhoneNumberId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserPhoneNumbers_PhoneNumbers_PhoneNumberId",
                        column: x => x.PhoneNumberId,
                        principalTable: "PhoneNumbers",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPhoneNumbers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPhoneNumbers_AspNetUsers_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPhoneNumbers_AspNetUsers_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserIdCreated",
                table: "AspNetUsers",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserIdModified",
                table: "AspNetUsers",
                column: "UserIdModified");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_UserIdCreated",
                table: "Emails",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_UserIdModified",
                table: "Emails",
                column: "UserIdModified");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_UserId",
                table: "Organizations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_UserIdCreated",
                table: "Organizations",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_UserIdModified",
                table: "Organizations",
                column: "UserIdModified");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUsers_UserId",
                table: "OrganizationUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUsers_UserIdCreated",
                table: "OrganizationUsers",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUsers_UserIdModified",
                table: "OrganizationUsers",
                column: "UserIdModified");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_UserIdCreated",
                table: "PhoneNumbers",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_UserIdModified",
                table: "PhoneNumbers",
                column: "UserIdModified");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmails_UserId",
                table: "UserEmails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmails_UserIdCreated",
                table: "UserEmails",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmails_UserIdModified",
                table: "UserEmails",
                column: "UserIdModified");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhoneNumbers_UserId",
                table: "UserPhoneNumbers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhoneNumbers_UserIdCreated",
                table: "UserPhoneNumbers",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhoneNumbers_UserIdModified",
                table: "UserPhoneNumbers",
                column: "UserIdModified");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserIdCreated",
                table: "AspNetUsers",
                column: "UserIdCreated",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserIdModified",
                table: "AspNetUsers",
                column: "UserIdModified",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserIdCreated",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserIdModified",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "OrganizationUsers");

            migrationBuilder.DropTable(
                name: "UserEmails");

            migrationBuilder.DropTable(
                name: "UserPhoneNumbers");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserIdCreated",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserIdModified",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DtCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DtModified",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserIdCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserIdModified",
                table: "AspNetUsers");
        }
    }
}
