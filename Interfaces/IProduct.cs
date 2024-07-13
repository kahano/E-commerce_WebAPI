using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;

namespace E_commercial_Web_RESTAPI.Interfaces
{
    public interface IProduct 
    {
        void Update(Product product);

        Product? GetProductById(long Id);

        
    }
}
