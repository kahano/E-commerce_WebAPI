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

        public async Task<Product> AddProductAsync(Product p)
        {
            await _context.products.AddAsync(p);
            await _context.SaveChangesAsync();
            return p;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.products.ToListAsync();
        }
    }
}
