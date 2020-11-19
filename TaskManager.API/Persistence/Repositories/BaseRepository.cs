using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Domain.Repositories;
using TaskManager.API.Persistence.Contexts;

namespace TaskManager.API.Persistence.Repositories
{
    public abstract class BaseRepository<T>:IRepository<T> where T: class
    {
        protected readonly TaskManagerDbContext context;
        protected readonly DbSet<T> dbSet;
        public BaseRepository(TaskManagerDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await context.AddAsync(entity);
        }

        public virtual Task<T> FindByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> ListAsync()
        {
            return await dbSet.ToListAsync();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}