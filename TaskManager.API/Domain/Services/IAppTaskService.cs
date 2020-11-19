using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.API.Domain.Models;
using TaskManager.API.Domain.Services.Communication;
using TaskManager.API.Filters;

namespace TaskManager.API.Domain.Services
{
    public interface IAppTaskService
    {
         Task<IEnumerable<AppTask>> ListAsync();
         Task<SaveAppTaskResponse> SaveAsync(AppTask appTask);
         Task<SaveAppTaskResponse> UpdateAsync(int id, AppTask appTask);
         Task<SaveAppTaskResponse> DeleteAsync(int id);
         Task<IEnumerable<AppTask>> SearchAsync(TaskSearchOptions searchOptions);
    }
}