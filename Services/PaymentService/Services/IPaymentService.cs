using PaymentService.DTOs;
using PaymentService.Models;

namespace PaymentService.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPayments();
        Task<Payment> GetPaymentById(int id);
        Task<Payment> GetPaymentByOrderId(int orderId);
        Task<PaymentDto> ProcessPayment(CreatePaymentDto paymentDto);
        Task<bool> RefundPayment(int id);
    }
}