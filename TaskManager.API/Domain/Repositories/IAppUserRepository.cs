using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.API.Domain.Models;
using TaskManager.API.Filters;

namespace TaskManager.API.Domain.Repositories
{
    public interface IAppUserRepository : IRepository<AppUser>
    {   
        Task<IEnumerable<AppUser>> SearchAsync(UserSearchOptions searchOptions);      
    }
}