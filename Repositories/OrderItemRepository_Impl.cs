using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commercial_Web_RESTAPI.Repositories
{
    public class OrderItemRepository_Impl : Repository_Impl<OrderItem>, IOrderItem
    {
        private readonly ApplicationDBcontext _context;

        public OrderItemRepository_Impl(ApplicationDBcontext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order?> GetByPaymentIdAsync(string paymentId)
        {
            return await _context.orders.FirstOrDefaultAsync(o => o.PaymentId == paymentId);
        }

        public OrderItem? GetOrderItemById(long Id)
        {
            return _context.OrderItems.Include(k => k.Product).FirstOrDefault(k => k.Id == Id);
        }
    }
}
