using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Services.Communication;

namespace TaskManager.API.Domain.Services
{
    public interface IAppRoleService
    {       
         Task<AppRoleResponse> DeleteAsync(int id);
         Task<IEnumerable<AppRole>> ListAsync();
         Task<AppRoleResponse> SaveAsync(AppRole role);
         Task<AppRoleResponse> UpdateAsync(int id, AppRole role);
    }
}