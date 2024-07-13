using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace E_commercial_Web_RESTAPI.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Order> _order_repository { get; }
        public IProduct _product_repository { get; }

        public ICart _cart_repository { get; }

        public IOrderItem _orderItem_repository { get; }

        public ICartItem _cartItem_repository { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> CommitChanges();
    }
}
