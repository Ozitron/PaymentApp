using System.ComponentModel.DataAnnotations;

namespace IcePaymentAPI.Dto
{
    public class OrderDto
    {
        [StringLength(80, MinimumLength = 3, ErrorMessage = "ConsumerFullName must be between 3 and 80 characters.")]
        public string ConsumerFullName { get; set; }

        [StringLength(200, MinimumLength = 5, ErrorMessage = "ConsumerAddress must be between 5 and 200 characters.")]
        public string ConsumerAddress { get; set; }
    }
}
