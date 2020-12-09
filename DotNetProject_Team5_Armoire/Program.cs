using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetProject_Team5_Armoire.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetProject_Team5_Armoire
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
             var host = CreateHostBuilder(args).Build();
            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var dbCategory = services.GetRequiredService<ClothDbContext>();
                    await CategoryDbSeeder.SeedAsync(dbCategory); 
                    

                }
                catch (Exception e)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(e, "An error occured while seeding the Db");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
