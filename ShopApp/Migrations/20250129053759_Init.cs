using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopApp.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PromoCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CartId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CartId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "OrderId", "PromoCode", "Total", "UserName" },
                values: new object[,]
                {
                    { new Guid("59c2fa95-4ef3-4299-8468-b4c43f9cfd72"), new Guid("271313ab-bace-4132-8564-f7faf9e64bb4"), "Promo_Test1", 100m, "dog1" },
                    { new Guid("7e04bc7b-d205-4f9b-a21b-d71a241db55c"), new Guid("e40e3199-6e85-405a-b0d7-6bea67bf8afd"), "Last", 70m, "ivan2" },
                    { new Guid("870e2f1e-e06f-4877-b90f-3e33e8b7ed83"), new Guid("6dc7864f-1cdf-4458-aec3-521bd5a8336c"), "Promo_Test1", 123.11m, "ivan1" },
                    { new Guid("929644b5-66d9-48d2-8955-b10f64c72961"), new Guid("ec40d1d6-0b96-431a-98de-58ab42ff5488"), "Next", 60m, "petrov3" },
                    { new Guid("bb0a0d79-79cd-474a-bbec-a3ac899827a2"), new Guid("3f46a3a1-7d10-48ca-b7f7-d1ee1a000358"), "J1N", 80m, "pet2" },
                    { new Guid("c88df46f-8924-4cab-8d4d-9f67299aaf3d"), new Guid("47f5bb79-4112-4e6a-9336-2f9d35a1b06d"), "Free", 90m, "dog1" },
                    { new Guid("e87d21d1-96dc-436e-92a6-283884af589e"), new Guid("5956302c-6aa6-43de-afd1-0b527473f933"), "Free", 100m, "ivan2" },
                    { new Guid("f171504b-fce1-46d8-a44b-6e7f545a875e"), new Guid("4f7e9fe7-b11d-44f1-ac66-2b2f8637c328"), "Promo_Test1", 90m, "ivanov3" },
                    { new Guid("f1ab33be-cd77-4904-9859-f8714c69bd34"), null, "Sale1", 50.00m, "petrov2" }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "CartId", "Count", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("4092421e-2500-430e-b1cd-95139a9b2121"), new Guid("7e04bc7b-d205-4f9b-a21b-d71a241db55c"), 1, "Пластилин", 50.00m },
                    { new Guid("5528c8c3-cfa5-4fb7-9cdf-2d287a444435"), new Guid("59c2fa95-4ef3-4299-8468-b4c43f9cfd72"), 1, "Комод", 100.00m },
                    { new Guid("6b4fe1ae-e223-422d-a0dd-1d9782ac7548"), new Guid("f171504b-fce1-46d8-a44b-6e7f545a875e"), 1, "Тетрадь", 90.00m },
                    { new Guid("6f7a011a-003e-48e2-9c32-4643f238d8b7"), new Guid("bb0a0d79-79cd-474a-bbec-a3ac899827a2"), 1, "Стол", 80.00m },
                    { new Guid("73c2742d-2201-431d-8197-62bb7e357495"), new Guid("c88df46f-8924-4cab-8d4d-9f67299aaf3d"), 1, "Линейка", 90.00m },
                    { new Guid("7b41da5a-0738-49c0-8db0-ac81c712c860"), new Guid("e87d21d1-96dc-436e-92a6-283884af589e"), 1, "Стул", 50.00m },
                    { new Guid("7eb24925-b00f-4c90-9a78-f465be9e3669"), new Guid("870e2f1e-e06f-4877-b90f-3e33e8b7ed83"), 2, "Тетрадь", 50.00m },
                    { new Guid("8412671e-997c-4214-a53e-9b266ded9158"), new Guid("e87d21d1-96dc-436e-92a6-283884af589e"), 1, "Пластилин", 50.00m },
                    { new Guid("9839e221-f9e5-4a2a-8d7f-29dc4c5d594e"), new Guid("929644b5-66d9-48d2-8955-b10f64c72961"), 1, "Атлас", 60.00m },
                    { new Guid("a8cd310d-1e52-44e1-b6b7-478a682c80cb"), new Guid("f1ab33be-cd77-4904-9859-f8714c69bd34"), 5, "Ластик", 10.00m },
                    { new Guid("b2a91a99-6cf6-48ed-a281-f0001299e84a"), new Guid("7e04bc7b-d205-4f9b-a21b-d71a241db55c"), 1, "Карандаш", 20.00m },
                    { new Guid("fd2efbd8-7969-4210-a283-760b6d63779b"), new Guid("870e2f1e-e06f-4877-b90f-3e33e8b7ed83"), 1, "Карандаш", 23.11m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CartId", "Status" },
                values: new object[,]
                {
                    { new Guid("271313ab-bace-4132-8564-f7faf9e64bb4"), new Guid("59c2fa95-4ef3-4299-8468-b4c43f9cfd72"), "Fail" },
                    { new Guid("3f46a3a1-7d10-48ca-b7f7-d1ee1a000358"), new Guid("bb0a0d79-79cd-474a-bbec-a3ac899827a2"), "Paid" },
                    { new Guid("47f5bb79-4112-4e6a-9336-2f9d35a1b06d"), new Guid("c88df46f-8924-4cab-8d4d-9f67299aaf3d"), "Paid" },
                    { new Guid("4f7e9fe7-b11d-44f1-ac66-2b2f8637c328"), new Guid("f171504b-fce1-46d8-a44b-6e7f545a875e"), "Refunded" },
                    { new Guid("5956302c-6aa6-43de-afd1-0b527473f933"), new Guid("e87d21d1-96dc-436e-92a6-283884af589e"), "Init" },
                    { new Guid("6dc7864f-1cdf-4458-aec3-521bd5a8336c"), new Guid("870e2f1e-e06f-4877-b90f-3e33e8b7ed83"), "Init" },
                    { new Guid("e40e3199-6e85-405a-b0d7-6bea67bf8afd"), new Guid("7e04bc7b-d205-4f9b-a21b-d71a241db55c"), "Refunded" },
                    { new Guid("ec40d1d6-0b96-431a-98de-58ab42ff5488"), new Guid("929644b5-66d9-48d2-8955-b10f64c72961"), "Init" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CartId",
                table: "Orders",
                column: "CartId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Carts");
        }
    }
}
