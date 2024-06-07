using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories.Repository_Impl;
using E_commercial_Web_RESTAPI.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripePaymentTests.Respositories
{
    public class PaymentRepositoryTests
    {
        private async Task<ApplicationDBcontext> GetDatabaseContext()
        {

            var options = new DbContextOptionsBuilder<ApplicationDBcontext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;
            var databaseContext = new ApplicationDBcontext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.customers.CountAsync() <= 0)
            {
                for (int i = 1; i < 2; i++)
                {
                    databaseContext.customers.Add(
                        new Customer()
                        {

                            Name = "Ahmed",
                            PhoneNumber = "98300090000"
                        });
                }

            }

            if (await databaseContext.payments.CountAsync() <= 0)
            {
                for (int i = 1; i < 2; i++)
                {
                    databaseContext.payments.DefaultIfEmpty();

                 


                    await databaseContext.SaveChangesAsync();

                }
            }
            return databaseContext;

        }
    

        [Fact]
        public async Task makePayment()
        {
            var dbContext = await GetDatabaseContext();
            var payment_repository = new PaymentRepository_Impl(dbContext, new StripePaymentService());
            var customer_repository = new CustomerRepository_Impl(dbContext);
            var customer = await customer_repository.FindCustomerById(1);


            var payment1 = new Payment()
            {

                amount = 18500,
                Currency = Currency.EUR,
                source = "tok_visa"

            };

            var payment2 = new Payment()
            {

                amount = 10000,
                Currency = Currency.USD,
                source = "tok_visa"
            };

            var payment3 = new Payment()
            {

                amount = 150000,
                Currency = Currency.NOK,
                source = "tok_mastercard"

            };




            var paymentlist = dbContext.payments.ToList();
            var response1 = await payment_repository.InsertPayment(customer.Id, payment1);
            var response2 = await payment_repository.InsertPayment(customer.Id, payment2);

            List<Payment> Allpayments = await payment_repository.GetAllPayments();
            Allpayments.Should().NotBeNull();
            Allpayments.Should().BeOfType<List<Payment>>();
            Allpayments.Count.Should().BeGreaterThan(0);
            Assert.True(response1.Success);
            Assert.True(response2.Success);



        }

            [Fact]

            public async Task GetTransaction()
            {
                var dbContext = await GetDatabaseContext();
                var paymentList = dbContext.payments.ToList();
                var payment_repository = new PaymentRepository_Impl(dbContext, new StripePaymentService());
                var customer_repository = new CustomerRepository_Impl(dbContext);
                var payment1 = new Payment()
                {

                    amount = 18500,
                    Currency = Currency.EUR,
                    source = "tok_visa"

                };

                var payment2 = new Payment()
                {

                    amount = 10000,
                    Currency = Currency.USD,
                    source = "tok_visa"
                };


                var payment3 = new Payment()
                {

                    amount = 150000,
                    Currency = Currency.NOK,
                    source = "tok_mastercard"

                };




            var customer1 = await customer_repository.FindCustomerById(1);
           
                await payment_repository.InsertPayment(1, payment1);
                await payment_repository.InsertPayment(1, payment2);


                List<Payment> Allpayments = await payment_repository.GetAllPayments();
                Allpayments.Count.Should().BeGreaterThan(0);
                var returned_payment1 = await payment_repository.GetPaymentById(1);
                var returned_payment2 = await payment_repository.GetPaymentById(2);
                Assert.Contains(returned_payment1, Allpayments);
                Assert.Contains(returned_payment2, Allpayments);

             }


    }



}
