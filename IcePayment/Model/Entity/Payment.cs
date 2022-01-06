using System.ComponentModel.DataAnnotations;
using IcePaymentAPI.Model.Entity.Base;
using IcePaymentAPI.Model.Common;

namespace IcePaymentAPI.Model.Entity
{
    public class Payment : EntityBase
    {
        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(3)]
        public string CurrencyCode { get; set; } 

        [Required]
        public PaymentStatus Status { get; set; }

        [Required]
        public Order Order { get; set; }
    }
}
