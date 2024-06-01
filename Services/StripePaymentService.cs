using E_commercial_Web_RESTAPI.DTOS;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Mapper;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;
using Stripe;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_commercial_Web_RESTAPI.Services
{
    public class StripePaymentService
    {




        public async Task<ApiResponse> ChargeCardsync(Payment payment)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51PK4cdRvTnksK7lQo03Ma0OB4R4dyHqgtWKR7ZLPC7mMoCnBOvfxtbmbcRFOw6S5a9GJTdgR98l9izmBJeF0iqiU00ZR4GtLcF";


                var options = new ChargeCreateOptions
                {
                    Amount = payment.amount,
                    Currency = payment.Currency.ToString(),
                    Source = payment.source, // considered as a bank credit/debitcard with information of owner

                };

                var service = new ChargeService();

                Charge charge = await service.CreateAsync(options);


                if (charge.Paid)
                {

                    return new ApiResponse
                    {
                        Success = true,
                        Message = $"Payment {charge.Id} is Successful",
                        StatusCode = 200
                    };

                }
                else
                {

                    return new ApiResponse
                    {
                        Success = false,
                        Message = $"Payment {charge.Id} is Unsuccessful",
                        StatusCode = 400
                    };
                }

            }
            catch (StripeException e)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = $"Stripe error for {payment.Currency.ToString()}: {e.Message}",
                    StatusCode = 500
                };
            }


            catch (Exception e)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = e.Message,
                    StatusCode = 400
                };
            }


        }

    }
}
