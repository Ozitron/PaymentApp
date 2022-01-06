using Microsoft.AspNetCore.Mvc;
using IcePayment.API.Data;
using IcePaymentAPI.Dto;
using IcePaymentAPI.Model.Entity;

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
        public async Task<List<Payment>> GetAllPayments()
        {
            return await _paymentRepository.GetAllPayments();
        }

        [Route("Payment/GetById/{id}")]
        [HttpGet("{id:long}")]
        public async Task<Payment> GetPaymentById(long id)
        {
            return await _paymentRepository.GetPaymentById(id);
        }

        [Route("Payment/Add")]
        [HttpPost]
        public async Task<long> AddPayment(PaymentDto paymentDto)
        {
            return await _paymentRepository.AddPayment(paymentDto);
        }
    }
}
