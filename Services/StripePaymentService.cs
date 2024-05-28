using E_commercial_Web_RESTAPI.DTOS;
using E_commercial_Web_RESTAPI.Models.Payment.Payment;
using E_commercial_Web_RESTAPI.Repositories;
using Stripe;
using System.ComponentModel.DataAnnotations;

namespace E_commercial_Web_RESTAPI.Services
{
    public class StripePaymentService
    {
 

        public StripePaymentService()
        {
            
        }


        public async Task<dynamic> ChargeCardsync(Payment payment)
        {
            try
            {
                StripeConfiguration.ApiKey = "sk_test_51PK4cdRvTnksK7lQo03Ma0OB4R4dyHqgtWKR7ZLPC7mMoCnBOvfxtbmbcRFOw6S5a9GJTdgR98l9izmBJeF0iqiU00ZR4GtLcF";
                var options = new ChargeCreateOptions
                {
                    Amount = payment.amount,
                    Currency = payment.Currency.ToString().ToLower(),
                    Source = payment.source,

                };

                var service = new ChargeService();
                Charge charge = await service.CreateAsync(options);

                if (charge.Paid)
                {
                    return "Payment is succeeded";

                }
                else
                {
                    return "Payment is Failed";
                }

            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        
    }
}
