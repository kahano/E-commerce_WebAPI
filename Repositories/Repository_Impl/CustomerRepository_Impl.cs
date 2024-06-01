using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.Helpers;
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
            return await _context.customers.Include(s => s.payments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Customer>> GetAllCustomers(CustomerQueryObject query)
        {
            var customers =   _context.customers.Include(x => x.payments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                customers = customers.Where(s => s.Name.Equals(query.Name));

            }
            if (!string.IsNullOrWhiteSpace(query.PhoneNumber))
            {
                customers = customers.Where(s => s.PhoneNumber.Equals(query.PhoneNumber));
            }
            return await customers.ToListAsync();

        }
    }
}
