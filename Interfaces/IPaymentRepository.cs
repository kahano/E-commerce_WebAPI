using E_commercial_Web_RESTAPI.DTOS;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Models;
using System.Numerics;

namespace E_commercial_Web_RESTAPI.Repositories
{
    public interface IPaymentRepository
    {


        Task<ApiResponse> CheckOutPayment(long orderId, long CartId, Payment payment);
        Task<Payment?> GetPaymentById(long paymentId);
        //Task<List<Payment>> GetAllPaymentsByCustomer(PaymentQueryObject query);
   

    }
}
