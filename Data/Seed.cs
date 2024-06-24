using E_commercial_Web_RESTAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace E_commercial_Web_RESTAPI.Data
{
    public class Seed
    {

       


        public static async Task SeedAsync(ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            var logger = loggerFactory.CreateLogger<Seed>();

            try
            {
                var context = new ApplicationDBcontext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDBcontext>>());
                string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var filePath = Path.Combine(path,"Data", "SeedData", "products.json");


                logger.LogInformation($"Checking if file exists at: {filePath}");
              
                if(File.Exists(filePath))
                {
                    logger.LogInformation("File found, reading data...");
                    var Data = await File.ReadAllTextAsync(filePath);

                    var productList = JsonConvert.DeserializeObject<List<Product>>(Data);
                    if (productList != null && productList.Any())
                    {
                        logger.LogInformation("Adding products to the database...");
                       
                        await context.products.AddRangeAsync(productList);
                        await context.SaveChangesAsync();
                        logger.LogInformation("Database seeding completed.");
                    }
                    else
                    {
                        logger = loggerFactory.CreateLogger<Seed>();
                        logger.LogWarning("No products found in the JSON file.");
                    }


                }
                else
                {
                    logger.LogWarning($"Seed data file not found: {filePath}");
                }
            }
            
            catch (Exception ex)
            {


                logger.LogError(ex, "An error occurred during seeding.");
            }
        }
    }
}
