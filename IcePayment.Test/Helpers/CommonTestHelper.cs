using IcePayment.API.Dto;

namespace IcePayment.Test.Helpers
{
    internal static class CommonTestHelper
    {
        internal static PaymentDto CreateValidPaymentDto()
        {
            return new PaymentDto()
            {
                Amount = 100.123M,
                CurrencyCode = "USD",
                Order = new OrderDto() { ConsumerAddress = "Amsterdam, Netherlands", ConsumerFullName = "John Doe" }

            };
        }
        internal static string CreateLongString(int stringLength) => new string('*', stringLength);
    }
}
