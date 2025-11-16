using Microsoft.AspNetCore.Mvc;
using CartService.DTOs;
using CartService.Services;

namespace CartService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ShoppingCartDto>> GetCart(string userId)
        {
            try
            {
                var cart = await _cartService.GetCart(userId);
                var cartDto = new ShoppingCartDto
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
                return Ok(cartDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching cart for user {UserId}", userId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("{userId}/items")]
        public async Task<ActionResult<ShoppingCartDto>> AddItem(string userId, [FromBody] AddItemDto item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var cart = await _cartService.AddItem(userId, item);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding item to cart for user {UserId}", userId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{userId}/items/{productId}")]
        public async Task<ActionResult<ShoppingCartDto>> RemoveItem(string userId, string productId)
        {
            try
            {
                var cart = await _cartService.RemoveItem(userId, productId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing item from cart for user {UserId}", userId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{userId}/items/{productId}")]
        public async Task<ActionResult<ShoppingCartDto>> UpdateItemQuantity(string userId, string productId, [FromBody] int quantity)
        {
            try
            {
                if (quantity <= 0)
                {
                    return BadRequest("Quantity must be greater than zero");
                }

                var cart = await _cartService.UpdateItemQuantity(userId, productId, quantity);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating item quantity in cart for user {UserId}", userId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> ClearCart(string userId)
        {
            try
            {
                await _cartService.ClearCart(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while clearing cart for user {UserId}", userId);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}