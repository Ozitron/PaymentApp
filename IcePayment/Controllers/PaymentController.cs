using Microsoft.AspNetCore.Mvc;
using IcePaymentAPI.Model.Entity;
using IcePaymentAPI.Mapper;
using IcePaymentAPI.Dto;

namespace IcePaymentAPI.Controllers
{
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly DataContext _context;

        public PaymentController(DataContext context)
        {
            _context = context;
        }

        [Route("~/api/GetAllPayments")]
        [HttpGet]
        public async Task<ActionResult<List<Payment>>> GetAllPayments()
        {
            return Ok(await _context.Payments.Include(x => x.Order).ToListAsync());
        }

        [Route("~/api/GetPaymentById/{id}")]
        [HttpGet("{id:long}")]
        public async Task<ActionResult<Payment>> GetPaymentById(long id)
        {
            var payment = await _context.Payments.Include(x => x.Order).FirstAsync(x => x.Id == id);
            return Ok(payment);
        }

        [Route("~/api/AddPayment")]
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
