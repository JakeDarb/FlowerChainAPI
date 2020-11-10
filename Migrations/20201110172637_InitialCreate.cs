using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowerChainAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    newsLetter = table.Column<bool>(nullable: false),
                    personId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    workStartDate = table.Column<DateTime>(nullable: false),
                    personId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Flower",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    flowerType = table.Column<string>(nullable: true),
                    price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flower", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FlowerBouquet",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    bouquetName = table.Column<string>(nullable: true),
                    price = table.Column<double>(nullable: false),
                    amountSold = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerBouquet", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FlowerBouquetOrder",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    orderId = table.Column<int>(nullable: false),
                    flowerBouquetId = table.Column<int>(nullable: false),
                    amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerBouquetOrder", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FlowerFlowerBouquet",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    flowerId = table.Column<int>(nullable: false),
                    flowerBouquetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerFlowerBouquet", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FlowerShop",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    shopName = table.Column<string>(nullable: true),
                    streetName = table.Column<string>(nullable: true),
                    postalCode = table.Column<string>(nullable: true),
                    houseNumber = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerShop", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FlowerShopSupplier",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    flowerShopId = table.Column<int>(nullable: false),
                    supplierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerShopSupplier", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    datTimeOrder = table.Column<DateTime>(nullable: false),
                    personId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    streetName = table.Column<string>(nullable: true),
                    postalCode = table.Column<string>(nullable: true),
                    houseNumber = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    dateOfBirth = table.Column<DateTime>(nullable: false),
                    isEmployee = table.Column<bool>(nullable: false),
                    flowerShopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    shopName = table.Column<string>(nullable: true),
                    streetName = table.Column<string>(nullable: true),
                    postalCode = table.Column<string>(nullable: true),
                    houseNumber = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Flower",
                columns: new[] { "id", "flowerType", "price" },
                values: new object[,]
                {
                    { 1, "Roos", 1.2 },
                    { 2, "Madeliefje", 1.3 },
                    { 3, "Vergeet-me-nietje", 1.0 },
                    { 4, "Violetje", 1.3999999999999999 }
                });

            migrationBuilder.InsertData(
                table: "FlowerBouquet",
                columns: new[] { "id", "amountSold", "bouquetName", "description", "price" },
                values: new object[,]
                {
                    { 1, 3, "Liefde", "4 rozen en 2 violetjes", 4.0 },
                    { 2, 2, "Zonnenstraal", "4 rozen en 6 vergeet me nietjes", 5.0 },
                    { 3, 4, "Welkom terug", "4 rozen, 2 madeliefjes en 3 violetjes", 6.0 },
                    { 4, 1, "Tot ziens", "4 rozen, 2 madeliefjes, 3 violetjes en 2 vergeet-me-nietjes", 5.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Flower");

            migrationBuilder.DropTable(
                name: "FlowerBouquet");

            migrationBuilder.DropTable(
                name: "FlowerBouquetOrder");

            migrationBuilder.DropTable(
                name: "FlowerFlowerBouquet");

            migrationBuilder.DropTable(
                name: "FlowerShop");

            migrationBuilder.DropTable(
                name: "FlowerShopSupplier");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
