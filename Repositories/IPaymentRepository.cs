using E_commercial_Web_RESTAPI.DTOS;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Models;
using System.Numerics;

namespace E_commercial_Web_RESTAPI.Repositories
{
    public interface IPaymentRepository
    {


        Task<ApiResponse> InsertPayment(long customerId, Payment payment);
        Task<Payment?> GetPaymentById(long Id);
        Task<List<Payment>> GetAllPaymentsByCustomer(PaymentQueryObject query);

    }
}
