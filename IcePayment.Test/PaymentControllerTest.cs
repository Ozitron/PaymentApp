using System;
using System.Collections.Generic;
using System.Linq;
using IcePayment.API.Data;
using IcePaymentAPI.Controllers;
using IcePaymentAPI.Dto;
using IcePaymentAPI.Model.Common;
using IcePaymentAPI.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IcePayment.Test
{
    public class PaymentControllerTest
    {
        private readonly Mock<IPaymentRepository> _paymentRepository;
        private readonly PaymentController _paymentController;

        public PaymentControllerTest()
        {
            _paymentRepository = new Mock<IPaymentRepository>();
            _paymentController = new PaymentController(_paymentRepository.Object);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetPaymentById_ValidPayment_ReturnSinglePayment(long Id)
        {
            //Arrange
            var payment = GetSamplePaymentList().First(x => x.Id == Id);
            _paymentRepository.Setup(x => x.GetPaymentById(Id)).ReturnsAsync(payment);

            //Act
            var result = await _paymentController.GetPaymentById(Id);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<OkObjectResult>(okResult);

            //Assert
            var selectedPayment = okResult.Value as Payment;
            Assert.Equal(payment, selectedPayment);
        }

        [Theory]
        [InlineData(123)]
        public async void GetPaymentById_InvalidPayment_PaymentNotFound(int Id)
        {
            //Arrange
            _paymentRepository.Setup(x => x.GetAllPayments()).ReturnsAsync(GetSamplePaymentList());

            //Act
            IActionResult actionResult = await _paymentController.GetPaymentById(Id);

            //Assert
            var okResult = actionResult as OkObjectResult;
            Assert.IsType<OkObjectResult>(okResult);

            //Assert
            var payment = okResult.Value as Payment;
            Assert.Null(payment);
        }

        [Fact]
        public async void GetAllPayments_ValidState_ReturnAllPayments()
        {
            //Arrange
            var paymentList = GetSamplePaymentList();
            _paymentRepository.Setup(x => x.GetAllPayments()).ReturnsAsync(paymentList);

            //Act
            IActionResult actionResult = await _paymentController.GetAllPayments();

            //Assert
            var okResult = actionResult as OkObjectResult;
            Assert.IsType<OkObjectResult>(okResult);

            //Assert
            var resultList = okResult.Value as List<Payment>;
            Assert.True(resultList.Count == paymentList.Count);
        }

        [Fact]
        public async void AddPayment_ValidPaymentDto_ReturnSuccess()
        {
            //Arrange
            var paymentDto = CreateValidPaymentDto();
            _paymentRepository.Setup(x => x.AddPayment(paymentDto)).ReturnsAsync(1);

            //Act
            var actionResult = await _paymentController.AddPayment(paymentDto);

            //Assert
            var okResult = actionResult as OkObjectResult;
            Assert.IsType<OkObjectResult>(okResult);

            //Assert
            var paymentId = (long)okResult?.Value;
            Assert.Equal(paymentId, 1);
        }

        [Fact]
        public async void AddPayment_InvalidPaymentAmount_ReturnsBadRequest()
        {
            //Arrange
            var paymentDto = CreateValidPaymentDto();
            paymentDto.Amount *= -1;

            //Act
            var actionResult = await _paymentController.AddPayment(paymentDto);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddPayment_InvalidPaymentCurrencyCode_ReturnsBadRequest()
        {
            //Arrange
            var paymentDto = CreateValidPaymentDto();
            paymentDto.CurrencyCode += "X";

            //Act
            var actionResult = await _paymentController.AddPayment(paymentDto);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(5)]
        public async void AddPayment_InvalidPaymentStatus_ReturnsBadRequest(int status)
        {
            //Arrange
            var paymentDto = CreateValidPaymentDto();
            paymentDto.Status = (PaymentStatus)status;

            //Act
            var actionResult = await _paymentController.AddPayment(paymentDto);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddPayment_InvalidOrderConsumerFullNameTooShort_ReturnsBadRequest()
        {
            //Arrange
            var paymentDto = CreateValidPaymentDto();
            paymentDto.Order.ConsumerFullName = string.Empty;

            //Act
            var actionResult = await _paymentController.AddPayment(paymentDto);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddPayment_InvalidOrderConsumerFullNameTooLong_ReturnsBadRequest()
        {
            //Arrange
            var paymentDto = CreateValidPaymentDto();
            paymentDto.Order.ConsumerFullName = CreateLongString(81);

            //Act
            var actionResult = await _paymentController.AddPayment(paymentDto);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddPayment_InvalidOrderConsumerConsumerAddressTooShort_ReturnsBadRequest()
        {
            //Arrange
            var paymentDto = CreateValidPaymentDto();
            paymentDto.Order.ConsumerAddress = string.Empty;

            //Act
            var actionResult = await _paymentController.AddPayment(paymentDto);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddPayment_InvalidOrderConsumerConsumerAddressTooLong_ReturnsBadRequest()
        {
            //Arrange
            var paymentDto = CreateValidPaymentDto();
            paymentDto.Order.ConsumerAddress = CreateLongString(201);

            //Act
            var actionResult = await _paymentController.AddPayment(paymentDto);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        private static List<Payment> GetSamplePaymentList()
        {
            var payments = new List<Payment>()
            {
                new()
                {
                    Id = 1,
                    Amount = 123.45M,
                    CreationDate = DateTime.Now,
                    CurrencyCode = "USD",
                    Status = PaymentStatus.Created,
                    Order = new Order()
                    {
                        Id = 1,
                        ConsumerFullName = "John Doe",
                        ConsumerAddress = "London"
                    }
                },
                new()
                {
                    Id = 2,
                    Amount = 67.89M,
                    CreationDate = DateTime.Now,
                    CurrencyCode = "EUR",
                    Status = PaymentStatus.Created,
                    Order = new Order()
                    {
                        Id = 2,
                        ConsumerFullName = "Jane Doe",
                        ConsumerAddress = "Amsterdam"
                    }
                }
            };
            return payments;
        }

        private static PaymentDto CreateValidPaymentDto()
        {
            return new PaymentDto()
            {
                Amount = 100.123M,
                CurrencyCode = "USD",
                Order = new OrderDto() { ConsumerAddress = "Beijing, China", ConsumerFullName = "Wei Long" },
                Status = PaymentStatus.Created
            };
        }

        private static string CreateLongString(int stringLength) => new string('*', stringLength);
    }
}