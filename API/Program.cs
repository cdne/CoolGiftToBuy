using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contexts;
using API.Data.SeedData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = service.GetRequiredService<GiftContext>();
                    await context.Database.MigrateAsync();
                    await GiftContextSeed.SeedAsync(context, loggerFactory);
                }
                catch(Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error ocurred during migrations");
                }
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}