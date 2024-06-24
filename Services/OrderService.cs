using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_commercial_Web_RESTAPI.Services
{
    public class OrderService : IOrder
    {
      
        private readonly ApplicationDBcontext _context;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService( IUnitOfWork unitOfWork, ApplicationDBcontext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        }
        public async Task<Order> createOrderAsync(long customerId, string Address, long productId, int quantity) 

        {
            //  Check if the customer exists


            var customerExists = await _unitOfWork._customer_repository.DoesCustomerExists(c => c.Id == customerId);
            if (!customerExists)
            {
                throw new ArgumentException($"Customer with ID {customerId} does not exist.");
            }
         
            var cart = await _unitOfWork._cart_repository.AddToCart(customerId, productId, quantity);

            var items = new List<OrderItem>();
            
            foreach (var item in cart.BasketItems)
            {
           
                var productitem = await _unitOfWork._product_repository.GetByIdAsync(item.ProductId);
                if (productitem == null)
                {
                    throw new NullReferenceException($"Product with ID {item.ProductId} not found.");
                }
                var Ordereditem = new OrderItem
                {
                    ProductId = productitem.Id,
                    ProductName = productitem.Name,
                    Price = productitem.price,
                    Quantity = quantity,
                    PictureUrl = item.imageurl


                };
                items.Add(Ordereditem);
            }
            var totalpurchase = items.Sum(item => item.Price * item.Quantity);
            var order = new Order()
            {
                CustomerId = customerId,
                Address = Address,
                total = totalpurchase,
                OrderItems = items
                

            };
            _unitOfWork._order_repository.Add(order);
            var res = await _unitOfWork.CommitChanges();
            if (res <= 0)
            {
                throw new Exception("Error saving order.");
            }
           
            await _context.SaveChangesAsync();
            return order;


        }

        public async Task<IReadOnlyList<Order>> GetAllOrders() // all orders of all customers purchased
        {
              return await _context.orders.Include(k => k.OrderItems).ThenInclude(oi => oi.Product)
                .Include(o => o.Customer).ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(long id)
        {

            return await _context.orders.Include(k => k.OrderItems).ThenInclude(oi => oi.Product)
                .Include(o => o.Customer).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Order>> GetOrderForCustomerAsync(OrderQueryObject query) 
        {
            var orders = _context.orders.Include(o => o.Customer)
                               .Include(k => k.OrderItems)
                               .ThenInclude(oi => oi.Product)
                               .AsQueryable();

            if (!string.IsNullOrEmpty(query.customerId))
            {
                long customerId;
                if (long.TryParse(query.customerId, out customerId))
                {
                    orders = orders.Where(x => x.CustomerId == customerId);
                }
                else
                {
                    return new List<Order>();
                }
            }

            return await orders.ToListAsync();


      
        }


    }
}
