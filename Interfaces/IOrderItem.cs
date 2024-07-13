using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;

namespace E_commercial_Web_RESTAPI.Interfaces
{
    public interface IOrderItem: IRepository<OrderItem>
    {
        OrderItem? GetOrderItemById(long Id);
        Task<Order?> GetByPaymentIdAsync(string paymentId);
    }
}
