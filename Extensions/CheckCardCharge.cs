using E_commercial_Web_RESTAPI.DTOS;
using E_commercial_Web_RESTAPI.Mapper;
using E_commercial_Web_RESTAPI.Models.Payment.Payment;
using Stripe;
using System.Runtime.CompilerServices;

namespace E_commercial_Web_RESTAPI.Extensions
{
    public static class CheckCardCharge
    {
        private static bool IsCardDebited;
        private static readonly List<Currency> currencies = new List<Currency> { Currency.USD, Currency.GBP, Currency.EUR, Currency.NOK };
        public static bool CheckCardCurrency(this PaymentDTO paymentDTO){

           
            
            if (currencies.Contains(PaymentMapper.ConvertToCurrencyEnum(paymentDTO.Currency)))
            {
                 var pay = new Payment
                {
                     Id = paymentDTO.Id,
                    amount = paymentDTO.amount,
                    source = paymentDTO.source,
                    Currency = PaymentMapper.ConvertToCurrencyEnum(paymentDTO.Currency)
              



                };
                if(pay != null)
                {
                    return IsCardDebited = true;
                }
            }

            return false;
        }
    }
}
