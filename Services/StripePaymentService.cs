using E_commercial_Web_RESTAPI.DTOS;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;
using Stripe;
using Stripe.Issuing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_commercial_Web_RESTAPI.Services
{
    public class StripePaymentService : IStripePaymentService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public StripePaymentService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {

            _unitOfWork = unitOfWork;
            _configuration = configuration;

        }


        public async Task<Cart> CreateOrUpdatePayment(string UserId)
        {

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            Cart? cart = _unitOfWork._cart_repository.GetCart(UserId);


            if (cart is null) return null;

            cart.amount = (long)cart.Items.Sum(i => i.Quantity * (i.Price));

            var service = new PaymentIntentService();

            PaymentIntent intent;

            if (string.IsNullOrEmpty(cart.PaymentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = cart.amount,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                };

                intent = await service.CreateAsync(options);
                cart.PaymentId = intent.Id;
                cart.SessionId = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = cart.amount

                };
                await service.UpdateAsync(cart.PaymentId, options);
            }
            _unitOfWork._cart_repository.Update(cart);
           await _unitOfWork.CommitChanges();
            return cart;
                
        }
    }
}
