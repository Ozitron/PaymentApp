using Microsoft.AspNetCore.Mvc;
using IcePayment.API.Dto;
using IcePayment.API.Data.Repositories;

namespace IcePayment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
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
            return Ok(await _paymentRepository.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentDto paymentDto)
        {
            var id = await _paymentRepository.Create(paymentDto);
            if (id > 0)
                return Ok(id);
            return BadRequest();
        }
    }
}
