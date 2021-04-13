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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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
                name: "UserLogins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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
                    { new Guid("02b92adc-c0dc-4a87-9aa8-df5d90cf2a00"), new DateTime(2021, 4, 13, 2, 27, 13, 973, DateTimeKind.Utc).AddTicks(3042), null, "Category 1", null, new DateTime(2021, 4, 13, 2, 27, 13, 973, DateTimeKind.Utc).AddTicks(3055) },
                    { new Guid("02b92adc-c0dc-4a87-9aa8-df5d90cf2a01"), new DateTime(2021, 4, 13, 2, 27, 13, 973, DateTimeKind.Utc).AddTicks(3878), null, "Category 2", null, new DateTime(2021, 4, 13, 2, 27, 13, 973, DateTimeKind.Utc).AddTicks(3882) },
                    { new Guid("02b92adc-c0dc-4a87-9aa8-df5d90cf2a02"), new DateTime(2021, 4, 13, 2, 27, 13, 973, DateTimeKind.Utc).AddTicks(3903), null, "Category 3", null, new DateTime(2021, 4, 13, 2, 27, 13, 973, DateTimeKind.Utc).AddTicks(3905) }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("32febb22-f596-4b1e-b0a8-b11ad54be200"), new DateTime(2021, 4, 13, 2, 27, 13, 970, DateTimeKind.Utc).AddTicks(2655), "Tag 1", new DateTime(2021, 4, 13, 2, 27, 13, 970, DateTimeKind.Utc).AddTicks(3541) },
                    { new Guid("32febb22-f596-4b1e-b0a8-b11ad54be201"), new DateTime(2021, 4, 13, 2, 27, 13, 970, DateTimeKind.Utc).AddTicks(8157), "Tag 2", new DateTime(2021, 4, 13, 2, 27, 13, 970, DateTimeKind.Utc).AddTicks(8175) },
                    { new Guid("32febb22-f596-4b1e-b0a8-b11ad54be203"), new DateTime(2021, 4, 13, 2, 27, 13, 970, DateTimeKind.Utc).AddTicks(8218), "Tag 3", new DateTime(2021, 4, 13, 2, 27, 13, 970, DateTimeKind.Utc).AddTicks(8219) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "EmailAddress", "FirstName", "LastName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("628019e2-665b-4a4c-ae23-4490ffd4fa00"), new DateTime(2021, 4, 13, 2, 27, 13, 973, DateTimeKind.Utc).AddTicks(9780), "jbeleren@mailinator.com", "Jace", "Beleren", new DateTime(2021, 4, 13, 2, 27, 13, 973, DateTimeKind.Utc).AddTicks(9784) },
                    { new Guid("628019e2-665b-4a4c-ae23-4490ffd4fa01"), new DateTime(2021, 4, 13, 2, 27, 13, 974, DateTimeKind.Utc).AddTicks(1622), "lvess@mailinator.com", "Liliana", "Vess", new DateTime(2021, 4, 13, 2, 27, 13, 974, DateTimeKind.Utc).AddTicks(1626) },
                    { new Guid("628019e2-665b-4a4c-ae23-4490ffd4fa02"), new DateTime(2021, 4, 13, 2, 27, 13, 974, DateTimeKind.Utc).AddTicks(1662), "cnalaar@mailinator.com", "Chandra", "Nalaar", new DateTime(2021, 4, 13, 2, 27, 13, 974, DateTimeKind.Utc).AddTicks(1663) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Name", "Price", "TagLine", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("254a99bb-bee5-43e0-80dc-e76622110500"), new Guid("02b92adc-c0dc-4a87-9aa8-df5d90cf2a00"), new DateTime(2021, 4, 13, 2, 27, 13, 973, DateTimeKind.Utc).AddTicks(5682), "Palmolive Naturals White with Milk Whitening Bar Soap 80g 2+1 Value Pack", "Product 1 Soap", 0m, "Pag di ka pumuti, iitim ka.", new DateTime(2021, 4, 13, 2, 27, 13, 973, DateTimeKind.Utc).AddTicks(5688) },
                    { new Guid("254a99bb-bee5-43e0-80dc-e76622110501"), new Guid("02b92adc-c0dc-4a87-9aa8-df5d90cf2a00"), new DateTime(2021, 4, 13, 2, 27, 13, 973, DateTimeKind.Utc).AddTicks(8430), "Global Version Xiaomi Redmi 9A Smartphones 2GB RAM 32GB ROM 6.53″ Intelligent Face Unlock Xiaomi Mall", "Product 2 Cellphone", 0m, "Pang ML", new DateTime(2021, 4, 13, 2, 27, 13, 973, DateTimeKind.Utc).AddTicks(8434) },
                    { new Guid("254a99bb-bee5-43e0-80dc-e76622110502"), new Guid("02b92adc-c0dc-4a87-9aa8-df5d90cf2a00"), new DateTime(2021, 4, 13, 2, 27, 13, 973, DateTimeKind.Utc).AddTicks(8492), "Marby Mini Monay 250g", "Product 3 Bread", 0m, "Busog ka agad", new DateTime(2021, 4, 13, 2, 27, 13, 973, DateTimeKind.Utc).AddTicks(8493) }
                });

            migrationBuilder.InsertData(
                table: "UserLogins",
                columns: new[] { "Id", "CreatedAt", "Key", "Type", "UpdatedAt", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("6d51e388-e135-4c85-b754-e86c2d0500f0"), new DateTime(2021, 4, 13, 2, 27, 13, 987, DateTimeKind.Utc).AddTicks(2987), "Password", 1, new DateTime(2021, 4, 13, 2, 27, 13, 987, DateTimeKind.Utc).AddTicks(3009), new Guid("628019e2-665b-4a4c-ae23-4490ffd4fa00"), "$2b$10$PNa3/L3clxgaXkkiNXmmR.Jqnsev7eZ/EBmdASqXpwTD3oxT77SNm" },
                    { new Guid("0d2698c8-7299-4c2f-8ea0-938c5643f8a9"), new DateTime(2021, 4, 13, 2, 27, 14, 118, DateTimeKind.Utc).AddTicks(8581), "LoginStatus", 0, new DateTime(2021, 4, 13, 2, 27, 14, 118, DateTimeKind.Utc).AddTicks(8605), new Guid("628019e2-665b-4a4c-ae23-4490ffd4fa00"), "Active" },
                    { new Guid("29b4e280-954a-49a4-9de5-3fc2a1fa45d8"), new DateTime(2021, 4, 13, 2, 27, 14, 118, DateTimeKind.Utc).AddTicks(8712), "LoginRetries", 1, new DateTime(2021, 4, 13, 2, 27, 14, 118, DateTimeKind.Utc).AddTicks(8713), new Guid("628019e2-665b-4a4c-ae23-4490ffd4fa00"), "0" },
                    { new Guid("e8f46698-775f-424c-82ca-ad187f3774c3"), new DateTime(2021, 4, 13, 2, 27, 14, 118, DateTimeKind.Utc).AddTicks(9158), "Password", 1, new DateTime(2021, 4, 13, 2, 27, 14, 118, DateTimeKind.Utc).AddTicks(9160), new Guid("628019e2-665b-4a4c-ae23-4490ffd4fa01"), "$2b$10$.5pS8PRo.1LXv2GwJDtIu.UYha5oyIX6O6BCphyCAZaPqEmf/Ax5e" },
                    { new Guid("49c3dbf5-a719-4f23-9969-8eda6c235b0f"), new DateTime(2021, 4, 13, 2, 27, 14, 237, DateTimeKind.Utc).AddTicks(8616), "LoginStatus", 0, new DateTime(2021, 4, 13, 2, 27, 14, 237, DateTimeKind.Utc).AddTicks(8643), new Guid("628019e2-665b-4a4c-ae23-4490ffd4fa01"), "Active" },
                    { new Guid("6332f116-ad65-4a47-b751-6fbb87588f63"), new DateTime(2021, 4, 13, 2, 27, 14, 237, DateTimeKind.Utc).AddTicks(8710), "LoginRetries", 1, new DateTime(2021, 4, 13, 2, 27, 14, 237, DateTimeKind.Utc).AddTicks(8711), new Guid("628019e2-665b-4a4c-ae23-4490ffd4fa01"), "0" },
                    { new Guid("7f36dcb8-b04f-43fe-b2b0-f280af2a898a"), new DateTime(2021, 4, 13, 2, 27, 14, 237, DateTimeKind.Utc).AddTicks(8765), "Password", 1, new DateTime(2021, 4, 13, 2, 27, 14, 237, DateTimeKind.Utc).AddTicks(8766), new Guid("628019e2-665b-4a4c-ae23-4490ffd4fa02"), "$2b$10$bu0MYxYWgclT6GUKOiA1/eO45mO6QXWadAfJ8I6WcebxUeex7hsoC" },
                    { new Guid("0b307598-d25c-476d-b61d-d2fd8e164d04"), new DateTime(2021, 4, 13, 2, 27, 14, 358, DateTimeKind.Utc).AddTicks(9470), "LoginStatus", 0, new DateTime(2021, 4, 13, 2, 27, 14, 358, DateTimeKind.Utc).AddTicks(9544), new Guid("628019e2-665b-4a4c-ae23-4490ffd4fa02"), "Active" },
                    { new Guid("54545c95-ca91-4323-9122-0f99508ca73f"), new DateTime(2021, 4, 13, 2, 27, 14, 358, DateTimeKind.Utc).AddTicks(9659), "LoginRetries", 1, new DateTime(2021, 4, 13, 2, 27, 14, 358, DateTimeKind.Utc).AddTicks(9690), new Guid("628019e2-665b-4a4c-ae23-4490ffd4fa02"), "0" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
