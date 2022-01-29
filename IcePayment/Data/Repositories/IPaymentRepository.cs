using IcePayment.API.Dto;
using IcePayment.API.Model.Entity;

namespace IcePayment.API.Data.Repositories
{
    public interface IPaymentRepository
    {
        Task<long> Create(PaymentCreateDto paymentDto);
        Task<Payment> Get(long id);
        Task<List<Payment>> GetAll();
    }
}
