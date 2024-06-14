using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mdswebapi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    cateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    cateDesc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__categori__A88B4DC41691FD27", x => x.cateID);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    customerPhone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    customerEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    customerAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    customerLogin = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    customerPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__customer__B611CB9D99012085", x => x.customerID);
                });

            migrationBuilder.CreateTable(
                name: "orderStatus",
                columns: table => new
                {
                    osID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    osDesc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__orderSta__5241DF11B0C6ADFD", x => x.osID);
                });

            migrationBuilder.CreateTable(
                name: "pharmacies",
                columns: table => new
                {
                    pharID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pharLogin = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    pharPass = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    pharName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    pharPhone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    pharEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    pharAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__pharmaci__8E7B6B32784A7171", x => x.pharID);
                });

            migrationBuilder.CreateTable(
                name: "promotions",
                columns: table => new
                {
                    promoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    promoName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    startDate = table.Column<DateOnly>(type: "date", nullable: true),
                    endDate = table.Column<DateOnly>(type: "date", nullable: true),
                    discountPercent = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__promotio__E19E71D6C784FB3C", x => x.promoID);
                });

            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    cartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cart__415B03D8CF0AF4A4", x => x.cartID);
                    table.ForeignKey(
                        name: "FK__cart__customerID__5EBF139D",
                        column: x => x.customerID,
                        principalTable: "customers",
                        principalColumn: "customerID");
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    osID = table.Column<int>(type: "int", nullable: true),
                    customerID = table.Column<int>(type: "int", nullable: true),
                    orderPlacedAt = table.Column<DateOnly>(type: "date", nullable: true),
                    orderDeliveredAt = table.Column<DateOnly>(type: "date", nullable: true),
                    shippingFees = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order__0809337D8B57FA0F", x => x.orderID);
                    table.ForeignKey(
                        name: "FK__order__customerI__628FA481",
                        column: x => x.customerID,
                        principalTable: "customers",
                        principalColumn: "customerID");
                    table.ForeignKey(
                        name: "FK__order__osID__619B8048",
                        column: x => x.osID,
                        principalTable: "orderStatus",
                        principalColumn: "osID");
                });

            migrationBuilder.CreateTable(
                name: "medicines",
                columns: table => new
                {
                    medID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    medName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    medDesc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    medDiscount = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    medRemain = table.Column<int>(type: "int", nullable: true),
                    medPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    medPicture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    cateID = table.Column<int>(type: "int", nullable: true),
                    pharID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__medicine__2D4FA91CF950ACE6", x => x.medID);
                    table.ForeignKey(
                        name: "FK__medicines__cateI__5DCAEF64",
                        column: x => x.cateID,
                        principalTable: "categories",
                        principalColumn: "cateID");
                    table.ForeignKey(
                        name: "FK__medicines__pharI__656C112C",
                        column: x => x.pharID,
                        principalTable: "pharmacies",
                        principalColumn: "pharID");
                });

            migrationBuilder.CreateTable(
                name: "cartDetail",
                columns: table => new
                {
                    cdID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cartID = table.Column<int>(type: "int", nullable: true),
                    medID = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cartDeta__289C55A48E433A9B", x => x.cdID);
                    table.ForeignKey(
                        name: "FK__cartDetai__cartI__60A75C0F",
                        column: x => x.cartID,
                        principalTable: "cart",
                        principalColumn: "cartID");
                    table.ForeignKey(
                        name: "FK__cartDetai__medID__5FB337D6",
                        column: x => x.medID,
                        principalTable: "medicines",
                        principalColumn: "medID");
                });

            migrationBuilder.CreateTable(
                name: "orderItem",
                columns: table => new
                {
                    orderItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderID = table.Column<int>(type: "int", nullable: true),
                    medID = table.Column<int>(type: "int", nullable: true),
                    itemQuantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__orderIte__3724BD72FB0C49AF", x => x.orderItemID);
                    table.ForeignKey(
                        name: "FK__orderItem__medID__6477ECF3",
                        column: x => x.medID,
                        principalTable: "medicines",
                        principalColumn: "medID");
                    table.ForeignKey(
                        name: "FK__orderItem__order__6383C8BA",
                        column: x => x.orderID,
                        principalTable: "order",
                        principalColumn: "orderID");
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    reviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerID = table.Column<int>(type: "int", nullable: true),
                    medID = table.Column<int>(type: "int", nullable: true),
                    reviewContent = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__reviews__2ECD6E24E98902BA", x => x.reviewID);
                    table.ForeignKey(
                        name: "FK__reviews__custome__6754599E",
                        column: x => x.customerID,
                        principalTable: "customers",
                        principalColumn: "customerID");
                    table.ForeignKey(
                        name: "FK__reviews__medID__66603565",
                        column: x => x.medID,
                        principalTable: "medicines",
                        principalColumn: "medID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_cart_customerID",
                table: "cart",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_cartDetail_cartID",
                table: "cartDetail",
                column: "cartID");

            migrationBuilder.CreateIndex(
                name: "IX_cartDetail_medID",
                table: "cartDetail",
                column: "medID");

            migrationBuilder.CreateIndex(
                name: "IX_medicines_cateID",
                table: "medicines",
                column: "cateID");

            migrationBuilder.CreateIndex(
                name: "IX_medicines_pharID",
                table: "medicines",
                column: "pharID");

            migrationBuilder.CreateIndex(
                name: "IX_order_customerID",
                table: "order",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_order_osID",
                table: "order",
                column: "osID");

            migrationBuilder.CreateIndex(
                name: "IX_orderItem_medID",
                table: "orderItem",
                column: "medID");

            migrationBuilder.CreateIndex(
                name: "IX_orderItem_orderID",
                table: "orderItem",
                column: "orderID");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_customerID",
                table: "reviews",
                column: "customerID");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_medID",
                table: "reviews",
                column: "medID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cartDetail");

            migrationBuilder.DropTable(
                name: "orderItem");

            migrationBuilder.DropTable(
                name: "promotions");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "medicines");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "orderStatus");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "pharmacies");
        }
    }
}
