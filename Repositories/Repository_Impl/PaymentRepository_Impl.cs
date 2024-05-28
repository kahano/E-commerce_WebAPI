using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.DTOS;
using E_commercial_Web_RESTAPI.Extensions;
using E_commercial_Web_RESTAPI.Mapper;
using E_commercial_Web_RESTAPI.Mapper.CustomerMappper;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Models.Payment.Payment;
using E_commercial_Web_RESTAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Numerics;

namespace E_commercial_Web_RESTAPI.Repositories.Repository_Impl
{
    public class PaymentRepository_Impl : IPaymentRepository


    {
        private readonly ApplicationDBcontext _context;
        private readonly ICustomerRepository _customerRepository;
        private readonly StripePaymentService _paymentService;

       
        public PaymentRepository_Impl(ApplicationDBcontext context, ICustomerRepository customerRepository, StripePaymentService paymentService)
        {
            _context = context;
            _customerRepository = customerRepository;
            _paymentService = paymentService;
        }


        public async Task<ApiResponse> InsertPayment(long customerId, Payment payment)
        {
            var isCustomerAvailable = await _context.customers.FirstOrDefaultAsync(s => s.Id == customerId);
            if (isCustomerAvailable == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = $"Customer with ID {customerId} is not found",
                    StatusCode = 404
                };


            }
            payment.CustomerId = isCustomerAvailable.Id;

            var response = await _paymentService.ChargeCardsync(payment);

            if (response.Success)
            {
                await _context.payments.AddAsync(payment);
                await _context.SaveChangesAsync();
            }

            return response;





        }

       

        public async Task<Payment?> GetPaymentById(long customerId) =>
        
            await _context.payments.FirstOrDefaultAsync(p => p.CustomerId == customerId);
        

        public async Task<List<Payment>> GetAllPaymentsByCustomers() =>
            await _context.payments.ToListAsync();
        
    }
}
