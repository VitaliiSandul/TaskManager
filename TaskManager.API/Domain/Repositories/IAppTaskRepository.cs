using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.API.Domain.Models;
using TaskManager.API.Filters;

namespace TaskManager.API.Domain.Repositories
{
    public interface IAppTaskRepository: IRepository<AppTask>
    {
        Task<IEnumerable<AppTask>> SearchAsync(TaskSearchOptions searchOptions);
    }
}