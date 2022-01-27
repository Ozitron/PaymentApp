using System.ComponentModel.DataAnnotations;
using IcePayment.API.Model.Common;
using IcePayment.API.Model.Entity.Base;

namespace IcePayment.API.Model.Entity
{
    public class Payment : EntityBase
    {
        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(3)]
        public string CurrencyCode { get; set; } 

        [Required]
        public PaymentStatus Status { get; set; }

        [Required]
        public Order Order { get; set; }
    }
}
