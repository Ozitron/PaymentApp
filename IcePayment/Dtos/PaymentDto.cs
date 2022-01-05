using IcePaymentAPI.Model.Common;
using System.ComponentModel.DataAnnotations;

namespace IcePaymentAPI.Dtos
{
    public class PaymentDto
    {
        [Range(0.0000001, (double)decimal.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public decimal Amount { get; set; }

        [StringLength(3, MinimumLength = 3, ErrorMessage = "CurrencyCode must be 3 characters long.")]
        public string CurrencyCode { get; set; }

        [Range(0, 2, ErrorMessage = "Status must be between 0 and 2.")]
        public PaymentStatus Status { get; set; }

        public OrderDto Order { get; set; }
    }
}
