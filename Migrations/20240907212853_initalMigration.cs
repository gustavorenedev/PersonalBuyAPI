using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class initalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Client_PX",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Address = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Client_PX", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "TB_Product_PX",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Quantity = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Product_PX", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "TB_Cart_PX",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ClientId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Cart_PX", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_TB_Cart_PX_TB_Client_PX_ClientId",
                        column: x => x.ClientId,
                        principalTable: "TB_Client_PX",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ItemCart_PX",
                columns: table => new
                {
                    ItemCartId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ProductId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Quantity = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CartId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ItemCart_PX", x => x.ItemCartId);
                    table.ForeignKey(
                        name: "FK_TB_ItemCart_PX_TB_Cart_PX_CartId",
                        column: x => x.CartId,
                        principalTable: "TB_Cart_PX",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ItemCart_PX_TB_Product_PX_ProductId",
                        column: x => x.ProductId,
                        principalTable: "TB_Product_PX",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Cart_PX_ClientId",
                table: "TB_Cart_PX",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ItemCart_PX_CartId",
                table: "TB_ItemCart_PX",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ItemCart_PX_ProductId",
                table: "TB_ItemCart_PX",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ItemCart_PX");

            migrationBuilder.DropTable(
                name: "TB_Cart_PX");

            migrationBuilder.DropTable(
                name: "TB_Product_PX");

            migrationBuilder.DropTable(
                name: "TB_Client_PX");
        }
    }
}
