using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IcePayment.API.Controllers;
using IcePayment.API.Data.Repositories;
using IcePayment.API.Dto;
using IcePayment.API.Model.Entity;
using IcePayment.API.Validators;
using IcePayment.Test.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IcePayment.Test.UnitTests
{
    public class PaymentControllerTest
    {
        private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
        private readonly PaymentController _paymentController;
        private readonly PaymentValidator _paymentValidator;

        public PaymentControllerTest()
        {
            _paymentRepositoryMock = new Mock<IPaymentRepository>();
            _paymentController = new PaymentController(_paymentRepositoryMock.Object);
            _paymentValidator = new PaymentValidator();
        }

        [Fact]
        public async Task Create_ValidPayment_ReturnsSuccess()
        {
            // arrange
            var payment = CommonTestHelper.CreateValidPaymentDto();
            _paymentRepositoryMock.Setup(p => p.Create(payment)).ReturnsAsync(1);

            // act
            var result = (OkObjectResult)await _paymentController.Post(payment);
            
            // assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void Create_InvalidPayment_ReturnsBadRequest()
        {
            // Arrange
            var currencyMissingPaymentDto = new PaymentCreateDto
            {
                Amount = 1
            };
            _paymentController.ModelState.AddModelError("Currency", "Required");

            // Act
            var response = _paymentController.Post(currencyMissingPaymentDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
        
        [Fact]
        public async void Get_Payments_ReturnsAllPayments()
        {
            //Arrange
            var paymentList = CommonTestHelper.GetSamplePaymentList();
            _paymentRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(paymentList);

            //Act
            var result = (OkObjectResult)await _paymentController.Get();

            //Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);

            //Assert
            var resultList = result.Value as List<Payment>;
            Assert.True(resultList.Count == paymentList.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void Get_PaymentById_ReturnsPaymentWithSelectedId(long id)
        {
            //Arrange
            var payment = CommonTestHelper.GetSamplePaymentList().First(x => x.Id == id);
            _paymentRepositoryMock.Setup(x => x.Get(id)).ReturnsAsync(payment);

            //Act
            var result = (OkObjectResult)await _paymentController.Get(id);

            //Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);

            //Assert
            var selectedPayment = result.Value as Payment;
            Assert.Equal(payment, selectedPayment);
        }


        [Theory]
        [InlineData(123)]
        public async void Get_PaymentByIdWithInvalidPaymentId_ReturnsPaymentNotFound(int id)
        {
            //Arrange
            _paymentRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(CommonTestHelper.GetSamplePaymentList());

            //Act
            var actionResult = await _paymentController.Get(id);

            //Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

    }
}