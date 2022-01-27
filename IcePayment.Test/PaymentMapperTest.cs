using IcePayment.API.Mapper;
using IcePayment.API.Model.Common;
using IcePayment.Test.Helpers;
using Xunit;

namespace IcePayment.Test
{
    public class PaymentMapperTest
    {
        [Fact]
        public void MapPayment_ValidMappingOperation_ReturnsPaymentCreatedStatus()
        {
            //Arrange
            var paymentDto = CommonTestHelper.CreateValidPaymentDto();

            //Act
            var payment = PaymentMapper.MapPayment(paymentDto);

            //Assert
            Assert.Equal(PaymentStatus.Created, payment.Status);
        }
    }
}
