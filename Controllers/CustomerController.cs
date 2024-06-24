using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.DTOS.Customers;
using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Mapper.CustomerMappper;
using E_commercial_Web_RESTAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commercial_Web_RESTAPI.Controllers
{
   
 
    public class CustomerController : APIBaseController
    {
     
        private readonly ICustomerRepository _customer_repository;

        public CustomerController( ICustomerRepository customer_repository)
        {
          
            _customer_repository = customer_repository;
        }


        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateNewCustomer(CustomerInfoRequestDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerModel = await _customer_repository.CreateCustomer(customer.ToCustomerFromRequestDTO());
            return CreatedAtAction(nameof(GetById), new { id = customerModel.Id }, customerModel.ToCustomerDTO());

        }

        [HttpGet("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerModel = await _customer_repository.FindCustomerById(id);
            if (customerModel == null)
            {
                return NotFound();
            }
            return Ok(customerModel.ToCustomerDTO());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] CustomerQueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customers = await _customer_repository.GetAllCustomers(query);
            if(customers is null || !customers.Any())
            {
                return NotFound("No customer found ! ");
            }
            var customerModel = customers.Select(s => s.ToCustomerDTO()).ToList();
            return Ok(customerModel);

        }

    }
}
