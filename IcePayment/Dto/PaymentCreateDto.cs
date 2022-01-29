namespace IcePayment.API.Dto
{
    public class PaymentCreateDto
    {
        public decimal Amount { get; set; }
        
        public string CurrencyCode { get; set; }
        
        public OrderDto Order { get; set; }
    }
}
