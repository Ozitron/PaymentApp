using IcePayment.Data;
using IcePaymentAPI.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using IcePayment.Dtos;

namespace IcePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PaymentController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Payment>>> GetAll()
        {
            return Ok(await _context.Payments.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetById(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return BadRequest("Payment not found.");
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult<List<Payment>>> AddPayment(PaymentDto paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok(await _context.Payments.ToListAsync());
        }
    }
}
