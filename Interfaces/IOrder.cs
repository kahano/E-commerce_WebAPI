using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;

namespace E_commercial_Web_RESTAPI.Interfaces
{
    public interface IOrder  
    {
        Task<Order> PlaceOrderAsync(string UserId, string Address);

        Task<IEnumerable<Order>> GetOrderForUserAsync(string UserId);


        Task<Order?> GetOrderByIdAsync(long id);
    }
}
