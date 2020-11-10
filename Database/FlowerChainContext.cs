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

    }

   
}