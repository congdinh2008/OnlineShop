using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Data
{
    public static class DbInitializer
    {
        public static async Task SeedDate(OnlineShopDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Categories.Any())
            {
                return;
            }
            var categories = new Category[]
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name="Category 01",
                    Notes="This is Category 01",
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name="Category 02",
                    Notes="This is Category 02",
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name="Category 03",
                    Notes="This is Category 03",
                }
            };

            await context.Categories.AddRangeAsync(categories);

            var products = new List<Product>()
            {

            }
        }
    }
}
