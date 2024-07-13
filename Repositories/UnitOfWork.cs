using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Services;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace E_commercial_Web_RESTAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBcontext _context;
        

        private CartRepository_Impl? cartRepository;
        private CartItemRepository_Impl? cartItemRepository;
        private ProductRepository_Impl? productRepository;
        private OrderItemRepository_Impl? OrderItemRepository;

        public IRepository<Order> _order_repository { get; private set; }


        public ICart _cart_repository => cartRepository ??= new CartRepository_Impl(_context);

  
        public ICartItem _cartItem_repository { get; private set; }

        public IOrderItem _orderItem_repository { get; private set; }

        public IProduct _product_repository => productRepository ??= new ProductRepository_Impl(_context);  

        public UnitOfWork(ApplicationDBcontext context)
        {
            _context = context;
            _order_repository = new Repository_Impl<Order>(_context);
            _cartItem_repository = new CartItemRepository_Impl(_context);
            _orderItem_repository = new OrderItemRepository_Impl(_context);
      
         
        
        }


        public async Task<int> CommitChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
