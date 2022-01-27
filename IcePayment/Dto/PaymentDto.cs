using System.ComponentModel.DataAnnotations;
using IcePayment.API.Helper;

namespace IcePayment.API.Dto
{
    public class PaymentDto
    {
        [CustomValidation(typeof(ValidationMethods), "ValidatePositive")]
        public decimal Amount { get; set; }

        [StringLength(3, MinimumLength = 3, ErrorMessage = "CurrencyCode must be 3 characters long.")]
        public string CurrencyCode { get; set; }
        
        public OrderDto Order { get; set; }
    }
}
