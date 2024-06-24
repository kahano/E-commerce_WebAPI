using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
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

        public void clearCart(long cartId)
        {
            var query = "DELETE FROM carts WHERE Id = @cartId";
            var parameters = new[] { new SqlParameter("@cartId", cartId) };
            var check = _context.Database.ExecuteSqlRaw(query, parameters);

            if (check <= 0)
            {
                new ApiResponse
                { Success = false, Message = "Problem Creating Order", StatusCode = 400 };
            }



        }

        

        public async Task<Cart> AddToCart(long customerId, long productId, int quantity)
        {

            Cart? cart = await this.GetBasketItemsAsyncByCustomerId(customerId);

            if (cart is null)
            {
                
                createCart(customerId);
                cart = await this.GetBasketItemsAsyncByCustomerId(customerId);
                var productitem = await _context.products.FindAsync(productId);
                if (productitem is not null && quantity <= productitem.Quantity)
                {
                    cart.BasketItems.Add(new BasketItem

                    {
                        CartId = cart.Id,
                        ProductId = productId,
                        ProductName = productitem.Name,
                        Quantity = quantity,
                        Price = productitem.price,
                        imageurl = productitem.imageurl

                    });
                    productitem.Quantity = productitem.Quantity - quantity;
                }
              

            }
            else
            {
         
                var indeks = cart.BasketItems.FindIndex(x => x.ProductId == productId);
                var productitem = await _context.products.FindAsync(productId);
                if(productitem is not null && quantity <= productitem.Quantity)
                {
                    if (indeks < 0 )
                    {
                        cart.BasketItems.Add(new BasketItem

                        {
                            CartId = cart.Id,
                            ProductId = productId,
                            ProductName = productitem.Name,
                            Quantity = quantity,
                            Price = productitem.price,
                            imageurl = productitem.imageurl

                        });

                    }
                    else
                    {
                        cart.BasketItems[indeks].Quantity += quantity;

                       
                    }
                   
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"product with ID {productitem.Id} does not exist more.");
                }
                productitem.Quantity = productitem.Quantity - quantity;
            }

            await this.UpdateBasketAsync(cart);
            return cart;
            }

        public async Task<bool> DeleteBasketItemsAsync(long Cartid , long CustomerId)
        {
            var cart = await _context.carts.Where(c => c.customerId == CustomerId).Include(c => c.BasketItems)
                .ThenInclude(k => k.Product).FirstOrDefaultAsync();


            if (cart is not null || cart.BasketItems is not null)
            {
                cart.BasketItems.RemoveAll(c => c.CartId == Cartid);
                await _context.SaveChangesAsync();
                return true;
            }       
            
            
            return false;

        }

        public async Task<Cart?> GetBasketItemsAsyncByBasketId(long BasketId)
        {
            return await _context.carts.Include(c => c.BasketItems)
               .ThenInclude(k => k.Product).FirstOrDefaultAsync(c => c.Id == BasketId);
        }

        public async Task<Cart?> GetBasketItemsAsyncByCustomerId(long CustomerId)
        {

            return await _context.carts.Include(c => c.BasketItems)
                .ThenInclude(k => k.Product).FirstOrDefaultAsync(c => c.customerId == CustomerId);

        }

        private void createCart(long customerId)
        {
             _context.carts.Add(new Cart()
             {
                 customerId = customerId,

               
              
             });
            _context.SaveChanges();

        }

        

        public async Task<Cart> UpdateBasketAsync(Cart basket)
        {
            _context.carts.Update(basket);
            await _context.SaveChangesAsync();
            return basket;
        }

    }
}
