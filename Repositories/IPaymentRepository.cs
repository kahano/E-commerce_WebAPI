using E_commercial_Web_RESTAPI.DTOS;
using E_commercial_Web_RESTAPI.Models.Payment.Payment;
using System.Numerics;

namespace E_commercial_Web_RESTAPI.Repositories
{
    public interface IPaymentRepository
    {

    
        Task<dynamic> InsertPayment(long customerId, Payment payment);
        Task<Payment?> GetPaymentById(long customerId);
        Task<List<Payment>> GetAllPaymentsByCustomers( );

    }
}
