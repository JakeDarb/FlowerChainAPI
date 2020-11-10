using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowerChainAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flowers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropColumn(
                name: "supplierId",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "customerId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "employeeId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "amount",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "orderId",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "houseNumber",
                table: "Supplier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "personId",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Employee_personId",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "flowerShopId",
                table: "Person",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "houseNumber",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "personId",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "houseNumber",
                table: "FlowerShop",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "FlowerBouquet",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "FlowerBouquet",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Flower",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    flowerType = table.Column<string>(nullable: true),
                    price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flower", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flower");

            migrationBuilder.DropColumn(
                name: "houseNumber",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "personId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Employee_personId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "flowerShopId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "houseNumber",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "personId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "houseNumber",
                table: "FlowerShop");

            migrationBuilder.DropColumn(
                name: "description",
                table: "FlowerBouquet");

            migrationBuilder.DropColumn(
                name: "price",
                table: "FlowerBouquet");

            migrationBuilder.AddColumn<string>(
                name: "supplierId",
                table: "Supplier",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "customerId",
                table: "Person",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "employeeId",
                table: "Person",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "amount",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "orderId",
                table: "Order",
                type: "longtext",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Flowers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    flowerType = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flowers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    buyPrice = table.Column<double>(type: "double", nullable: false),
                    sellPrice = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }
    }
}
