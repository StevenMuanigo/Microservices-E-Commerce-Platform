using PaymentService.DTOs;
using PaymentService.Models;

namespace PaymentService.Services
{
    public class PaymentService : IPaymentService
    {
        // In-memory storage for payments (in a real application, this would be a database)
        private static readonly List<Payment> _payments = new List<Payment>();
        private static int _nextId = 1;

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await Task.FromResult(_payments);
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            var payment = _payments.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(payment);
        }

        public async Task<Payment> GetPaymentByOrderId(int orderId)
        {
            var payment = _payments.FirstOrDefault(p => p.OrderId == orderId);
            return await Task.FromResult(payment);
        }

        public async Task<PaymentDto> ProcessPayment(CreatePaymentDto paymentDto)
        {
            // Simulate payment processing
            await Task.Delay(1000); // Simulate network delay

            // In a real application, you would integrate with a payment provider like Stripe or PayPal
            // For this mock implementation, we'll simulate a successful payment

            var payment = new Payment
            {
                Id = _nextId++,
                OrderId = paymentDto.OrderId,
                Amount = paymentDto.Amount,
                Method = paymentDto.Method,
                Status = PaymentStatus.Completed,
                TransactionId = GenerateTransactionId(),
                CardLastFourDigits = paymentDto.CardNumber.Length >= 4 
                    ? paymentDto.CardNumber.Substring(paymentDto.CardNumber.Length - 4) 
                    : string.Empty
            };

            _payments.Add(payment);

            return await Task.FromResult(new PaymentDto
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                Method = payment.Method,
                Status = payment.Status,
                PaymentDate = payment.PaymentDate,
                TransactionId = payment.TransactionId
            });
        }

        public async Task<bool> RefundPayment(int id)
        {
            var payment = _payments.FirstOrDefault(p => p.Id == id);
            if (payment == null)
                return await Task.FromResult(false);

            // Simulate refund processing
            await Task.Delay(1000); // Simulate network delay

            payment.Status = PaymentStatus.Refunded;
            return await Task.FromResult(true);
        }

        private string GenerateTransactionId()
        {
            return "txn_" + Guid.NewGuid().ToString("N")[..16];
        }
    }
}