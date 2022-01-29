using IcePayment.API.Dto;
using IcePayment.API.Helpers;
using IcePayment.API.Mapper;
using IcePayment.API.Model.Entity;

namespace IcePayment.API.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DataContext _context;

        public PaymentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<long> Create(PaymentCreateDto paymentDto)
        {
            try
            {
                var payment = PaymentMapper.MapPaymentCreateDtoToPayment(paymentDto);
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                return payment.Id;
            }
            catch
            {
                throw new AppException();
            }
        }

        public async Task<Payment> Get(long id)
        {
            return await _context.Payments.Include(x => x.Order).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Payment>> GetAll()
        {
            return await _context.Payments.Include(x => x.Order).ToListAsync();
        }
    }
}
