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
        private IStripePaymentService _stripePaymentService;

        public OrderService(IUnitOfWork unitOfWork, ApplicationDBcontext context, IStripePaymentService stripePaymentService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _stripePaymentService = stripePaymentService ?? throw new ArgumentNullException();

        }
       
       

       

        public async Task<IEnumerable<Order>> GetOrderForUserAsync(string UserId)
        {
            return await _context.orders.Where(k => k.UserId == UserId).Include(k => k.OrderItems).ThenInclude(k => k.Product).ToListAsync();
        }

        public async Task<Order> PlaceOrderAsync(string UserId, string Address)

        {
           

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    Cart? cart = _unitOfWork._cart_repository.GetCart(UserId);
                    if (cart == null || !cart.Items.Any())
                    {
                        throw new InvalidOperationException("Cart is empty or not found.");
                    }

                    //var cart = await _stripePaymentService.CreateOrUpdatePayment(UserId);


                    var orderitems = new List<OrderItem>();

                    foreach (var item in cart.Items)
                    {

                        var Ordereditem = new OrderItem
                        {
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            PictureUrl = item.imageurl


                        };
                        orderitems.Add(Ordereditem);
                    }
                    var totalpurchase = cart.Items.Sum(item => item.Price * item.Quantity);
                    

                    var existingOrder = await _unitOfWork._orderItem_repository.GetByPaymentIdAsync(cart.PaymentId);
                    if (existingOrder != null)
                    {
                        _unitOfWork._order_repository.Delete(existingOrder);
                        await _stripePaymentService.CreateOrUpdatePayment(UserId);


                    }
                   
                    var order = new Order()
                    {
                        UserId = UserId,
                        customerName= 
                        Address = Address,
                        total = totalpurchase,
                        Status = OrderStatus.PaymentSucceeded.ToString(),
                        OrderItems = orderitems,
                        PaymentId = cart.PaymentId,


                    };
                    _unitOfWork._order_repository.Add(order);

                    await _unitOfWork._cartItem_repository.clearCart(UserId);
                    var res = await _unitOfWork.CommitChanges();
                    if (res <= 0)
                    {
                        throw new Exception("Error saving order.");
                    }

          
                    await transaction.CommitAsync();    
                    return order;
                }
                catch (Exception ex)
                {

                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

      




        public async Task<Order?> GetOrderByIdAsync(long id)
        {

            return await _context.orders.Include(k => k.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(x => x.Id == id);
        }




    }
}
