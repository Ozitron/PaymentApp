using System;
using System.Threading.Tasks;
using IcePaymentAPI.Controllers;
using IcePaymentAPI.Data;
using IcePaymentAPI.Model.Common;
using IcePaymentAPI.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Xunit;

namespace IcePayment.Test
{
    public class PaymentControllerTest
    {
        private DataContext _context;
        private PaymentController _paymentController;

        [Fact]
        public async Task GetPaymentById()
        {
            Initialize();
            var result = await _paymentController.GetPaymentById(1);
            Assert.True(result != null);
        }


        [Fact]
        public async Task GetAllPayments_WhenCalled_ReturnsOkResult()
        {
            Initialize();
            var payments = await _paymentController.GetAllPayments();
            Assert.True(payments != null);
        }

        private void Initialize()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("PaymentDb")
                .Options;

            _context = new DataContext(options);
            _paymentController = new PaymentController(_context);
            MockDbContext();
        }

        private void MockDbContext()
        {
            _context.Payments.Add(new Payment()
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
            });

            _context.Payments.Add(new Payment()
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
            });
        }
    }
}