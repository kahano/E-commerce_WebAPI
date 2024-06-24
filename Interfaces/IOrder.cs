using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Models;

namespace E_commercial_Web_RESTAPI.Interfaces
{
    public interface IOrder
    {
        Task<Order> createOrderAsync(long customerId, string Address, long productId, int quantity);

        Task<List<Order>> GetOrderForCustomerAsync(OrderQueryObject query);

        Task<IReadOnlyList<Order>> GetAllOrders();

        Task<Order?> GetOrderByIdAsync(long id);
    }
}
