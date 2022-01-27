using System.ComponentModel.DataAnnotations;

namespace IcePayment.API.Model.Entity.Base
{
    public class EntityBase
    {
        [Key]
        public long Id { get; set; }
    }
}
