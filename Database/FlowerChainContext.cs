using Microsoft.EntityFrameworkCore;
using FlowerChainAPI.Models.Domain;

namespace FlowerChainAPI.Database
{
   

    public class FlowerChainContext : DbContext
    {
        public FlowerChainContext(DbContextOptions<FlowerChainContext> options) :base (options)
        {

        }
        public DbSet<Customer> Customer {get; set;}

        public DbSet<Employee> Employee {get; set;}
        public DbSet<FlowerBouquet> FlowerBouquet {get; set;}
        public DbSet<Flower> Flower {get; set;}
        public DbSet<FlowerShop> FlowerShop {get; set;}
        public DbSet<Order> Order {get; set;}
        public DbSet<Person> Person {get; set;}
        public DbSet<Supplier> Supplier {get; set;}
        public DbSet<FlowerFlowerBouquet> FlowerFlowerBouquet {get; set;}
        public DbSet<FlowerBouquetOrder> FlowerBouquetOrder {get; set;}
        public DbSet<FlowerShopSupplier> FlowerShopSupplier {get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //dataseeding flowers
            modelBuilder.Entity<Flower>().HasData(new Flower {id = 1, flowerType="Roos", price=1.2});
            modelBuilder.Entity<Flower>().HasData(new Flower {id = 2, flowerType="Madeliefje", price=1.3});
            modelBuilder.Entity<Flower>().HasData(new Flower {id = 3, flowerType="Vergeet-me-nietje", price=1});
            modelBuilder.Entity<Flower>().HasData(new Flower {id = 4, flowerType="Violetje", price=1.4});
            
            //dataseeding flowerbouquets
            modelBuilder.Entity<FlowerBouquet>().HasData(new FlowerBouquet {id = 1, bouquetName="Liefde", price=4 , amountSold=3 ,description="4 rozen en 2 violetjes"});
            modelBuilder.Entity<FlowerBouquet>().HasData(new FlowerBouquet {id = 2, bouquetName="Zonnenstraal", price=5 , amountSold=2 ,description="4 rozen en 6 vergeet me nietjes"});
            modelBuilder.Entity<FlowerBouquet>().HasData(new FlowerBouquet {id = 3, bouquetName="Welkom terug", price=6 , amountSold=4 ,description="4 rozen, 2 madeliefjes en 3 violetjes"});
            modelBuilder.Entity<FlowerBouquet>().HasData(new FlowerBouquet {id = 4, bouquetName="Tot ziens", price=5 , amountSold=1 ,description="4 rozen, 2 madeliefjes, 3 violetjes en 2 vergeet-me-nietjes"});
            
            //dataseeding flowershops
            modelBuilder.Entity<FlowerShop>().HasData(new FlowerShop {id = 1, shopName="De bloemenkrans", streetName="Van hoeystraat", houseNumber="52", city="Mechelen", postalCode="2800", phoneNumber="015635478", email="debloemenkrans@hotmail.com" });
            modelBuilder.Entity<FlowerShop>().HasData(new FlowerShop {id = 2, shopName="Het Blomke", streetName="Dorpstraat", houseNumber="3", city="Korbeek-Lo", postalCode="3000", phoneNumber="015435878", email="blomke@hotmail.com" });
            modelBuilder.Entity<FlowerShop>().HasData(new FlowerShop {id = 3, shopName="Peters bloemen", streetName="Broekstraat", houseNumber="14", city="Tielt-Winge", postalCode="3390", phoneNumber="016258843", email="petersbloemen@hotmail.com" });
        }

        

    }

   
}