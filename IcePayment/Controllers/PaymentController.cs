using Microsoft.AspNetCore.Mvc;
using IcePayment.API.Dto;
using IcePayment.API.Data.Repositories;

namespace IcePayment.API.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _paymentRepository.GetAll());
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid payment id.");
            }

            var payment = await _paymentRepository.Get(id);
            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentCreateDto paymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentId = await _paymentRepository.Create(paymentDto);
            return Ok(new { message = $"Payment {paymentId} successfully created" });
        }
    }
}
