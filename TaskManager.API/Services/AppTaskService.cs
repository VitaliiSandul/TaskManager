using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Repositories;
using TaskManager.API.Domain.Services;
using TaskManager.API.Domain.Services.Communication;
using TaskManager.API.Filters;

namespace TaskManager.API.Services
{
    public class AppTaskService : IAppTaskService
    {
        private readonly IAppTaskRepository appTaskRepository;
        private IUnitOfWork unitOfWork;
        public AppTaskService(IAppTaskRepository appTaskRepository,
                               IUnitOfWork unitOfWork)
        {
            this.appTaskRepository = appTaskRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<SaveAppTaskResponse> DeleteAsync(int id)
        {
            var existingAppTask = await appTaskRepository.FindByIdAsync(id);

            if (existingAppTask == null)
                return new SaveAppTaskResponse("Task not found");

            try 
            {
                appTaskRepository.Remove(existingAppTask);
                await unitOfWork.CompleteAsync();

                return new SaveAppTaskResponse(existingAppTask);
            }
            catch (Exception ex)
            {
                return new SaveAppTaskResponse($"Error when deleting task: {ex.Message}");
            }
        }

        public async Task<IEnumerable<AppTask>> ListAsync()
        {
            return await appTaskRepository.ListAsync();
        }

        public async Task<SaveAppTaskResponse> SaveAsync(AppTask appTask)
        {
            try 
            {
                await appTaskRepository.AddAsync(appTask);
                await unitOfWork.CompleteAsync();

                return new SaveAppTaskResponse(appTask);
            }
            catch (Exception ex)
            {
                return new SaveAppTaskResponse($"Save task error: {ex.Message}");
            }
        }

        public async Task<SaveAppTaskResponse> UpdateAsync(int id, AppTask appTask)
        {
            var existingAppTask = await appTaskRepository.FindByIdAsync(id);

            if (existingAppTask == null)
                return new SaveAppTaskResponse("Task not found");            
            
            existingAppTask.UserId = appTask.UserId;
            existingAppTask.TaskTitle = appTask.TaskTitle;
            existingAppTask.TaskDetails = appTask.TaskDetails;
            existingAppTask.TaskCreationDate = appTask.TaskCreationDate;
            existingAppTask.TaskStatus = appTask.TaskStatus;
            existingAppTask.TaskPriority = appTask.TaskPriority;

            try
            {
                appTaskRepository.Update(existingAppTask);
                await unitOfWork.CompleteAsync();

                return new SaveAppTaskResponse(existingAppTask);
            }
            catch (Exception ex)
            {
                return new SaveAppTaskResponse($"Error when updating task: {ex.Message}");
            }
        }

        public async Task<IEnumerable<AppTask>> SearchAsync(TaskSearchOptions searchOptions)
        {
            return await appTaskRepository.SearchAsync(searchOptions);
        }
    }
}