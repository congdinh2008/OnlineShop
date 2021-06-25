using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Data
{
    public static class DbInitializer
    {
        public static void SeedData(OnlineShopDbContext context)
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

            context.Categories.AddRange(categories);

            var products = new List<Product>()
            {
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 01",
                    Summary = "This is product 01",
                    Price = 3.99M,
                    ImageUrl = "product01.jpg",
                    Category = categories.Single(x=>x.Name == "Category 01")
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 02",
                    Summary = "This is product 02",
                    Price = 3.99M,
                    ImageUrl = "product02.jpg",
                    Category = categories.Single(x=>x.Name == "Category 01")
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 03",
                    Summary = "This is product 03",
                    Price = 3.99M,
                    ImageUrl = "product03.jpg",
                    Category = categories.Single(x=>x.Name == "Category 02")
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 04",
                    Summary = "This is product 04",
                    Price = 3.99M,
                    ImageUrl = "product04.jpg",
                    Category = categories.Single(x=>x.Name == "Category 03")
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 05",
                    Summary = "This is product 05",
                    Price = 3.99M,
                    ImageUrl = "product05.jpg",
                    Category = categories.Single(x=>x.Name == "Category 02")
                },
            };
            context.Products.AddRange(products);

            context.SaveChanges();
        }
    }
}
