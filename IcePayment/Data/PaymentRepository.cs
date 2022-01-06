using IcePaymentAPI.Dto;
using IcePaymentAPI.Mapper;
using IcePaymentAPI.Model.Entity;

namespace IcePayment.API.Data
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DataContext _context;

        public PaymentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<long> AddPayment(PaymentDto paymentDto)
        {
            var payment = PaymentMapper.MapPayment(paymentDto);
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment.Id;
        }

        public async Task<Payment> GetPaymentById(long id)
        {
            return await _context.Payments.Include(x => x.Order).FirstAsync(x => x.Id == id);
        }

        public async Task<List<Payment>> GetAllPayments()
        {
            return await _context.Payments.Include(x => x.Order).ToListAsync();
        }
    }
}
