using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commercial_Web_RESTAPI.Repositories
{
    public class CartItemRepository_Impl : Repository_Impl<CartItem>, ICartItem
    {

        private readonly ApplicationDBcontext _context;
        public CartItemRepository_Impl(ApplicationDBcontext context) : base(context)
        {
            _context = context;
        }

        public Task<bool> clearCart(string userId)
        {
            var Items = _context.CarttItems.Where(k => k.UserId == userId);
            var cart = _context.carts.FirstOrDefault(x => x.UserId == userId);
            _context.CarttItems.RemoveRange(Items);
            _context.carts.Remove(cart);
            return Task.FromResult(true);
        }

        public void DeleteCartItem(string UserId)
        {
           
                var cartItem = GetCartItem(UserId);
         
                if (cartItem is not null)
                {
                   _context.Remove(cartItem);
                    _context.SaveChanges();
                }
            
        }

        public IEnumerable<CartItem> GetAllItems(string userId)
        {
            return _context.CarttItems.Where(c => c.UserId == userId).Include(k => k.Product).ToList();
        }


        public CartItem? GetCartItem(string userId)
        {
            return _context.CarttItems.Include(k => k.Product).FirstOrDefault(k => k.UserId == userId);
        }

        public IEnumerable<Product> GetUserProducts(string userId)
        {
            return _context.CarttItems.AsNoTracking().Where(k => k.UserId == userId).Select(c => c.Product);   
        }
    }
}
