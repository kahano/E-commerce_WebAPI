using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using E_commercial_Web_RESTAPI.Models;
using Microsoft.EntityFrameworkCore;

using E_commercial_Web_RESTAPI.Repositories.Repository_Impl;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.DTOS.Customers;
using E_commercial_Web_RESTAPI.Mapper.CustomerMappper;
using FluentAssertions;
using Stripe;
using Customer = E_commercial_Web_RESTAPI.Models.Customer;
using E_commercial_Web_RESTAPI.Services;
using System.Diagnostics.Eventing.Reader;
using Castle.Core.Resource;


namespace StripePaymentTests.Respositories
{
    public class Repository_ImplTests
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
                    databaseContext.customers.Add(
                    new Customer()
                    {

                        Name = "Karl",
                        PhoneNumber = "983000901100"
                    });
                     await databaseContext.SaveChangesAsync();

                }


            }


            //if (await databaseContext.payments.CountAsync() <= 0)
            //{
                //for (int i = 1; i < 2; i++)
                //{
                //    databaseContext.payments.Add(
                //        new Payment()
                //        {

                //            amount = 10000,
                //            Currency = Currency.USD,
                //            source = "tok_visa"
                //        });

                //    databaseContext.payments.Add(
                //    new Payment()
                //    {

                //        amount = 150000,
                //        Currency = Currency.NOK,
                //        source = "tok_mastercard"

                //    });

             
                    //await databaseContext.SaveChangesAsync();

               //}
            //}
            return databaseContext;

        }
               
              
        





        [Fact]
        public async Task createCustomer()
        {
            var dbContext = await GetDatabaseContext();

            var customer_repository = new CustomerRepository_Impl(dbContext);

            Customer nyCustomer = new Customer() { Name = "Henrik", PhoneNumber = "91204500000" };

            await customer_repository.CreateCustomer(nyCustomer);
            List<Customer> customers = await customer_repository.GetCustomers();

            customers.Should().NotBeNull();
            customers.Should().BeOfType<List<Customer>>();
            Assert.NotNull(customers);
            Assert.Contains(nyCustomer, customers);
            Assert.NotEqual("Karl",nyCustomer.Name);
            Assert.Equal("Henrik",nyCustomer.Name);
            customers.Count.Should().BeInRange(0, 10);
            Assert.Equal(3,customers.Count);
            Assert.Distinct(customers);
    
            

        }


        [Fact]

        public async Task getCustomers()
        {
            //Arrange


            var dbContext = await GetDatabaseContext();

            var customer_repository = new CustomerRepository_Impl(dbContext);

            //Act 
            List<Customer> customers = await customer_repository.GetCustomers();
            var customer = await customer_repository.FindCustomerById(1);


            //Assert 
            customers.Should().NotBeNull();
            customers.Should().BeOfType<List<Customer>>();
            Assert.NotNull(customers);
            Assert.Contains(customer, customers);
            Assert.Equal("Ahmed", customer.Name);
            Assert.NotEqual("Karl", customer.Name);
            Assert.True(customers.Any());

        }

        [Fact]
        public async Task makePayment()
        {
            var dbContext = await GetDatabaseContext();
            var payment_repository = new PaymentRepository_Impl(dbContext, new StripePaymentService());
            var customer_repository = new CustomerRepository_Impl(dbContext);
            var customer2 = await customer_repository.FindCustomerById(2);
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
            var response =  await payment_repository.InsertPayment(customer2.Id, payment1);
           
            List<Payment> Allpayments = await payment_repository.GetAllPayments();
            Allpayments.Should().NotBeNull();
            Allpayments.Should().BeOfType<List<Payment>>();
            Allpayments.Count.Should().BeGreaterThan(0);
            Assert.True(response.Success);
         


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

           

     
            var customer1 = await customer_repository.FindCustomerById(1);
            var customer2 = await customer_repository.FindCustomerById(2);
            await payment_repository.InsertPayment(1, payment1);
            await payment_repository.InsertPayment(2, payment2);
        
    
            List<Payment> Allpayments = await payment_repository.GetAllPayments();
            Allpayments.Count.Should().BeGreaterThan(0);
            var returned_payment1 = await payment_repository.GetPaymentById(1);
            var returned_payment2 = await payment_repository.GetPaymentById(2);
            Assert.Contains(returned_payment1, Allpayments);
            Assert.Contains(returned_payment2, Allpayments);
      
        }


    }

}
