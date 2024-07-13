using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Models;

namespace E_commercial_Web_RESTAPI.Interfaces
{
    public interface IStripePaymentService
    {
        Task<Cart> CreateOrUpdatePayment(string UserId);
        //Task<Order> UpdateOrderPaymentSucceeded(long paymentId);
        //Task<Order> UpdateOrderPaymentFailed(long paymentId);
        //Task<Payment> ChargeCardsync(long orderId, long CartId, Payment payment);
    }
}
