using System.Threading.Tasks;
using TaskManager.API.Persistence.Contexts;
using TaskManager.API.Domain.Repositories;

namespace TaskManager.API.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskManagerDbContext context;
        public UnitOfWork(TaskManagerDbContext context)
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}