using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IcePayment.API.Model.Entity;
using IcePayment.Test.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace IcePayment.Test.IntegrationTests
{
    public class PaymentControllerIntegrationTest : WebApplicationFactory<Program>
    {
        private const string Route = "/api/payments";
        private readonly HttpClient _client;

        public PaymentControllerIntegrationTest()
        {
            _client = CreateClient();
        }

        [Fact]
        public async Task Create_WithValidPayment_ReturnsOK()
        {
            // Arrange
            var request = CommonTestHelper.CreateValidPaymentDto();
            var content = CommonTestHelper.DefaultContent(request);

            // Act
            var response = await _client.PostAsync(Route, content);
            var actualStatusCode = response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.OK, actualStatusCode);
        }

        [Fact]
        public async Task Create_WhenInsertFailsWithInvalidPayment_ReturnsBadRequest()
        {
            // Arrange
            var request = CommonTestHelper.CreateValidPaymentDto();
            request.Amount = -1;
            var content = CommonTestHelper.DefaultContent(request);

            // Act
            var response = await _client.PostAsync(Route, content);
            var actualStatusCode = response.StatusCode;

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, actualStatusCode);
        }

        [Fact]
        public async Task Get_WhenGetSucceeds_ReturnsOKWithInsertedPayment()
        {
            // Arrange
            var request = CommonTestHelper.CreateValidPaymentDto();
            var content = CommonTestHelper.DefaultContent(request);
            await _client.PostAsync(Route, content);

            // Act
            var responseGet = await _client.GetAsync(Route);
            var actualGetResult = await responseGet.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Payment[]>(actualGetResult);
            var insertedPaymentExist = result[0].Order.ConsumerFullName == request.Order.ConsumerFullName;

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseGet.StatusCode);
            Assert.NotNull(result);
            Assert.True(insertedPaymentExist);
        }

        [Fact]
        public async Task GetAll_WhenSucceeds_ReturnsAllPayments()
        {
            // Arrange
            var request = CommonTestHelper.CreateValidPaymentDto();
            var content = CommonTestHelper.DefaultContent(request);
            await _client.PostAsync(Route, content);
            request.Amount += 100;
            content = CommonTestHelper.DefaultContent(request);
            await _client.PostAsync(Route, content);

            // Act
            var responseGet = await _client.GetAsync(Route);
            responseGet.EnsureSuccessStatusCode();
            var actualGetResult = await responseGet.Content.ReadAsStringAsync();
            var resultList = JsonConvert.DeserializeObject<Payment[]>(actualGetResult);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responseGet.StatusCode);
            Assert.True(resultList.Length > 0);
        }
    }
}
