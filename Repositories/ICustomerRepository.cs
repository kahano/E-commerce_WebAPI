using E_commercial_Web_RESTAPI.Helpers;
using E_commercial_Web_RESTAPI.Models;

namespace E_commercial_Web_RESTAPI.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer?> FindCustomerById(long id);

        Task<List<Customer>> GetAllCustomers(CustomerQueryObject query);
        Task<List<Customer>> GetCustomers(); // for testing purposes


    }
}
