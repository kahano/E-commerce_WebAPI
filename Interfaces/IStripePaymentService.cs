using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Models;

namespace E_commercial_Web_RESTAPI.Interfaces
{
    public interface IStripePaymentService
    {
       // Task<Cart> CreateOrUpdatePayment(long basketId);
        //Task<Order> UpdateOrderPaymentSucceeded(long paymentId);
        //Task<Order> UpdateOrderPaymentFailed(long paymentId);
        Task<Payment> ChargeCardsync(long CartId, Payment payment);
    }
}
