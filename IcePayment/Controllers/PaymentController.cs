using Microsoft.AspNetCore.Mvc;
using IcePaymentAPI.Model.Entity;
using IcePaymentAPI.Mapper;
using IcePaymentAPI.Dtos;

namespace IcePaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly DataContext _context;

        public PaymentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Payment>>> GetAllPayments()
        {
            return Ok(await _context.Payments.Include(x => x.Order).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPaymentById(long id)
        {
            var payment = await _context.Payments.Include(x => x.Order).FirstAsync(x => x.Id == id);
            if (payment == null)
                return BadRequest("Payment not found.");
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult<List<Payment>>> AddPayment(PaymentDto paymentDto)
        {
            var payment = PaymentMapper.MapPayment(paymentDto);
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok(await _context.Payments.Include(x => x.Order).FirstAsync(x => x.Id == payment.Id));
        }
    }
}
