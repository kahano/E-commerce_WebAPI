using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories.Repository_Impl;
using E_commercial_Web_RESTAPI.Services;
using System.Linq.Expressions;

namespace E_commercial_Web_RESTAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBcontext _context;
        
        private  CustomerRepository_Impl? customerRepository;

        private CartRepository_Impl? cartRepository;

        public IRepository<Order> _order_repository { get; private set; }

        public IRepository<Product> _product_repository { get; private set; }

        public ICart _cart_repository => cartRepository ??= new CartRepository_Impl(_context);

        public ICustomerRepository _customer_repository => customerRepository ??= new CustomerRepository_Impl(_context);


        public UnitOfWork(ApplicationDBcontext context)
        {
            _context = context;
            _order_repository = new Repository_Impl<Order>(_context);
            _product_repository = new Repository_Impl<Product>(_context);
         
        
        }


        public async Task<int> CommitChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        
    }
}
