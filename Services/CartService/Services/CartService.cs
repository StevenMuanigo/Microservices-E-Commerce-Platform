using CartService.DTOs;
using CartService.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CartService.Services
{
    public class CartService : ICartService
    {
        private readonly IConnectionMultiplexer _redis;

        public CartService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<ShoppingCart> GetCart(string userId)
        {
            var db = _redis.GetDatabase();
            var cartJson = await db.StringGetAsync($"cart:{userId}");
            
            if (string.IsNullOrEmpty(cartJson))
            {
                return new ShoppingCart { UserId = userId };
            }
            
            return JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
        }

        public async Task<ShoppingCartDto> AddItem(string userId, AddItemDto item)
        {
            var cart = await GetCart(userId);
            
            // Check if item already exists in cart
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    ImageFile = item.ImageFile
                });
            }
            
            await SaveCart(userId, cart);
            
            return MapToDto(cart);
        }

        public async Task<ShoppingCartDto> RemoveItem(string userId, string productId)
        {
            var cart = await GetCart(userId);
            cart.Items.RemoveAll(i => i.ProductId == productId);
            await SaveCart(userId, cart);
            
            return MapToDto(cart);
        }

        public async Task<ShoppingCartDto> UpdateItemQuantity(string userId, string productId, int quantity)
        {
            var cart = await GetCart(userId);
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            
            if (item != null)
            {
                if (quantity <= 0)
                {
                    cart.Items.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
                
                await SaveCart(userId, cart);
            }
            
            return MapToDto(cart);
        }

        public async Task ClearCart(string userId)
        {
            var db = _redis.GetDatabase();
            await db.KeyDeleteAsync($"cart:{userId}");
        }

        private async Task SaveCart(string userId, ShoppingCart cart)
        {
            var db = _redis.GetDatabase();
            var cartJson = JsonConvert.SerializeObject(cart);
            await db.StringSetAsync($"cart:{userId}", cartJson, TimeSpan.FromDays(30));
        }

        private ShoppingCartDto MapToDto(ShoppingCart cart)
        {
            return new ShoppingCartDto
            {
                UserId = cart.UserId,
                Items = cart.Items.Select(item => new CartItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    ImageFile = item.ImageFile
                }).ToList(),
                TotalPrice = cart.TotalPrice
            };
        }
    }
}