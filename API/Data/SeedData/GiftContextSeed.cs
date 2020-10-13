using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using API.Contexts;
using API.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace API.Data.SeedData
{
    /// <summary>
    /// Class adds data in database if it's empty
    /// Reads json files and populates database with data
    /// </summary>
    public static class GiftContextSeed
    {
        /// <summary>
        /// Add categories and products in database if it's empty
        /// </summary>
        /// <param name="context">Entity framework context</param>
        /// <param name="loggerFactory">Logger</param>
        /// <returns></returns>
        public static async Task SeedAsync(GiftContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Categories.Any())
                {
                    var categoryData = File.ReadAllText(@"..\API\Data\SeedData\categories.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);

                    foreach (var category in categories)
                    {
                        context.Categories.Add(category);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    var productData = File.ReadAllText(@"..\API\Data\SeedData\products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);

                    foreach (var product in products)
                    {
                        context.Products.Add(product);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                loggerFactory.CreateLogger<GiftContext>().LogError($"{ex.Message} - Error adding categories to db");
            }
        }
    }
}