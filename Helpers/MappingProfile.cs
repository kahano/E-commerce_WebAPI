using AutoMapper;
using E_commercial_Web_RESTAPI.DTOS.Order;
using E_commercial_Web_RESTAPI.DTOS.OrderItem;
using E_commercial_Web_RESTAPI.DTOS.Payments;
using E_commercial_Web_RESTAPI.Models;

namespace E_commercial_Web_RESTAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderRequestDTO>()


                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
       

            CreateMap<Order, OrderDTO>()

                 .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.customerId, opt => opt.MapFrom(src => src.CustomerId))
                 .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                 .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                 .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                 .ForMember(dest => dest.total, opt => opt.MapFrom(src => src.total));


            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProductItemId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));


            CreateMap<Payment, PaymentRequestDTO>()

                .ForMember(k => k.Currency, opt => opt.MapFrom(src => src.Currency))
                .ForMember(k => k.source, opt => opt.MapFrom(src => src.source));
            

            CreateMap<Payment, PaymentDTO>()
                .ForMember(k => k.amount, opt => opt.MapFrom(src => src.amount))
                .ForMember(k => k.Currency, opt => opt.MapFrom(src => src.Currency))
                .ForMember(k => k.source, opt => opt.MapFrom(src => src.source))
                .ForMember(k => k.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(k => k.customerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(k => k.CreatedBy, opt => opt.MapFrom(src => src.customer.Name));
        }
    }
}
