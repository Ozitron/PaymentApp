using System;
using System.Collections.Generic;
using System.Linq;
using IcePayment.API.Controllers;
using IcePayment.API.Data.Repositories;
using IcePayment.API.Model.Common;
using IcePayment.API.Model.Entity;
using IcePayment.Test.Helpers;
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
            _paymentRepository.Setup(x => x.Get(Id)).ReturnsAsync(payment);

            //Act
            var result = await _paymentController.Get(Id);

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
            _paymentRepository.Setup(x => x.GetAll()).ReturnsAsync(GetSamplePaymentList());

            //Act
            IActionResult actionResult = await _paymentController.Get(Id);

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
            _paymentRepository.Setup(x => x.GetAll()).ReturnsAsync(paymentList);

            //Act
            IActionResult actionResult = await _paymentController.Get();

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
            var paymentDto = CommonTestHelper.CreateValidPaymentDto();
            _paymentRepository.Setup(x => x.Create(paymentDto)).ReturnsAsync(1);

            //Act
            var actionResult = await _paymentController.Create(paymentDto);

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
            var paymentDto = CommonTestHelper.CreateValidPaymentDto();
            paymentDto.Amount *= -1;

            //Act
            var actionResult = await _paymentController.Create(paymentDto);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddPayment_InvalidPaymentCurrencyCode_ReturnsBadRequest()
        {
            //Arrange
            var paymentDto = CommonTestHelper.CreateValidPaymentDto();
            paymentDto.CurrencyCode += "X";

            //Act
            var actionResult = await _paymentController.Create(paymentDto);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddPayment_IsValidPaymentStatus_ReturnsSuccess()
        {
            //Arrange
            var paymentDto = CommonTestHelper.CreateValidPaymentDto();

            //Act
            var actionResult = await _paymentController.Create(paymentDto);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddPayment_InvalidOrderConsumerFullNameTooShort_ReturnsBadRequest()
        {
            //Arrange
            var paymentDto = CommonTestHelper.CreateValidPaymentDto();
            paymentDto.Order.ConsumerFullName = string.Empty;

            //Act
            var actionResult = await _paymentController.Create(paymentDto);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void Create_InvalidOrderConsumerFullNameTooLong_ReturnsBadRequest()
        {
            //Arrange
            var paymentDto = CommonTestHelper.CreateValidPaymentDto();
            paymentDto.Order.ConsumerFullName = CommonTestHelper.CreateLongString(81);

            //Act
            var actionResult = await _paymentController.Create(paymentDto);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddPayment_InvalidOrderConsumerConsumerAddressTooShort_ReturnsBadRequest()
        {
            //Arrange
            var paymentDto = CommonTestHelper.CreateValidPaymentDto();
            paymentDto.Order.ConsumerAddress = string.Empty;

            //Act
            var actionResult = await _paymentController.Create(paymentDto);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async void AddPayment_InvalidOrderConsumerConsumerAddressTooLong_ReturnsBadRequest()
        {
            //Arrange
            var paymentDto = CommonTestHelper.CreateValidPaymentDto();
            paymentDto.Order.ConsumerAddress = CommonTestHelper.CreateLongString(201);

            //Act
            var actionResult = await _paymentController.Create(paymentDto);

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
    }
}