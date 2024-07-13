using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commercial_Web_RESTAPI.Repositories
{
    public class ProductRepository_Impl :IProduct
    {
        private readonly ApplicationDBcontext _context;

        public ProductRepository_Impl(ApplicationDBcontext context) { 

            _context = context;
        }

        public Product? GetProductById(long Id)
        {
            return _context.products.FirstOrDefault(p => p.Id == Id);   
        }

        public void Update(Product product)
        {
            _context.products.Update(product);
        }
    }
}
