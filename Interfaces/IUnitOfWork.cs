using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Models;
using System.Linq.Expressions;

namespace E_commercial_Web_RESTAPI.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Order> _order_repository { get; }
        public IRepository<Product> _product_repository { get; }

        public ICart _cart_repository { get; }

        public ICustomerRepository _customer_repository { get; }
     



        Task<int> CommitChanges();
    }
}
