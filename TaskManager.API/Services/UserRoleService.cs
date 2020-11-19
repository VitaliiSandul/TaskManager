using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Repositories;
using TaskManager.API.Domain.Services;
using TaskManager.API.Domain.Services.Communication;

namespace TaskManager.API.Services
{
    public class UserRoleService : IUserRoleService
    {

        private readonly IAppRoleRepository roleRepository;
        private readonly IAppUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserRoleService(IAppRoleRepository roleRepository,
                               IAppUserRepository userRepository,
                               IUnitOfWork unitOfWork)
        {
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<AppUserResponse> DeleteRoleAsync(int userId, int roleId)
        {
            try 
            {
                AppUser user = await userRepository.FindByIdAsync(userId);
                user.UserRoles.Remove(user.UserRoles.SingleOrDefault(x => x.RoleId == roleId));
                await unitOfWork.CompleteAsync();
                return new AppUserResponse(user);
            }
            catch (Exception ex)
            {
                return new AppUserResponse($"Error when deleting the role: {ex.Message}");
            }
        }

        public async Task<IEnumerable<AppUser>> ListUsersByRoleAsync(int roleId)
        {
            var users = await userRepository.ListAsync();
            var usersInRole = users.Where(x => x.UserRoles.Contains(x.UserRoles.SingleOrDefault(y => y.RoleId == roleId)));
            return usersInRole;
        }

        public async Task<AppUserResponse> SetUserRoleAsync(int userId, int roleId)
        {
            try
            {
                AppUser user = await userRepository.FindByIdAsync(userId);
                user.UserRoles.Add(new UserRole {UserId = userId, RoleId = roleId});
                await unitOfWork.CompleteAsync();
                user = await userRepository.FindByIdAsync(userId);
                return new AppUserResponse(user);
            }
            catch (Exception ex)
            {
                return new AppUserResponse($"Error when setting the role: {ex.Message}");
            }
        }
    }
}