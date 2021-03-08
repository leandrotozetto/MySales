using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MySales.Product.Api.Interface.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Sku",
                columns: table => new
                {
                    SkuId = table.Column<Guid>(nullable: false),
                    PartNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Stock = table.Column<int>(nullable: false),
                    SizeId = table.Column<Guid>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sku", x => x.SkuId);
                    table.ForeignKey(
                        name: "FK_Sku_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sku_ProductId",
                table: "Sku",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Sku_PartNumber_ProductId",
                table: "Sku",
                columns: new[] { "PartNumber", "ProductId" },
                unique: true,
                filter: "[PartNumber] IS NOT NULL AND [ProductId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sku");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
