using CatalogService.DTOs;
using CatalogService.Models;

namespace CatalogService.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByCategory(string categoryName);
        Task<Product> CreateProduct(CreateProductDto productDto);
        Task<bool> UpdateProduct(UpdateProductDto productDto);
        Task<bool> DeleteProduct(string id);
    }
}