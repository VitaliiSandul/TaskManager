using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Domain.Models;
using TaskManager.API.Persistence.Contexts;
using TaskManager.API.Domain.Repositories;
using TaskManager.API.Filters;
using TaskManager.API.Extensions;

namespace TaskManager.API.Persistence.Repositories
{
    public class AppTaskRepository : BaseRepository<AppTask>, IAppTaskRepository
    {
        public AppTaskRepository(TaskManagerDbContext context) : base(context)
        {
        }
        public async override Task<AppTask> FindByIdAsync(int id)
        {
            return await context.AppTask.FindAsync(id);
        }

        public async Task<IEnumerable<AppTask>> SearchAsync(TaskSearchOptions searchOptions)
        {
            var predicate = PredicateBuilder.True<AppTask>();

            if(searchOptions.UserId > 0)
                predicate = predicate.And(x => x.UserId == searchOptions.UserId);

            if (!string.IsNullOrEmpty(searchOptions.TaskTitle))
                predicate = predicate.And(x => x.TaskTitle.Contains(searchOptions.TaskTitle));
                
            if (!string.IsNullOrEmpty(searchOptions.TaskDetails))
                predicate = predicate.And(x => x.TaskDetails.Contains(searchOptions.TaskDetails));
            
            if(searchOptions.TaskCreationDate != DateTime.MinValue) 
                predicate = predicate.And(x => x.TaskCreationDate == searchOptions.TaskCreationDate);

            if(searchOptions.StartDate != DateTime.MinValue)
                predicate = predicate.And(x => x.TaskCreationDate >= searchOptions.StartDate);

            if(searchOptions.EndDate != DateTime.MinValue)
                predicate = predicate.And(x => x.TaskCreationDate <= searchOptions.EndDate);

            if(searchOptions.TaskStatus == true)
                predicate = predicate.And(x => x.TaskStatus == searchOptions.TaskStatus);
            
            if (!string.IsNullOrEmpty(searchOptions.TaskPriority))
                predicate = predicate.And(x => x.TaskPriority == searchOptions.TaskPriority);

            var pred = predicate.Compile();
            var search = await context.AppTask.ToListAsync();
            var result = search.Where(pred);
            return result;
        }
    }
}