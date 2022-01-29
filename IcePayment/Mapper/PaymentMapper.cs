using IcePayment.API.Dto;
using IcePayment.API.Model.Common;
using IcePayment.API.Model.Entity;

namespace IcePayment.API.Mapper
{
    public static class PaymentMapper
    { 
        public static Payment MapPaymentCreateDtoToPayment(PaymentCreateDto paymentDto)
        {
            var payment = new Payment
            {
                Amount = paymentDto.Amount,
                CurrencyCode = paymentDto.CurrencyCode,
                Status = PaymentStatus.Created,
                Order = new Order
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
