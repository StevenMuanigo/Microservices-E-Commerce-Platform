using CatalogService.Models;
using MongoDB.Driver;

namespace CatalogService.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}