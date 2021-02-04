using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoshenJimenez.Mercadia3.Web.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TagLine = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TagLine = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: true),
                    TagId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTags_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "TagLine", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("02b92adc-c0dc-4a87-9aa8-df5d90cf2a00"), new DateTime(2021, 2, 4, 2, 25, 27, 499, DateTimeKind.Utc).AddTicks(2886), null, "Category 1", null, new DateTime(2021, 2, 4, 2, 25, 27, 499, DateTimeKind.Utc).AddTicks(2931) },
                    { new Guid("02b92adc-c0dc-4a87-9aa8-df5d90cf2a01"), new DateTime(2021, 2, 4, 2, 25, 27, 499, DateTimeKind.Utc).AddTicks(6111), null, "Category 2", null, new DateTime(2021, 2, 4, 2, 25, 27, 499, DateTimeKind.Utc).AddTicks(6135) },
                    { new Guid("02b92adc-c0dc-4a87-9aa8-df5d90cf2a02"), new DateTime(2021, 2, 4, 2, 25, 27, 499, DateTimeKind.Utc).AddTicks(6216), null, "Category 3", null, new DateTime(2021, 2, 4, 2, 25, 27, 499, DateTimeKind.Utc).AddTicks(6219) }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("32febb22-f596-4b1e-b0a8-b11ad54be200"), new DateTime(2021, 2, 4, 2, 25, 27, 492, DateTimeKind.Utc).AddTicks(9811), "Tag 1", new DateTime(2021, 2, 4, 2, 25, 27, 493, DateTimeKind.Utc).AddTicks(1285) },
                    { new Guid("32febb22-f596-4b1e-b0a8-b11ad54be201"), new DateTime(2021, 2, 4, 2, 25, 27, 493, DateTimeKind.Utc).AddTicks(6325), "Tag 2", new DateTime(2021, 2, 4, 2, 25, 27, 493, DateTimeKind.Utc).AddTicks(6354) },
                    { new Guid("32febb22-f596-4b1e-b0a8-b11ad54be203"), new DateTime(2021, 2, 4, 2, 25, 27, 493, DateTimeKind.Utc).AddTicks(6429), "Tag 3", new DateTime(2021, 2, 4, 2, 25, 27, 493, DateTimeKind.Utc).AddTicks(6431) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Name", "Price", "TagLine", "UpdatedAt" },
                values: new object[] { new Guid("254a99bb-bee5-43e0-80dc-e76622110500"), new Guid("02b92adc-c0dc-4a87-9aa8-df5d90cf2a00"), new DateTime(2021, 2, 4, 2, 25, 27, 500, DateTimeKind.Utc).AddTicks(1260), "Palmolive Naturals White with Milk Whitening Bar Soap 80g 2+1 Value Pack", "Product 1 Soap", 0m, "Pag di ka pumuti, iitim ka.", new DateTime(2021, 2, 4, 2, 25, 27, 500, DateTimeKind.Utc).AddTicks(1275) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Name", "Price", "TagLine", "UpdatedAt" },
                values: new object[] { new Guid("254a99bb-bee5-43e0-80dc-e76622110501"), new Guid("02b92adc-c0dc-4a87-9aa8-df5d90cf2a00"), new DateTime(2021, 2, 4, 2, 25, 27, 501, DateTimeKind.Utc).AddTicks(254), "Global Version Xiaomi Redmi 9A Smartphones 2GB RAM 32GB ROM 6.53″ Intelligent Face Unlock Xiaomi Mall", "Product 2 Cellphone", 0m, "Pang ML", new DateTime(2021, 2, 4, 2, 25, 27, 501, DateTimeKind.Utc).AddTicks(281) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Name", "Price", "TagLine", "UpdatedAt" },
                values: new object[] { new Guid("254a99bb-bee5-43e0-80dc-e76622110502"), new Guid("02b92adc-c0dc-4a87-9aa8-df5d90cf2a00"), new DateTime(2021, 2, 4, 2, 25, 27, 501, DateTimeKind.Utc).AddTicks(441), "Marby Mini Monay 250g", "Product 3 Bread", 0m, "Busog ka agad", new DateTime(2021, 2, 4, 2, 25, 27, 501, DateTimeKind.Utc).AddTicks(444) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_ProductId",
                table: "ProductTags",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_TagId",
                table: "ProductTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
