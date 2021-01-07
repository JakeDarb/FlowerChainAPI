using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using FlowerChainAPI.Models;
using FlowerChainAPI.Repositories;
using FlowerChainAPI.Database;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace FlowerChainAPI
{
    public class Startup
    {

        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        => this.Configuration = configuration;
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

           
            services.Configure<FlowerChainDatabaseSettings>(
                Configuration.GetSection(nameof(FlowerChainDatabaseSettings)));

            services.AddSingleton<IFlowerChainDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<FlowerChainDatabaseSettings>>().Value);

                services.AddSingleton<FlowerBouquetOrderRepository>();

                

           services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Latest); 
           
           services.AddScoped<IFlowerBouquetRepository, FlowerBouquetRepository>();
           services.AddScoped<IFlowerShopRepository, FlowerShopRepository>();
           

           //database connection
           services.AddDbContextPool<FlowerChainContext>(    
                dbContextOptions => dbContextOptions
                    .UseMySql(
                        // Replace with your connection string.
                        Configuration.GetConnectionString("GlobalDatabase"),
                        // Replace with your server version and type.
                         new MySqlServerVersion(new Version(8, 0, 21)),
                        mySqlOptions => mySqlOptions
                            .CharSetBehavior(CharSetBehavior.NeverAppend))
                            
                    // Everything from this point on is optional but helps with debugging.
                    .UseLoggerFactory(
                        LoggerFactory.Create(
                            logging => logging
                                .AddConsole()
                                .AddFilter(level => level >= LogLevel.Information)))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

        services.AddSwaggerGen();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMvc();

             app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                 c.RoutePrefix = string.Empty;
             });

             app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
            
        
        }
    }
}
