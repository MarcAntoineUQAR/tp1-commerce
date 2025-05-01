using EC_Payments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EC_Payments.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ECPaymentsContext _context;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(ECPaymentsContext context, ILogger<PaymentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            try
            {
                var payment = await _context.PaymentInfos.FindAsync(id);

                if (payment == null)
                {
                    _logger.LogWarning("Payment with ID {PaymentId} not found.", id);
                    return NotFound($"Payment with ID {id} not found.");
                }

                return Ok(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving payment with ID {PaymentId}.", id);
                return StatusCode(500, "Internal server error while retrieving payment.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment(PaymentInfos payment)
        {
            try
            {
                if (payment == null)
                {
                    _logger.LogWarning("Payment request is null.");
                    return BadRequest("Payment request is null.");
                }

                _context.PaymentInfos.Add(payment);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Payment created successfully with ID {PaymentId}.", payment.Id);
                return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error while adding payment.");
                return StatusCode(500, "Database update error while adding payment.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a payment.");
                return StatusCode(500, "Internal server error while adding payment.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                var payment = await _context.PaymentInfos.FindAsync(id);

                if (payment == null)
                {
                    _logger.LogWarning("Payment with ID {PaymentId} not found for deletion.", id);
                    return NotFound($"Payment with ID {id} not found.");
                }

                _context.PaymentInfos.Remove(payment);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Payment with ID {PaymentId} deleted successfully.", id);
                return Ok("Payment deleted");
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error while deleting payment.");
                return StatusCode(500, "Database update error while deleting payment.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting payment with ID {PaymentId}.", id);
                return StatusCode(500, "Internal server error while deleting payment.");
            }
        }
    }
}
