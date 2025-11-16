using CartService.DTOs;
using CartService.Models;

namespace CartService.Services
{
    public interface ICartService
    {
        Task<ShoppingCart> GetCart(string userId);
        Task<ShoppingCartDto> AddItem(string userId, AddItemDto item);
        Task<ShoppingCartDto> RemoveItem(string userId, string productId);
        Task<ShoppingCartDto> UpdateItemQuantity(string userId, string productId, int quantity);
        Task ClearCart(string userId);
    }
}using CartService.DTOs;
using CartService.Models;

namespace CartService.Services
{
    public interface ICartService
    {
        Task<ShoppingCart> GetCart(string userId);
        Task<ShoppingCartDto> AddItem(string userId, AddItemDto item);
        Task<ShoppingCartDto> RemoveItem(string userId, string productId);
        Task<ShoppingCartDto> UpdateItemQuantity(string userId, string productId, int quantity);
        Task ClearCart(string userId);
    }
}