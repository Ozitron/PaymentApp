using IcePaymentAPI.Dto;
using IcePaymentAPI.Model.Entity;

namespace IcePayment.API.Data
{
    public interface IPaymentRepository
    {
        Task<long> AddPayment(PaymentDto paymentDto);
        Task<Payment> GetPaymentById(long id);
        Task<List<Payment>> GetAllPayments();
    }
}
