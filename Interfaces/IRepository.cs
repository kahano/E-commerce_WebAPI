using E_commercial_Web_RESTAPI.Models;
using System.Linq.Expressions;

namespace E_commercial_Web_RESTAPI.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T? GetByIdAsync(long Id);

        Task<IReadOnlyList<T>> GetAllAsync();

        void Add(T entity);

        void Update(T entity); 
       
        void Delete(T entity);
        
    }
}
