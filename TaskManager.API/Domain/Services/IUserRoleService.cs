using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Services.Communication;

namespace TaskManager.API.Domain.Services
{
    public interface IUserRoleService
    {
         Task<AppUserResponse> DeleteRoleAsync(int userId, int roleId);
         Task<IEnumerable<AppUser>> ListUsersByRoleAsync(int roleId);
         Task<AppUserResponse> SetUserRoleAsync(int userId, int roleId);
    }
}