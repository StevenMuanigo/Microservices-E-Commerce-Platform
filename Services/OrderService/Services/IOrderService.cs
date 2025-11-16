using OrderService.DTOs;
using OrderService.Models;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<Order>> GetOrdersByUserId(int userId);
        Task<OrderDto> CreateOrder(CreateOrderDto orderDto);
        Task<bool> UpdateOrderStatus(int id, OrderStatus status);
        Task<bool> DeleteOrder(int id);
    }
}