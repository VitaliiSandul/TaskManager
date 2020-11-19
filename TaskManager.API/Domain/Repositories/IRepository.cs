using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.API.Filters;

namespace TaskManager.API.Domain.Repositories
{
    public interface IRepository<T>
    {
         Task<IEnumerable<T>> ListAsync();
         Task AddAsync(T entity);
         Task<T> FindByIdAsync(int id);
         void Update(T entity);
         void Remove(T entity);         
    }
}