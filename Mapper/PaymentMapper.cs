using E_commercial_Web_RESTAPI.DTOS;
using E_commercial_Web_RESTAPI.Models.Payment.Payment;
using Stripe;

namespace E_commercial_Web_RESTAPI.Mapper
{
    public static class PaymentMapper
    {

        public static Currency ConvertToCurrencyEnum(string currency)
        {
            if (Enum.TryParse(typeof(Currency), currency, out var result))
            {
                return (Currency)result;
            }
            else
            {
                throw new ArgumentException("Invalid payment method value", nameof(currency));
            }
        }
        public static PaymentDTO ToPaymentDTO(this Payment payment)
        {
            return new PaymentDTO
            {
                Id = payment.Id,
                amount = payment.amount,
                source = payment.source,
                Currency = payment.Currency.ToString()


            };
        }

        public static Payment ToPayment(this PaymentDTO payment)
        {
            return new Payment
            {
           
                amount = payment.amount,
                source = payment.source,
                Currency = ConvertToCurrencyEnum(payment.Currency)

            };
        }

        public static Payment ToPaymentFromRequestDTO(this PaymentRequestDTO paymentdto)
        {
            return new Payment
            {
                Currency = ConvertToCurrencyEnum(paymentdto.Currency),

                amount = paymentdto.amount,
                
                  source = paymentdto.source,
              
              

            };
        }
    }
}
