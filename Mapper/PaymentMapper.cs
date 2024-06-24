using E_commercial_Web_RESTAPI.DTOS.Payments;
using E_commercial_Web_RESTAPI.Mapper.CustomerMappper;
using E_commercial_Web_RESTAPI.Models;
using Stripe;

namespace E_commercial_Web_RESTAPI.Mapper
{
    public static class PaymentMapper
    {

        //    public static Currency ConvertToCurrencyEnum(string currency)
        //    {
        //        if (Enum.TryParse(typeof(Currency), currency, out var result))
        //        {
        //            return (Currency)result;
        //        }
        //        else
        //        {
        //            throw new ArgumentException("Invalid payment method value", nameof(currency));
        //        }
        //    }
        public static PaymentDTO ToPaymentDTO(this Payment payment)
        {
            //if (payment is null)
            //{
            //    throw new ArgumentException($"{nameof(payment)} is null");
            //}

            return new PaymentDTO
            {
                //Id = payment.Id,
                //amount = payment.amount,
                //source = payment.source,
                //Currency = payment.Currency.ToUpper(),
                //CreatedDate = payment.CreatedDate,
                customerId = payment.CustomerId,
                CreatedBy = payment.customer.ToCustomerDTO().Name





            };
        }

        //public static Payment ToPayment(this PaymentDTO payment)
        //{
        //    if (payment is null)
        //    {
        //        throw new ArgumentException($"{nameof(payment)} is null");
        //    }

        //    return new Payment
        //    {

        //        amount = payment.amount,
        //        source = payment.source,
        //        Currency = payment.Currency.ToUpper()

        //    };
        //}

        public static Payment ToPaymentFromRequestDTO(this PaymentRequestDTO paymentdto)
        {
            if (paymentdto is null)
            {
                throw new ArgumentException($"{nameof(paymentdto)} is null");
            }

            return new Payment
            {
                Currency = paymentdto.Currency.ToUpper(),

                //amount = paymentdto.CartDTO.amount,

                source = paymentdto.source



            };
        }
    }
}
