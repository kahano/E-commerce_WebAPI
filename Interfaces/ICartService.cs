using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Models;

namespace E_commercial_Web_RESTAPI.Interfaces
{
    public interface ICartService
    {
        Task<ApiResponse> AddItemToCart(string UserId, long productId, int quantity);

        Task<ApiResponse> UpdateCartItemsQuantityAsync(string? UserId, long productId, int quantity);

        Task<ApiResponse> UpdateCartItem(CartItem Item);

      
    }
}
