using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using IcePayment.API.Dto;
using IcePayment.API.Model.Common;
using IcePayment.API.Model.Entity;
using Newtonsoft.Json;

namespace IcePayment.Test.Helpers
{
    internal static class CommonTestHelper
    {
        internal static PaymentCreateDto CreateValidPaymentDto()
        {
            return new PaymentCreateDto()
            {
                Amount = 100.123M,
                CurrencyCode = "USD",
                Order = new OrderDto() { ConsumerAddress = "Amsterdam, Netherlands", ConsumerFullName = "John Doe" }

            };
        }

        internal static List<Payment> GetSamplePaymentList()
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

        internal static string CreateLongString(int stringLength) => new string('*', stringLength);

        internal static StringContent DefaultContent(dynamic request) => new StringContent(JsonConvert.SerializeObject(request),
            Encoding.UTF8, "application/json");
    }
}
