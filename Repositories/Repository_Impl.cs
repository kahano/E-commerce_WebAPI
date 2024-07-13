using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Linq.Expressions;

namespace E_commercial_Web_RESTAPI.Repositories
{
    public class Repository_Impl<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDBcontext _context;
        


        public Repository_Impl(ApplicationDBcontext context)
        {
            _context = context;
        }

       

        public void Add(T entity)
        {
           _context.Set<T>().Add(entity);   
        }

      

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T? GetByIdAsync(long Id)
        {
            return  _context.Set<T>().FirstOrDefault(x => x.Id == Id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

       
    }
}
