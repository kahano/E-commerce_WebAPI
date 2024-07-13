using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;

namespace E_commercial_Web_RESTAPI.Interfaces
{
    public interface ICartItem : IRepository<CartItem>
    {
        //public void AddCartItem(CartItem cartItem);

        public CartItem? GetCartItem(string userId);

        Task<bool> clearCart(string userId);

        public IEnumerable<CartItem> GetAllItems(string UserId);
        public IEnumerable<Product> GetUserProducts(string userId);

        public void DeleteCartItem(string UserId);
    }
}
