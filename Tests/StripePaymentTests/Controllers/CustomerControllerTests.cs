using AutoFixture;
using E_commercial_Web_RESTAPI.Controllers;
using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.DTOS.Customers;
using E_commercial_Web_RESTAPI.Mapper.CustomerMappper;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.AutoMoq;
using E_commercial_Web_RESTAPI.DTOS.Payments;

namespace StripePaymentTests.Controllers
{
    public class CustomerControllerTests
    {
        //private readonly ApplicationDBcontext _context;
        private readonly ICustomerRepository _customer_repository;
        private readonly Mock<ICustomerRepository> _repositoryMock;
        private readonly Fixture _fixture;

        public CustomerControllerTests()
        {

            
            _fixture = new Fixture();
          

            // Handle circular references
            _fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _repositoryMock = _fixture.Freeze<Mock<ICustomerRepository>>();
            _customer_repository = _repositoryMock.Object;

        }


        [Fact]

        public async Task Should_create_customer()
        {

           

            var request_customer = new CustomerInfoRequestDTO()
            {

                Name = "Ahmed",
                PhoneNumber = "98300090000"
            };

            var returned_customer = _fixture.Create<Customer>();
            _repositoryMock.Setup(temp => temp.CreateCustomer(It.IsAny<Customer>()))
                .ReturnsAsync(returned_customer);

            CustomerController controller = new CustomerController(_customer_repository);

            IActionResult result =  await controller.CreateNewCustomer(_fixture.Create<CustomerInfoRequestDTO>());
            result.Should().NotBeNull();
            CreatedAtActionResult ans = Assert.IsType<CreatedAtActionResult>(result);
            result.Should().BeOfType<CreatedAtActionResult>();
            ans.Equals(request_customer);
            ans.Value.Should().BeAssignableTo<CustomerDTO>();
            _repositoryMock.Verify(temp => temp.CreateCustomer(It.IsAny<Customer>()), Moq.Times.Once());
            



        }

        [Fact]
        public async Task Should_ReturnOk_customer()
        {

            var customer = new Customer()
            {

                Name = "Ahmed",
                PhoneNumber = "98300090000",
               
            };

            var returned_customer = _fixture.Create<Customer?>();
            _repositoryMock.Setup(temp => temp.FindCustomerById(It.Is<long>(id => id > 0)))
                .ReturnsAsync(returned_customer);



            CustomerController controller = new CustomerController(_customer_repository);

            IActionResult result = await controller.GetById(_fixture.Create<long>());
            result.Should().NotBeNull();
            result.Equals(returned_customer);
            OkObjectResult ans = Assert.IsType<OkObjectResult>(result);
            result.Should().BeOfType<OkObjectResult>();
            ans.Equals(customer.ToCustomerDTO());
            ans.Value.Should().BeAssignableTo<CustomerDTO>();



        }

    }
}
