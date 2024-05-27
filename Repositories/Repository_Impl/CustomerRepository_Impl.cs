using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commercial_Web_RESTAPI.Repositories.Repository_Impl
{
    public class CustomerRepository_Impl : ICustomerRepository

    {
        private readonly ApplicationDBcontext _context;

        public CustomerRepository_Impl(ApplicationDBcontext context)
        {
            _context = context;

        
            
        }
        public async Task<Customer> CreateCustomer(Customer customer)
        {
             await _context.customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
            
        }

        public async Task<Customer?> FindCustomerById(long id)
        {
            return await _context.customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _context.customers.ToListAsync();

        }
    }
}
