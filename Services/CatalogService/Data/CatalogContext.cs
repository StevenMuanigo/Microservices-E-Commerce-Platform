using CatalogService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CatalogService.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IOptions<CatalogDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            Products = mongoDatabase.GetCollection<Product>(databaseSettings.Value.ProductsCollectionName);
            
            // Seed data if collection is empty
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}