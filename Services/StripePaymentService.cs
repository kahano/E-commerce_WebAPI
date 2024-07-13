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


        //public async Task<Payment> ChargeCardsync(long orderId, long CartId, Payment payment)
        //{
        //    StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];


        //    var order = await _unitOfWork._order_repository.GetByIdAsync(orderId);
        //    var cart = await _unitOfWork._cart_repository.GetBasketItemsAsyncByBasketId(CartId);



        //    if (cart is null) return null;

        //    if (order is null) return null;

        //    order.Status = OrderStatus.Failed;
        //    payment.Status = nameof(order.Status);

        //    cart.amount = (long)cart.BasketItems.Sum(i => i.Quantity * (i.Price));
        //    payment.amount = cart.amount;

        //    var options = new ChargeCreateOptions
        //    {
        //        Amount = payment.amount,
        //        Currency = payment.Currency,
        //        Source = payment.source


        //    };

        //    var service = new ChargeService();



        //    Charge charge = await service.CreateAsync(options);


        //    if (charge.Paid)
        //    {
        //        order.Status = OrderStatus.PaymentSucceeded;
        //        payment.basketId = CartId;
        //        return payment;
        //    }
        //    else
        //    {
        //        throw new ArgumentException($"Cart with ID {cart.Id} does not exist.");
        //    }



        //}

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
