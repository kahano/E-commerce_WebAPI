using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Models;
using System.Linq.Expressions;

namespace E_commercial_Web_RESTAPI.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer?> FindCustomerById(long id);

       
        Task<List<Customer>> GetAllCustomers(CustomerQueryObject query);

        Task<bool> DoesCustomerExists(Expression<Func<Customer, bool>> predicate);
        

        Task<List<Customer>> GetCustomers(); // for testing purposes


    }
}
