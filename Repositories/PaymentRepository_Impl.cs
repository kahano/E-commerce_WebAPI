using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.DTOS;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Issuing;
using System.Numerics;

namespace E_commercial_Web_RESTAPI.Repositories
{
    //public class PaymentRepository_Impl : IPaymentRepository


    //{
    //    private readonly ApplicationDBcontext _context;
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IStripePaymentService _paymentService;


    //    public PaymentRepository_Impl(ApplicationDBcontext context, IStripePaymentService paymentService, IUnitOfWork unitOfWork)
    //    {
    //        _context = context;
    //        _paymentService = paymentService;
    //        _unitOfWork = unitOfWork;
    //    }






    //    public async Task<ApiResponse> CheckOutPayment(long orderId, long CartId, Payment payment)
    //    {

    //        var order = await _context.orders.FirstOrDefaultAsync(s => s.Id == orderId);
    //        if (order == null)
    //        {
    //            return new ApiResponse
    //            {
    //                Success = false,
    //                Message = $"order is not found",
    //                StatusCode = 404
    //            };


    //        }
    //        if (payment is null)
    //        {
    //            return new ApiResponse
    //            {
    //                Success = false,
    //                Message = $"payment is not found",
    //                StatusCode = 404
    //            };
    //        }

    //        payment.orderId = order.Id;
            

    //        var payment_process = await _paymentService.ChargeCardsync(orderId,CartId,payment);

    //        if (payment_process is null)
    //        {
                

    //            return new ApiResponse
    //            {
    //                Success = false,
    //                Message = $"payment is unsuccessful",
    //                StatusCode = 400
    //            };
    //        }

    //        await _context.payments.AddAsync(payment);

    //        await _unitOfWork._cart_repository.DeleteBasketItemsAsync(CartId);
    //        await _context.SaveChangesAsync();
         


    //        return new ApiResponse
    //        {
    //            Success = true,
    //            Message = $"Payment is Successful",
    //            StatusCode = 200
    //        };





    //    }



    //    public async Task<Payment?> GetPaymentById(long paymentId) => // to review 

    //        await _context.payments.FirstOrDefaultAsync(p => p.Id == paymentId);


    //    //public async Task<List<Payment>> GetAllPaymentsByCustomer(PaymentQueryObject query) // to review 
    //    //{
    //    //    var paymentsAll = _context.payments.Include(x => x.customer)
    //    //        .Include(c => c.Cart).ThenInclude(k => k.BasketItems).ThenInclude(x => x.Product)
    //    //        .AsQueryable();


    //    //    if (!string.IsNullOrWhiteSpace(query.source))
    //    //    {
    //    //        paymentsAll = paymentsAll.Where(s => s.source == query.source);
    //    //    }
    //    //    if (!string.IsNullOrWhiteSpace(query.CustomerId))
    //    //    {

    //    //        var val = long.Parse(query.CustomerId);
    //    //        paymentsAll = paymentsAll.Where(s => s.CustomerId == val);
    //    //    }
    //    //    return await paymentsAll.ToListAsync();

    //    //}

    //    public async Task<List<Payment>> GetAllPayments()
    //    {
    //        return await _context.payments.ToListAsync();
    //    }

    //}

       
}

