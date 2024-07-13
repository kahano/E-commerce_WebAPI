using AutoMapper;
using E_commercial_Web_RESTAPI.DTOS.BasketITem;
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


                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
   
       

            CreateMap<Order, OrderDTO>()

                 .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.UserName))
         
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                 .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                 .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                 .ForMember(dest => dest.total, opt => opt.MapFrom(src => src.total))
                 .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => (src.Status)));



            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProductItemId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));


            CreateMap<CartItem, CartItemRequestDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<CartItem, CartItemDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
               .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
               .ForMember(dest => dest.imageurl, opt => opt.MapFrom(src => src.imageurl));




            //CreateMap<Payment, PaymentRequestDTO>()

            //    .ForMember(k => k.Currency, opt => opt.MapFrom(src => src.Currency))
            //    .ForMember(k => k.source, opt => opt.MapFrom(src => src.source));


            //CreateMap<Payment, PaymentDTO>()
            //     .ForMember(k => k.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(k => k.amount, opt => opt.MapFrom(src => src.amount))
            //    .ForMember(k => k.Currency, opt => opt.MapFrom(src => src.Currency))
            //    .ForMember(k => k.source, opt => opt.MapFrom(src => src.source))
            //    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            //    .ForMember(k => k.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));

        }
    }
}
