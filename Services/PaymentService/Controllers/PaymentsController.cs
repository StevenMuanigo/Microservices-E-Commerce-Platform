using Microsoft.AspNetCore.Mvc;
using PaymentService.DTOs;
using PaymentService.Services;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAllPayments()
        {
            try
            {
                var payments = await _paymentService.GetAllPayments();
                var paymentDtos = payments.Select(p => new PaymentDto
                {
                    Id = p.Id,
                    OrderId = p.OrderId,
                    Amount = p.Amount,
                    Method = p.Method,
                    Status = p.Status,
                    PaymentDate = p.PaymentDate,
                    TransactionId = p.TransactionId
                });
                return Ok(paymentDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching payments");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            try
            {
                var payment = await _paymentService.GetPaymentById(id);
                if (payment == null)
                {
                    return NotFound($"Payment with ID {id} not found");
                }

                var paymentDto = new PaymentDto
                {
                    Id = payment.Id,
                    OrderId = payment.OrderId,
                    Amount = payment.Amount,
                    Method = payment.Method,
                    Status = payment.Status,
                    PaymentDate = payment.PaymentDate,
                    TransactionId = payment.TransactionId
                };

                return Ok(paymentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching payment with ID {PaymentId}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("order/{orderId:int}")]
        public async Task<ActionResult<PaymentDto>> GetPaymentByOrderId(int orderId)
        {
            try
            {
                var payment = await _paymentService.GetPaymentByOrderId(orderId);
                if (payment == null)
                {
                    return NotFound($"Payment for order ID {orderId} not found");
                }

                var paymentDto = new PaymentDto
                {
                    Id = payment.Id,
                    OrderId = payment.OrderId,
                    Amount = payment.Amount,
                    Method = payment.Method,
                    Status = payment.Status,
                    PaymentDate = payment.PaymentDate,
                    TransactionId = payment.TransactionId
                };

                return Ok(paymentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching payment for order ID {OrderId}", orderId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> ProcessPayment([FromBody] CreatePaymentDto paymentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var payment = await _paymentService.ProcessPayment(paymentDto);
                return Ok(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing payment");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("{id:int}/refund")]
        public async Task<ActionResult<bool>> RefundPayment(int id)
        {
            try
            {
                var result = await _paymentService.RefundPayment(id);
                if (!result)
                {
                    return NotFound($"Payment with ID {id} not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while refunding payment with ID {PaymentId}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}