using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Repositories;
using TaskManager.API.Filters;
using TaskManager.API.Persistence.Contexts;

namespace TaskManager.API.Persistence.Repositories
{
    public class AppRoleRepository : BaseRepository<AppRole>, IAppRoleRepository
    {
        public AppRoleRepository(TaskManagerDbContext context) : base(context)
        {
        }
        public async override Task<AppRole> FindByIdAsync(int id)
        {
            return await context.AppRole.FirstOrDefaultAsync(x => x.RoleId == id);
        }

    }
}