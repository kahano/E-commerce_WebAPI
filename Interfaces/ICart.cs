using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;

namespace E_commercial_Web_RESTAPI.Interfaces
{
    public interface ICart : IRepository<Cart>
    {
        Task<Cart?> GetBasketItemsAsyncByCustomerId( long customerId);
        Task<Cart?> GetBasketItemsAsyncByBasketId( long BasketId);

        Task<Cart> AddToCart(long customerId ,long productId, int quantity);

        void clearCart(long cartId);

       // Task<Cart> AddCartCompleted(long customerId);

        //Task<Cart> createCart(long customerId);

        Task<Cart> UpdateBasketAsync(Cart basket);

        Task<bool> DeleteBasketItemsAsync(long Cartid, long customerId);
    }
}
