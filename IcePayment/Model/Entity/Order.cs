using IcePaymentAPI.Model.Entity.Base;
using System.ComponentModel.DataAnnotations;

namespace IcePaymentAPI.Model.Entity
{
    public class Order : EntityBase
    {
        [Required]
        [MaxLength(80)]
        public string ConsumerFullName { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string ConsumerAddress { get; set; } = null!;
    }
}
