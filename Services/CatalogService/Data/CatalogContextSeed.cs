using CatalogService.Models;
using MongoDB.Driver;

namespace CatalogService.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "iPhone 15",
                    Summary = "This phone is the company's biggest change to its flagship smartphone in years.",
                    Description = "The iPhone 15 display has rounded corners that follow a beautiful curved design, and these corners are within a standard rectangle. When measured as a standard rectangular shape, the screen is 6.1 inches diagonally (actual viewable area is less).",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    Category = "Smart Phone",
                    Stock = 100
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Samsung Galaxy S24",
                    Summary = "Samsung Galaxy S24 is the latest flagship smartphone from Samsung.",
                    Description = "The Samsung Galaxy S24 features a stunning display, powerful processor, and advanced camera system.",
                    ImageFile = "product-2.png",
                    Price = 850.00M,
                    Category = "Smart Phone",
                    Stock = 150
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name = "MacBook Pro",
                    Summary = "The ultimate pro notebook from Apple.",
                    Description = "The MacBook Pro features the powerful M3 chip, stunning Retina display, and all-day battery life.",
                    ImageFile = "product-3.png",
                    Price = 2500.00M,
                    Category = "Laptop",
                    Stock = 50
                }
            };
        }
    }
}