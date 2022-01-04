using IcePaymentAPI.Model.Common;
using System.ComponentModel.DataAnnotations;

namespace IcePayment.Dtos
{
    public class PaymentDto
    {
        public DateTime CreationDate { get; set; }

        [Range(0.0, (double)decimal.MaxValue, ErrorMessage = "The amount field {0} must be greater than {1}.")]
        public decimal Amount { get; set; }

        [MaxLength(3)]
        public string CurrencyCode { get; set; }

        public PaymentStatus Status { get; set; }

        public OrderDto Order { get; set; }
    }
}
