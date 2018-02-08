using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class shopping2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "ShoppingBags",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StripeChargeRecords",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    RawResponse = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ShoppingBagId = table.Column<int>(nullable: false),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeChargeRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeChargeRecords_ShoppingBags_ShoppingBagId",
                        column: x => x.ShoppingBagId,
                        principalTable: "ShoppingBags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StripeChargeRecords_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StripeChargeRecords_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StripeChargeRecords_ShoppingBagId",
                table: "StripeChargeRecords",
                column: "ShoppingBagId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeChargeRecords_UserIdCreated",
                table: "StripeChargeRecords",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_StripeChargeRecords_UserIdModified",
                table: "StripeChargeRecords",
                column: "UserIdModified");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StripeChargeRecords");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "ShoppingBags");
        }
    }
}
