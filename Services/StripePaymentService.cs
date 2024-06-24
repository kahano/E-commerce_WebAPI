using E_commercial_Web_RESTAPI.DTOS;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Mapper;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;
using Stripe;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_commercial_Web_RESTAPI.Services
{
    public class StripePaymentService : IStripePaymentService
    {
  
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public StripePaymentService( IUnitOfWork unitOfWork, IConfiguration configuration)
        {
        
            _unitOfWork = unitOfWork;
            _configuration = configuration;

        }


        public async Task<Payment> ChargeCardsync(long CartId, Payment payment)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            

           var cart = await _unitOfWork._cart_repository.GetBasketItemsAsyncByBasketId(CartId);

            try
            {

                if (cart is null) return null;

                

                cart.amount = (long)cart.BasketItems.Sum(i => i.Quantity * (i.Price));
                payment.amount = cart.amount;

                var options = new ChargeCreateOptions
                {
                    Amount = payment.amount,
                    Currency = payment.Currency,
                    Source = payment.source


                };

                var service = new ChargeService();



                Charge charge = await service.CreateAsync(options);


                if (charge.Paid)
                {
                 
                    payment.basketId = CartId;
                    return payment;
                }
                else
                {
                    throw new ArgumentException($"Cart with ID {cart.Id} does not exist.");
                }


            }
            catch (Exception e)
            {
                throw e;
            }



        }

        
    }
}
