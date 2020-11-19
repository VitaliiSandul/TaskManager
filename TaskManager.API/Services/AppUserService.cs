using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Repositories;
using TaskManager.API.Domain.Services;
using TaskManager.API.Domain.Services.Communication;
using TaskManager.API.Filters;
using TaskManager.API.Persistence.Repositories;

namespace TaskManager.API.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        public AppUserService(IAppUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<AppUserResponse> DeleteAsync(int id)
        {
            var existingUser = await userRepository.FindByIdAsync(id);
            if (existingUser == null)
                return new AppUserResponse("User not found");

            try 
            {
                userRepository.Remove(existingUser);
                await unitOfWork.CompleteAsync();

                return new AppUserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new AppUserResponse($"Error when deleting user: {ex.Message}");
            }
        }

        public async Task<IEnumerable<AppUser>> ListAsync()
        {
            return await userRepository.ListAsync();
        }

        public async Task<AppUserResponse> SaveAsync(AppUser user)
        {
            try
            {
                await userRepository.AddAsync(user);
                await unitOfWork.CompleteAsync();

                return new AppUserResponse(user);
            }
            catch(Exception ex)
            {
                return new AppUserResponse($"Error when saving user: {ex.Message}");
            }
        }

        public async Task<IEnumerable<AppUser>> SearchAsync(UserSearchOptions searchOptions)
        {
            return await userRepository.SearchAsync(searchOptions);
        }

        public async Task<AppUserResponse> UpdateAsync(int id, AppUser user)
        {
            var existingUser = await userRepository.FindByIdAsync(id);
            if (existingUser == null)
                return new AppUserResponse("User not found");

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;        
            existingUser.Email = user.Email;
            existingUser.Photo = user.Photo;
            existingUser.Birthday = user.Birthday;
            existingUser.Phone = user.Phone;
            existingUser.Password = user.Password;

            try 
            {
                userRepository.Update(existingUser);
                await unitOfWork.CompleteAsync();

                return new AppUserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new AppUserResponse($"Error when deleting user: {ex.Message}");
            }
        }
    }
}