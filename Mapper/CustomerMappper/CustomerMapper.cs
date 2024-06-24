using E_commercial_Web_RESTAPI.DTOS.Customers;
using E_commercial_Web_RESTAPI.DTOS.Order;
using E_commercial_Web_RESTAPI.Models;
using System.Runtime.CompilerServices;

namespace E_commercial_Web_RESTAPI.Mapper.CustomerMappper
{
    public static class CustomerMapper
    {
        public static CustomerDTO ToCustomerDTO(this Customer customer)
        {
           
            return new CustomerDTO(

                customer.Id,
                customer.Name,
                customer.PhoneNumber,
                customer.payments.Select(s => s.ToPaymentDTO()).ToList()
            );
        }

        public static Customer ToCustomerFromRequestDTO(this CustomerInfoRequestDTO customer)
        {
            
            return new Customer {

             Name = customer.Name,
             PhoneNumber = customer.PhoneNumber
            };
        }
    }
}
