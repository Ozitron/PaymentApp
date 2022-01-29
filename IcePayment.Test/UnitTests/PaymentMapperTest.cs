using IcePayment.API.Dto;
using IcePayment.API.Mapper;
using IcePayment.API.Model.Common;
using IcePayment.API.Model.Entity;
using IcePayment.Test.Helpers;
using Xunit;

namespace IcePayment.Test.UnitTests
{
    public class PaymentMapperTest
    {
        [Fact]
        public void MapPaymentCreateDtoToPayment_GetsPaymentDto_ReturnsPayment()
        {
            //Arrange
            var paymentCreateDto = CommonTestHelper.CreateValidPaymentDto();

            //Act
            var payment = PaymentMapper.MapPaymentCreateDtoToPayment(paymentCreateDto);

            //Assert
            Assert.Equal(typeof(PaymentCreateDto), paymentCreateDto.GetType());
            Assert.Equal(typeof(Payment), payment.GetType());
        }

        [Fact]
        public void MapPaymentCreateDtoToPayment_ValidMappingOperation_ReturnsPaymentCreatedStatus()
        {
            //Arrange
            var paymentCreateDto = CommonTestHelper.CreateValidPaymentDto();

            //Act
            var payment = PaymentMapper.MapPaymentCreateDtoToPayment(paymentCreateDto);

            //Assert
            Assert.Equal(PaymentStatus.Created, payment.Status);
        }
    }
}
