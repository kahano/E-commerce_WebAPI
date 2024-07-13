using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Polly;
using StackExchange.Redis;
using Stripe;
using Stripe.Issuing;
using System.Linq;
using System.Text.Json;
using Product = E_commercial_Web_RESTAPI.Models.Product;

namespace E_commercial_Web_RESTAPI.Repositories
{
    public class CartRepository_Impl : Repository_Impl<Cart>,ICart

    {
         private readonly ApplicationDBcontext _context;
        public CartRepository_Impl(ApplicationDBcontext context) : base(context) 
        {
            _context = context;
        }

        

      

        public Cart CreateCart(string UserId)
        {
            //var CartItems = _context.CarttItems.Where(x => x.UserId == UserId).Include(k => k.Product).ToList();
         
                Cart cart = new Cart()
                {
                    UserId = UserId,
                   // Items = CartItems
                
                   
                };
                _context.carts.Add(cart);
                _context.SaveChanges();
                return cart;
            
         

        }

        
        public Cart? GetCart(string UserId)
        {
            var cart = _context.carts.AsNoTracking().Include(k => k.Items).ThenInclude(k => k.Product).FirstOrDefault(x => x.UserId == UserId);
            return cart;
           
        }
    }
}
