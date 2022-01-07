using Microsoft.AspNetCore.Mvc;
using IcePaymentAPI.Dto;

namespace IcePaymentAPI.Controllers
{
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [Route("Payment/GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            return Ok(await _paymentRepository.GetAllPayments());
        }

        [Route("Payment/GetById/{id}")]
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetPaymentById(long id)
        {
            return Ok(await _paymentRepository.GetPaymentById(id));
        }

        [Route("Payment/Add")]
        [HttpPost]
        public async Task<IActionResult> AddPayment(PaymentDto paymentDto)
        {
            var id = await _paymentRepository.AddPayment(paymentDto);
            if (id > 0)
                return Ok(id);
            return BadRequest();
        }
    }
}
