using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Repositories;
using TaskManager.API.Domain.Services;
using TaskManager.API.Domain.Services.Communication;

namespace TaskManager.API.Services
{
    public class AppRoleService : IAppRoleService
    {
        private readonly IAppRoleRepository roleRepository;
        private readonly IUnitOfWork unitOfWork;
        public AppRoleService(IAppRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            this.roleRepository = roleRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<AppRoleResponse> DeleteAsync(int id)
        {
            var existingRole = await roleRepository.FindByIdAsync(id);
            if (existingRole == null)
                return new AppRoleResponse("Role not found");

            try 
            {
                roleRepository.Remove(existingRole);
                await unitOfWork.CompleteAsync();

                return new AppRoleResponse(existingRole);
            }
            catch (Exception ex)
            {
                return new AppRoleResponse($"Error when deleting role: {ex.Message}");
            }
        }

        public async Task<IEnumerable<AppRole>> ListAsync()
        {
            return await roleRepository.ListAsync();
        }

        public async Task<AppRoleResponse> SaveAsync(AppRole role)
        {
            try
            {
                await roleRepository.AddAsync(role);
                await unitOfWork.CompleteAsync();

                return new AppRoleResponse(role);
            }
            catch(Exception ex)
            {
                return new AppRoleResponse($"Error when saving role: {ex.Message}");
            }
        }    

        public async Task<AppRoleResponse> UpdateAsync(int id, AppRole role)
        {
            var existingRole = await roleRepository.FindByIdAsync(id);
            if (existingRole == null)
                return new AppRoleResponse("Role not found");

            existingRole.RoleName = role.RoleName;         

            try 
            {
                roleRepository.Update(existingRole);
                await unitOfWork.CompleteAsync();

                return new AppRoleResponse(existingRole);
            }
            catch (Exception ex)
            {
                return new AppRoleResponse($"Error when deleting role: {ex.Message}");
            }
        }

    }
}