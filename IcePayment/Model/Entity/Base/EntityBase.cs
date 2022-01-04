using System.ComponentModel.DataAnnotations;

namespace IcePaymentAPI.Model.Entity.Base
{
    public class EntityBase
    {
        [Key]
        public long Id { get; set; }
    }
}
