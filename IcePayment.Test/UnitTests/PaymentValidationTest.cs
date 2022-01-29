using IcePayment.API.Validators;
using IcePayment.Test.Helpers;
using Xunit;

namespace IcePayment.Test.UnitTests
{
    public class PaymentValidationTest
    {
        private readonly PaymentValidator _paymentValidator;

        public PaymentValidationTest()
        {
            _paymentValidator = new PaymentValidator();
        }

        [Fact]
        public void Payment_InvalidAmount_ReturnsValidationError()
        {
            // Arrange
            var payment = CommonTestHelper.CreateValidPaymentDto();
            payment.Amount *= -1;

            // Act
            var result = _paymentValidator.Validate(payment);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Payment_NullCurrency_ReturnsValidationError()
        {
            // Arrange
            var payment = CommonTestHelper.CreateValidPaymentDto();
            payment.CurrencyCode = null;

            // Act
            var result = _paymentValidator.Validate(payment);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Payment_InvalidCurrency_ReturnsValidationError()
        {
            // Arrange
            var payment = CommonTestHelper.CreateValidPaymentDto();
            payment.CurrencyCode = "invalid";

            // Act
            var result = _paymentValidator.Validate(payment);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Payment_NullOrderConsumerFullName_ReturnsValidationError()
        {
            //Arrange
            var payment = CommonTestHelper.CreateValidPaymentDto();
            payment.Order.ConsumerFullName = null;

            // Act
            var result = _paymentValidator.Validate(payment);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Payment_EmptyOrderConsumerFullName_ReturnsValidationError()
        {
            //Arrange
            var payment = CommonTestHelper.CreateValidPaymentDto();
            payment.Order.ConsumerFullName = string.Empty;

            // Act
            var result = _paymentValidator.Validate(payment);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Payment_ShortOrderConsumerFullName_ReturnsValidationError()
        {
            //Arrange
            var payment = CommonTestHelper.CreateValidPaymentDto();
            payment.Order.ConsumerFullName = "t";

            // Act
            var result = _paymentValidator.Validate(payment);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Payment_LongOrderConsumerFullName_ReturnsValidationError()
        {
            //Arrange
            var payment = CommonTestHelper.CreateValidPaymentDto();
            payment.Order.ConsumerFullName = CommonTestHelper.CreateLongString(81);

            // Act
            var result = _paymentValidator.Validate(payment);

            // Assert
            Assert.False(result.IsValid);
        }
        
        [Fact]
        public void Payment_NullOrderConsumerAddress_ReturnsValidationError()
        {
            //Arrange
            var payment = CommonTestHelper.CreateValidPaymentDto();
            payment.Order.ConsumerAddress = null;

            // Act
            var result = _paymentValidator.Validate(payment);

            // Assert
            Assert.False(result.IsValid);
        }
        
        [Fact]
        public void Payment_EmptyOrderConsumerAddress_ReturnsValidationError()
        {
            //Arrange
            var payment = CommonTestHelper.CreateValidPaymentDto();
            payment.Order.ConsumerAddress = string.Empty;

            // Act
            var result = _paymentValidator.Validate(payment);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Payment_ShortOrderConsumerAddress_ReturnsValidationError()
        {
            //Arrange
            var payment = CommonTestHelper.CreateValidPaymentDto();
            payment.Order.ConsumerAddress = "t";

            // Act
            var result = _paymentValidator.Validate(payment);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Payment_LongOrderConsumerAddress_ReturnsValidationError()
        {
            //Arrange
            var payment = CommonTestHelper.CreateValidPaymentDto();
            payment.Order.ConsumerAddress = CommonTestHelper.CreateLongString(251);

            // Act
            var result = _paymentValidator.Validate(payment);

            // Assert
            Assert.False(result.IsValid);
        }

    }
}
