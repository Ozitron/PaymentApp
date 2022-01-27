using System.ComponentModel.DataAnnotations;
using IcePayment.API.Model.Entity.Base;

namespace IcePayment.API.Model.Entity
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
