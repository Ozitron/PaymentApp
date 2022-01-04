using AutoMapper;
using IcePayment.Dtos;
using IcePaymentAPI.Model.Entity;

namespace IcePayment.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<PaymentDto, Payment>();
            CreateMap<OrderDto, Order>();
        }
    }
}
