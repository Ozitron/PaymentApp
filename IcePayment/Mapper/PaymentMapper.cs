using IcePaymentAPI.Dto;
using IcePaymentAPI.Model.Entity;

namespace IcePaymentAPI.Mapper
{
    public static class PaymentMapper
    {
        public static Payment MapPayment(PaymentDto paymentDto)
        {
            var payment = new Payment()
            {
                Amount = paymentDto.Amount,
                CurrencyCode = paymentDto.CurrencyCode,
                Status = paymentDto.Status,
                Order = new Order()
                {
                    ConsumerAddress = paymentDto.Order.ConsumerAddress,
                    ConsumerFullName = paymentDto.Order.ConsumerFullName
                },
                CreationDate = DateTime.Now
            };

            return payment;
        }
    }
}
