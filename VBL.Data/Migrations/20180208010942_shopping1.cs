using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VBL.Data.Migrations
{
    public partial class shopping1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StripePaymentTokens",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CardBrand = table.Column<string>(nullable: true),
                    CardId = table.Column<string>(nullable: true),
                    CardLast4 = table.Column<string>(nullable: true),
                    ClientIP = table.Column<string>(nullable: true),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripePaymentTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripePaymentTokens_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StripePaymentTokens_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingBags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    PaymentTokenId = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Total = table.Column<double>(nullable: false),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingBags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingBags_StripePaymentTokens_PaymentTokenId",
                        column: x => x.PaymentTokenId,
                        principalTable: "StripePaymentTokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingBags_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingBags_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingBagItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DtCreated = table.Column<DateTime>(nullable: true),
                    DtModified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    RawProductData = table.Column<string>(nullable: true),
                    RawRegistrationData = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ShoppingBagId = table.Column<int>(nullable: false),
                    UserIdCreated = table.Column<int>(nullable: true),
                    UserIdModified = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingBagItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingBagItems_ShoppingBags_ShoppingBagId",
                        column: x => x.ShoppingBagId,
                        principalTable: "ShoppingBags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingBagItems_Users_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingBagItems_Users_UserIdModified",
                        column: x => x.UserIdModified,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBagItems_ShoppingBagId",
                table: "ShoppingBagItems",
                column: "ShoppingBagId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBagItems_UserIdCreated",
                table: "ShoppingBagItems",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBagItems_UserIdModified",
                table: "ShoppingBagItems",
                column: "UserIdModified");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBags_PaymentTokenId",
                table: "ShoppingBags",
                column: "PaymentTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBags_UserIdCreated",
                table: "ShoppingBags",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBags_UserIdModified",
                table: "ShoppingBags",
                column: "UserIdModified");

            migrationBuilder.CreateIndex(
                name: "IX_StripePaymentTokens_UserIdCreated",
                table: "StripePaymentTokens",
                column: "UserIdCreated");

            migrationBuilder.CreateIndex(
                name: "IX_StripePaymentTokens_UserIdModified",
                table: "StripePaymentTokens",
                column: "UserIdModified");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingBagItems");

            migrationBuilder.DropTable(
                name: "ShoppingBags");

            migrationBuilder.DropTable(
                name: "StripePaymentTokens");
        }
    }
}
