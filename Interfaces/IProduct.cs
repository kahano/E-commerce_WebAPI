using E_commercial_Web_RESTAPI.Models;

namespace E_commercial_Web_RESTAPI.Interfaces
{
    public interface IProduct
    {
        Task<Product> AddProductAsync(Product p);

        Task<List<Product>> GetAllProductsAsync();
    }
}
