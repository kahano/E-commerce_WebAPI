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


        public async Task<dynamic> InsertPayment(long customerId, Payment payment)
        {
            var isCustomerAvailable = await _context.customers.FirstOrDefaultAsync(s => s.Id == customerId);
            if (isCustomerAvailable == null)
            {
                throw new Exception("Customer is not found");

            }
            if (!CheckCardCharge.CheckCardCurrency(payment.ToPaymentDTO()))
            {
                          throw new InvalidOperationException("This PaymentCard is not valid");
                           
            }
         

             payment.CustomerId =  customerId;
            await _context.payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return await  _paymentService.ChargeCardsync(payment);
           
           
           
        }

       

        public async Task<Payment?> GetPaymentById(long customerId)
        {
            return await _context.payments.FirstOrDefaultAsync(p => p.CustomerId == customerId);
        }

        public async Task<List<Payment>> GetAllPaymentsByCustomers()
        {
            return await _context.payments.ToListAsync();
        }
    }
}
