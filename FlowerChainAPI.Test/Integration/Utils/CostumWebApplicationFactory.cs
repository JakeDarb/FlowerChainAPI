using System;
using System.Linq;
using FlowerChainAPI.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlowerChainAPI.Tests.Integration.Utils
{
    
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<FlowerChainContext>));

                services.Remove(descriptor);
                var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContextPool<FlowerChainContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);

                });


                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<FlowerChainContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                db.Database.EnsureCreated();
            });
        }

        // You can think of Action<...> as a reference to a method that is being passed.
        public void ResetAndSeedDatabase(Action<FlowerChainContext> contextFiller)
        {
            // Retrieve a service scope and a database-context instance.
            using var scope = Services.CreateScope();
            var scopedServices = scope.ServiceProvider;

            var db = scopedServices.GetRequiredService<FlowerChainContext>();
            // Clear the database
            db.Order.RemoveRange(db.Order.ToList());
            db.FlowerBouquet.RemoveRange(db.FlowerBouquet.ToList());
            db.FlowerShop.RemoveRange(db.FlowerShop.ToList());
            db.SaveChanges();

            // execute the method using retrieved database as parameter
            contextFiller(db);

            db.SaveChanges();
        }
        
    }
}