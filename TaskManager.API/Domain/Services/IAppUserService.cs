using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Services.Communication;
using TaskManager.API.Filters;

namespace TaskManager.API.Domain.Services
{
    public interface IAppUserService
    {         
         Task<AppUserResponse> DeleteAsync(int id);
         Task<IEnumerable<AppUser>> ListAsync();
         Task<AppUserResponse> SaveAsync(AppUser user);
         Task<AppUserResponse> UpdateAsync(int id, AppUser user);
         Task<IEnumerable<AppUser>> SearchAsync(UserSearchOptions searchOptions);
    }
}